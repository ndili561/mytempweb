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
using ContactByOption = CRM.DAL.Database.Entities.Lookup.ContactByOption;

namespace CRM.WebAPI.Services.Lookup
{
    public class ContactByOptionService : IContactByOptionService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ContactByOptionService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactByOptionDto>> GetAsync()
        {
            var list = await _repository.Get<ContactByOption>().ToListAsync();
            return _mapper.Map<IList<ContactByOptionDto>>(list);
        }

        public async Task<bool> ContactByOptionExistsAsync(int id)
        {
            return await _repository.Get<ContactByOption>().AnyAsync(p => p.Id == id);
        }

        public PageResult<ContactByOptionDto> GetAllAsync(ODataQueryOptions<ContactByOption> options)
        {
                var items = _repository.Get<ContactByOption>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<ContactByOption>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<ContactByOptionDto>>(items);
                return new PageResult<ContactByOptionDto>(result, null, count);
        }

        public async Task<ContactByOptionDto> GetAsync(int id)
        {
            var title = await _repository.GetAsync<ContactByOption>(id);

            return _mapper.Map<ContactByOptionDto>(title);
        }

        public async Task<int> SaveAsync(ContactByOptionDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(ContactByOptionDto entityDto)
        {
            var entity = _mapper.Map<ContactByOption>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var title = await _repository.GetAsync<ContactByOption>(id);

            if (title == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(title);

            return result;
        }
    }
}
