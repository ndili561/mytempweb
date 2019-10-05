using System;
using System.Threading.Tasks;
using CRM.Dto.Common;
using CRM.Dto.Settings;
using Microsoft.Extensions.Options;

namespace DocumentApi.HttpClient
{
    /// <summary>
    /// </summary>
    public class DocumentApiClient : BaseClient, IDocumentApiClient
    {
        private readonly string _personFilePath;
        private readonly string _userFilePath;
        private readonly string _emailAttachmentPath;
        public DocumentApiClient(IOptions<HttpClientSettings> httpClientSettings) : base(httpClientSettings)
        {
            _personFilePath = DocumentFolder + "Person\\{0}_{1}_{2}";
            _userFilePath = DocumentFolder + "User\\{0}_{1}_{2}";
            _emailAttachmentPath = DocumentFolder + "EmailAttachment\\{0}_{1}";
        }

        public async Task<FileMetadata> EncryptAndSaveFile(FileMetadata model)
        {
            if (model.PersonId.HasValue)
            {
                model.FileUncPath = string.Format(_personFilePath, model.PersonId, DateTime.Now.ToFileTimeUtc(), model.FileName);
            }
            else if (model.UserId.HasValue)
            {
                model.FileUncPath = string.Format(_userFilePath, model.PersonId, DateTime.Now.ToFileTimeUtc(), model.FileName);
            }
            else
            {
                model.FileUncPath = string.Format(_emailAttachmentPath, DateTime.Now.ToFileTimeUtc(), model.FileName);
            }
            var url = DocumentApiUri + "FileManagement/EncryptAndSaveFile";
            var result = await PostRequestToApi(url, model);
            return result;
        }

        public async Task<FileMetadata> DecryptAndGetFile(FileMetadata model)
        {
            var url = DocumentApiUri + "FileManagement/DecryptAndGetFile";
            var result = await PostRequestToApi(url, model);
            return result;
        }
    }
}
