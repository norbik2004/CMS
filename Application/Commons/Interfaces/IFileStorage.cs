using Microsoft.AspNetCore.Http;

namespace Application.Commons.Interfaces
{
    public interface IFileStorage
    {
        Task<bool> DeleteFileAsync(
            string bucketName,
            string fileName,
            CancellationToken ct = default);

        Task<bool> SaveFilesAsync(
            List<(string ObjectPath, IFormFile File)> files,
            string bucketName,
            CancellationToken ct = default);
    }
}
