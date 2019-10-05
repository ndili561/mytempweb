using System.Threading.Tasks;
using CRM.Dto.Common;

namespace DocumentApi.HttpClient
{
    /// <summary>
    /// </summary>
    public interface IDocumentApiClient
    {
        Task<FileMetadata> EncryptAndSaveFile(FileMetadata model);
        Task<FileMetadata> DecryptAndGetFile(FileMetadata model);

    }
}