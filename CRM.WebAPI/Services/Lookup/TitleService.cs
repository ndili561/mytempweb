using System;
using System.Collections.Generic;
using System.Data;
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
using Title = CRM.DAL.Database.Entities.Lookup.Title;

namespace CRM.WebAPI.Services.Lookup
{
    public class TitleService : ITitleService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TitleService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TitleDto>> GetAsync()
        {
            var list = await _repository.Get<Title>().ToListAsync();
            return _mapper.Map<IList<TitleDto>>(list);
        }

        public async Task<bool> TitleExistsAsync(int id)
        {
            return await _repository.Get<Title>().AnyAsync(p => p.Id == id);
        }

        public PageResult<TitleDto> GetAllAsync(ODataQueryOptions<Title> options)
        {
                var items = _repository.Get<Title>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Title>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<TitleDto>>(items);
                return new PageResult<TitleDto>(result, null, count);
        }

        public async Task<TitleDto> GetAsync(int id)
        {
            var title = await _repository.GetAsync<Title>(id);

            return _mapper.Map<TitleDto>(title);
        }

        public async Task<int> SaveAsync(TitleDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(TitleDto entityDto)
        {
            var entity = _mapper.Map<Title>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var title = await _repository.GetAsync<Title>(id);

            if (title == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(title);

            return result;
        }
    }
}
