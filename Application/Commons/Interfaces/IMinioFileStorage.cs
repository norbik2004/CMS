using Microsoft.AspNetCore.Http;

namespace Application.Commons.Interfaces
{
    public interface IMinioFileStorage
    {
        Task<bool> DeleteFileAsync(
            string bucketName,
            string fileName);

        Task<bool> SaveFilesAsync(
            List<(string ObjectPath, IFormFile File)> files,
            string bucketName);

        Task<MemoryStream> DownloadFileAsync(string bucketName, string fileName);
    }
}
