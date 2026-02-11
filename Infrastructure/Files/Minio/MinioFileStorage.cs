using Application.Commons.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Files.Minio
{
    public class MinioFileStorage() : IFileStorage
    {

        public Task<bool> DeleteFileAsync(string bucketName, string fileName, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveFilesAsync(List<(string ObjectPath, IFormFile File)> files, string bucketName, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
