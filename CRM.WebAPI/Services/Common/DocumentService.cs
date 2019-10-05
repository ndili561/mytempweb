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
    public class DocumentService : IDocumentService
    {
        private readonly IRepository _repository;
        private readonly IDocumentApiClient _documentApiClient;
        private readonly IMapper _mapper;

        public DocumentService(IRepository repository, IDocumentApiClient documentApiClient, IMapper mapper)
        {
            _repository = repository;
            _documentApiClient = documentApiClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentDto>> GetAsync()
        {
            var list = await _repository.Get<Document>().ToListAsync();
            return _mapper.Map<IList<DocumentDto>>(list);
        }

        public async Task<bool> DocumentExistsAsync(int id)
        {
            return await _repository.Get<Document>().AnyAsync(p => p.Id == id);
        }

        public PageResult<DocumentDto> GetAllAsync(ODataQueryOptions<Document> options)
        {
            var items = _repository.Get<Document>().AsQueryable();
            var count = items.Count();
            if (options.OrderBy != null)
                items = options.OrderBy.ApplyTo(items);  //perform sort 
            if (options.Filter != null)
                items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Document>();  //perform filter 

            count = items.ToList().Count;


            if (options.Skip != null)
                items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
            if (options.Top != null)
                items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
            var result = _mapper.Map<List<DocumentDto>>(items);
            return new PageResult<DocumentDto>(result, null, count);
        }

        public async Task<DocumentDto> GetAsync(int id)
        {
            
            var document = await _repository.GetAsync<Document>(id);
            var documentDto = _mapper.Map<DocumentDto>(document);
            var file = new FileMetadata{FileUncPath = document.RelativePath };
            file = await _documentApiClient.DecryptAndGetFile(file);
            documentDto.FileContent = file.FileContent;
            return documentDto;
        }

        public async Task<int> SaveAsync(DocumentDto document)
        {
            var result = await SaveAndReturnEntityAsync(document);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(DocumentDto entityDto)
        {
            var documentDb = _mapper.Map<Document>(entityDto);
            var file = new FileMetadata
            {
                FileContent = entityDto.FileContent,
                FileName = documentDb.Name,
                PersonId = entityDto.PersonId,
                UserId = entityDto.UploadById
            };
            file = await _documentApiClient.EncryptAndSaveFile(file);
            documentDb.RelativePath = file.FileUncPath;
            var result = await _repository.SaveAndReturnEntityAsync(documentDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var document = await _repository.GetAsync<Document>(id);
            if (document == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(document);
            return result;
        }
    }
}
