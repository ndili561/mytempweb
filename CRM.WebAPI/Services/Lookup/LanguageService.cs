using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Lookup
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public LanguageService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LanguageDto>> GetAsync()
        {
            var list = await _repository.Get<Language>().ToListAsync();
            return _mapper.Map<IList<LanguageDto>>(list);
        }

        public async Task<bool> LanguageExistsAsync(int id)
        {
            return await _repository.Get<Language>().AnyAsync(p => p.Id == id);
        }

        public PageResult<LanguageDto> GetAllAsync(ODataQueryOptions<Language> options)
        {
                var items = _repository.Get<Language>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Language>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<LanguageDto>>(items);
                return new PageResult<LanguageDto>(result, null, count);
        }

        public async Task<LanguageDto> GetAsync(int id)
        {
            var language = await _repository.GetAsync<Language>(id);

            return  _mapper.Map<LanguageDto>(language);
        }

        public async Task<int> SaveAsync(LanguageDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(LanguageDto entityDto)
        {
            var entity = _mapper.Map<Language>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var language = await _repository.GetAsync<Language>(id);

            if (language == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(language);

            return result;
        }
    }
}
