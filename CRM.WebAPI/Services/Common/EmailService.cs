using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Repository;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using DocumentApi.HttpClient;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Common
{
    public class EmailService : IEmailService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDocumentApiClient _documentApiClient;
        public EmailService(IRepository repository, IDocumentApiClient documentApiClient, IMapper mapper)
        {
            _repository = repository;
            _documentApiClient = documentApiClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmailDto>> GetAsync()
        {
            var list = await _repository.Get<Email>().ToListAsync();
            return _mapper.Map<IList<EmailDto>>(list);
        }

        public async Task<bool> EmailExistsAsync(int id)
        {
            return await _repository.Get<Email>().AnyAsync(p => p.Id == id);
        }

        public PageResult<EmailDto> GetAllAsync(ODataQueryOptions<Email> options)
        {
                var items = _repository.Get<Email>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Email>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<EmailDto>>(items);
                return new PageResult<EmailDto>(result, null, count);
        }

        public async Task<EmailDto> GetAsync(int id)
        {
            var email = await _repository.GetAsync<Email>(id);

            return  _mapper.Map<EmailDto>(email);
        }

        public async Task<int> SaveAsync(EmailDto email)
        {
            var result = await SaveAndReturnEntityAsync(email);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(EmailDto entityDto)
        {
            
            var emailDb = _mapper.Map<Email>(entityDto);
            emailDb = AddCcAndBcc(emailDb, entityDto);
            foreach (var attachment in entityDto.Attachments)
            {
                var file = new FileMetadata
                {
                    FileContent = attachment.FileContent,
                    FileName = attachment.Name
                };
                file = await _documentApiClient.EncryptAndSaveFile(file);
                attachment.RelativePath = file.FileUncPath;
            }
            var result = await _repository.SaveAndReturnEntityAsync(emailDb);
            return result;
        }

        private Email AddCcAndBcc(Email emailDb, EmailDto entityDto)
        {
            char[] delimiterChars = { ',', ';' };
            var toEmailAddresses = entityDto.ToEmailAddress.Split(delimiterChars).ToList();
            var ccEmailAddresses = entityDto.ToEmailAddress.Split(delimiterChars);
            var bccEmailAddresses = entityDto.ToEmailAddress.Split(delimiterChars);

            var toEmailEmployees = _repository.Get<User>().Where(x => toEmailAddresses.Contains(x.Email));
            var ccEmailEmployees = _repository.Get<User>().Where(x => ccEmailAddresses.Contains(x.Email));
            var bccEmailEmployees = _repository.Get<User>().Where(x => bccEmailAddresses.Contains(x.Email));
            foreach (var emailEmployee in toEmailEmployees)
            {
                emailDb.PersonEmails.Add(new PersonEmail { PersonId = emailEmployee.Id, EmailStatusId = 1 });
            }
            foreach (var emailEmployee in ccEmailEmployees)
            {
                emailDb.PersonEmails.Add(new PersonEmail { PersonId = emailEmployee.Id, EmailStatusId = 1, IsCc = true });
            }
            foreach (var emailEmployee in bccEmailEmployees)
            {
                emailDb.PersonEmails.Add(new PersonEmail { PersonId = emailEmployee.Id, EmailStatusId = 1, IsBcc = true });
            }
            return emailDb;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var email = await _repository.GetAsync<Email>(id);

            if (email == null)
            {
                return false;
            }


            var result = await _repository.HardDeleteAsync(email);

            return result;
        }
    }
}
