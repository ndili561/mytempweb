using System.Threading.Tasks;
using CRM.Entity.Model.Common;
using CRM.Entity.Search.Common;
using CRM.WebApiClient.Interface.Common;
using CRM.WebApiClient.Interface.Facade;

namespace CRM.WebApiClient.HttpClient.Facade
{
    public class CommonFacadeApiClient: ICommonFacadeApiClient
    {
        private readonly IEmailApiClient _emailApiClient;
        private readonly IDocumentApiClient _documentApiClient;
        private readonly ISmsApiClient _smsApiClient;
        public CommonFacadeApiClient(IEmailApiClient emailApiClient,
            IDocumentApiClient documentApiClient,
            ISmsApiClient smsApiClient) 
        {
            _emailApiClient = emailApiClient;
            _documentApiClient = documentApiClient;
            _smsApiClient = smsApiClient;
        }

        public async Task<DocumentSearchModel> GetDocuments(DocumentSearchModel model)
        {
            return await _documentApiClient.GetDocuments(model);
        }

        public async Task<DocumentDto> GetDocument(int id)
        {
            return await _documentApiClient.GetDocument(id);
        }

        public async Task<FileMetadata> GetDocumentFile(FileMetadata model)
        {
            return await _documentApiClient.GetDocumentFile(model);
        }

        public async Task<DocumentDto> PostDocument(DocumentDto model)
        {
            return await _documentApiClient.PostDocument(model);
        }

        public async Task<DocumentDto> PutDocument(int id, DocumentDto model)
        {
            return await _documentApiClient.PutDocument(id,model);
        }

        public async Task<EmailSearchModel> GetEmails(EmailSearchModel model)
        {
            return await _emailApiClient.GetEmails(model);
        }

        public async Task<EmailDto> GetEmail(int id)
        {
            return await _emailApiClient.GetEmail(id);
        }

        public async Task<EmailDto> PostEmail(EmailDto model)
        {
            return await _emailApiClient.PostEmail(model);
        }

        public async Task<EmailDto> PutEmail(int id, EmailDto model)
        {
            return await _emailApiClient.PutEmail(id,model);
        }

        public async Task<SmsSearchModel> GetSmses(SmsSearchModel model)
        {
            return await _smsApiClient.GetSmses(model);
        }

        public async Task<SmsDto> GetSms(int id)
        {
            return await _smsApiClient.GetSms(id);
        }

        public async Task<SmsDto> PostSms(SmsDto model)
        {
            return await _smsApiClient.PostSms(model);
        }

        public async Task<SmsDto> PutSms(int id, SmsDto model)
        {
            return await _smsApiClient.PutSms(id,model);
        }
    }
}