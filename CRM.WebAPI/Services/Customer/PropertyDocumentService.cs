using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.Dto.Common;
using DocumentApi.HttpClient;

namespace CRM.WebAPI.Services.Customer
{
    public class PropertyDocumentService : IPropertyDocumentService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDocumentApiClient _documentApiClient;
        public PropertyDocumentService(IDocumentApiClient documentApiClient,IRepository repository, IMapper mapper)
        {
            _documentApiClient = documentApiClient;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PropertyDocumentDto>> GetAsync()
        {
            var list = await _repository.Get<PropertyDocument>().ToListAsync();
            return _mapper.Map<IList<PropertyDocumentDto>>(list);
        }

        public async Task<bool> PropertyDocumentExistsAsync(int id)
        {
            return await _repository.Get<PropertyDocument>().AnyAsync(p => p.Id == id);
        }


        public async Task<PropertyDocumentDto> GetAsync(int id)
        {
            var propertyDocument = await _repository.CRMContext.PropertyDocuments
                .Include(x=>x.Document)
                .FirstOrDefaultAsync(x=>x.Id ==id);

            return _mapper.Map<PropertyDocumentDto>(propertyDocument);
        }

        public async Task<int> SaveAsync(PropertyDocumentDto propertyDocument)
        {
            var result = await SaveAndReturnEntityAsync(propertyDocument);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PropertyDocumentDto entityDto)
        {
            var propertyDocument = _mapper.Map<PropertyDocument>(entityDto);
            var documentDb = entityDto.Document;
            var file = new FileMetadata
            {
                FileContent = documentDb.FileContent,
                FileName = documentDb.Name,
                PersonId = documentDb.PersonId,
                UserId = documentDb.UploadById
            };
            file = await _documentApiClient.EncryptAndSaveFile(file);
            propertyDocument.Document.RelativePath = file.FileUncPath;
            var result = await _repository.SaveAndReturnEntityAsync(propertyDocument);
            result.SuccessMessage = "The data is saved successfully";
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var propertyDocument = await _repository.CRMContext.PropertyDocuments
                .Include(x => x.Document)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (propertyDocument == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(propertyDocument);
            await _repository.HardDeleteAsync(propertyDocument.Document);
            return result;
        }
    }
}
