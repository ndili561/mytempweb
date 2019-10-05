using System;
using System.Collections.Generic;
using System.Data;
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
    public class NationalityTypeService : INationalityTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public NationalityTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NationalityTypeDto>> GetAsync()
        {
            var list = await _repository.Get<NationalityType>().ToListAsync();
            return _mapper.Map<IList<NationalityTypeDto>>(list);
        }

        public async Task<bool> NationalityExistsAsync(int id)
        {
            return await _repository.Get<NationalityType>().AnyAsync(p => p.Id == id);
        }

        public PageResult<NationalityTypeDto> GetAllAsync(ODataQueryOptions<NationalityType> options)
        {
                var items = _repository.Get<NationalityType>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<NationalityType>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<NationalityTypeDto>>(items);
                return new PageResult<NationalityTypeDto>(result, null, count);
        }

        public async Task<NationalityTypeDto> GetAsync(int id)
        {
            var nationalityType = await _repository.GetAsync<NationalityType>(id);

            return  _mapper.Map<NationalityTypeDto>(nationalityType);
        }

        public async Task<int> SaveAsync(NationalityTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(NationalityTypeDto entityDto)
        {
            var entity = _mapper.Map<NationalityType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var nationalityType = await _repository.GetAsync<NationalityType>(id);

            if (nationalityType == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(nationalityType);

            return result;
        }
    }
}
