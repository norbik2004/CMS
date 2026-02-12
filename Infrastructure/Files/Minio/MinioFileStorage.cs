using Application.Commons.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Files.Minio
{
    public class MinioFileStorage : IMinioFileStorage
    {

        public const int MaxFileSize = 10 * 1024 * 1024; // 10 MB (10 megabajtów);
        private readonly ILogger<MinioFileStorage> _logger;
        private readonly IMinioClient _minioClient;

        public MinioFileStorage(IConfiguration configuration, ILogger<MinioFileStorage> logger)
        {
            _logger = logger;
            var minioSettings = configuration.GetSection("Minio");
            bool.TryParse(minioSettings["UseSSL"], out var useSSL);

            _minioClient = new MinioClient()
                .WithEndpoint($"{minioSettings["Host"]}:{minioSettings["Port"]}")
                .WithCredentials(minioSettings["Login"], minioSettings["Password"])
                .WithSSL(useSSL)
                .Build();

            _logger.LogInformation("MinioFileSender initialized with endpoint {Endpoint}", minioSettings["Host"]);
        }

        public async Task<bool> DeleteFileAsync(string bucketName, string fileName)
        {
            try
            {
                await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName));

                _logger.LogInformation("Successfully deleted file '{FileName}' from bucket '{BucketName}'", fileName,
                    bucketName);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting file '{FileName}' from bucket '{BucketName}'", fileName,
                    bucketName);
                return false;
            }
        }

        public async Task<MemoryStream> DownloadFileAsync(string bucketName, string fileName)
        {
            try
            {
                var memoryStream = new MemoryStream();

                await _minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

                memoryStream.Position = 0;

                _logger.LogInformation("Successfully downloaded file '{FileName}' from bucket '{BucketName}'", fileName,
                    bucketName);

                return memoryStream;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while downloading file '{FileName}' from bucket '{BucketName}'",
                    fileName, bucketName);
                throw new BadRequestException("Something went wrong while downloading file");
            }
        }

        public async Task<bool> SaveFilesAsync(List<(string ObjectPath, IFormFile File)> files, string bucketName)
        {
            try
            {
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
                if (!await _minioClient.BucketExistsAsync(bucketExistsArgs))
                {
                    await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                    _logger.LogInformation("Bucket '{BucketName}' created", bucketName);
                }

                foreach (var (objectPath, file) in files)
                {
                    if (file.Length > MaxFileSize)
                        throw new BadRequestException("File size is too big");

                    using var stream = file.OpenReadStream();
                    var putArgs = new PutObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(objectPath)
                        .WithStreamData(stream)
                        .WithObjectSize(file.Length)
                        .WithContentType(file.ContentType);

                    await _minioClient.PutObjectAsync(putArgs);

                    _logger.LogInformation("Uploaded file '{FileName}' to path '{ObjectPath}' in bucket '{BucketName}'",
                        file.FileName, objectPath, bucketName);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving files to bucket '{BucketName}'", bucketName);
                return false;
            }
        }
    }
}
