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
using Ethnicity = CRM.DAL.Database.Entities.Lookup.Ethnicity;

namespace CRM.WebAPI.Services.Lookup
{
    public class EthnicityService : IEthnicityService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public EthnicityService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EthnicityDto>> GetAsync()
        {
            var list = await _repository.Get<Ethnicity>().ToListAsync();
            return _mapper.Map<IList<EthnicityDto>>(list);
        }

        public async Task<bool> EthnicityExistsAsync(int id)
        {
            return await _repository.Get<Ethnicity>().AnyAsync(p => p.Id == id);
        }

        public PageResult<EthnicityDto> GetAllAsync(ODataQueryOptions<Ethnicity> options)
        {
                var items = _repository.Get<Ethnicity>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Ethnicity>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<EthnicityDto>>(items);
                return new PageResult<EthnicityDto>(result, null, count);
        }

        public async Task<EthnicityDto> GetAsync(int id)
        {
            var ethnicity = await _repository.GetAsync<Ethnicity>(id);

            return _mapper.Map<EthnicityDto>(ethnicity);
        }

        public async Task<int> SaveAsync(EthnicityDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(EthnicityDto entityDto)
        {
            var entity = _mapper.Map<Ethnicity>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ethnicity = await _repository.GetAsync<Ethnicity>(id);

            if (ethnicity == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(ethnicity);

            return result;
        }
    }
}
