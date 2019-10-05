using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Dto.Common;
using DocumentApi.HttpClient;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonDocumentService : IPersonDocumentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDocumentApiClient _documentApiClient;
        public PersonDocumentService(IDocumentApiClient documentApiClient,IRepository repository, IMapper mapper)
        {
            _documentApiClient = documentApiClient;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDocumentDto>> GetAsync()
        {
            var list = await _repository.Get<PersonDocument>().ToListAsync();
            return _mapper.Map<IList<PersonDocumentDto>>(list);
        }

        public async Task<bool> PersonDocumentExistsAsync(int id)
        {
            return await _repository.Get<PersonDocument>().AnyAsync(p => p.Id == id);
        }


        public async Task<PersonDocumentDto> GetAsync(int id)
        {
            var personDocument = await _repository.CRMContext.PersonDocuments
                .Include(x => x.Document)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<PersonDocumentDto>(personDocument);
        }

        public async Task<int> SaveAsync(PersonDocumentDto personDocument)
        {
            var result = await SaveAndReturnEntityAsync(personDocument);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonDocumentDto entityDto)
        {
            var personDocument = _mapper.Map<PersonDocument>(entityDto);
            var documentDb = entityDto.Document;
            var file = new FileMetadata
            {
                FileContent = documentDb.FileContent,
                FileName = documentDb.Name,
                PersonId = documentDb.PersonId,
                UserId = documentDb.UploadById
            };
            file = await _documentApiClient.EncryptAndSaveFile(file);
            personDocument.Document.RelativePath = file.FileUncPath;
            var result = await _repository.SaveAndReturnEntityAsync(personDocument);
            result.SuccessMessage = "The data is saved successfully";
            return result;
          
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personDocument = await _repository.GetAsync<PersonDocument>(id);

            if (personDocument == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personDocument);

            return result;
        }
    }
}
