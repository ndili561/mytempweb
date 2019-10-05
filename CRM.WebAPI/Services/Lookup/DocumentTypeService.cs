using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using DocumentType = CRM.DAL.Database.Entities.Lookup.DocumentType;

namespace CRM.WebAPI.Services.Lookup
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public DocumentTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DocumentTypeDto>> GetAsync()
        {
            var list = await _repository.Get<DocumentType>().ToListAsync();
            return _mapper.Map<IList<DocumentTypeDto>>(list);
        }

        public async Task<bool> DocumentTypeExistsAsync(int id)
        {
            return await _repository.Get<DocumentType>().AnyAsync(p => p.Id == id);
        }

        public PageResult<DocumentTypeDto> GetAllAsync(ODataQueryOptions<DocumentType> options)
        {
                var items = _repository.Get<DocumentType>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<DocumentType>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<DocumentTypeDto>>(items);
                return new PageResult<DocumentTypeDto>(result, null, count);
        }

        public async Task<DocumentTypeDto> GetAsync(int id)
        {
            var documentType = await _repository.GetAsync<DocumentType>(id);

            return  _mapper.Map<DocumentTypeDto>(documentType);
        }

        public async Task<int> SaveAsync(DocumentTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(DocumentTypeDto entityDto)
        {
            var entity = _mapper.Map<DocumentType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var documentType = await _repository.GetAsync<DocumentType>(id);

            if (documentType == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(documentType);

            return result;
        }
    }
}
