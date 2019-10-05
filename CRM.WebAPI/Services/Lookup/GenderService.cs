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
using Gender = CRM.DAL.Database.Entities.Lookup.Gender;

namespace CRM.WebAPI.Services.Lookup
{
    public class GenderService : IGenderService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GenderService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenderDto>> GetAsync()
        {
            var list = await _repository.Get<Gender>().ToListAsync();
            return _mapper.Map<IList<GenderDto>>(list);
        }

        public async Task<bool> GenderExistsAsync(int id)
        {
            return await _repository.Get<Gender>().AnyAsync(p => p.Id == id);
        }

        public PageResult<GenderDto> GetAllAsync(ODataQueryOptions<Gender> options)
        {
                var items = _repository.Get<Gender>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Gender>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<GenderDto>>(items);
                return new PageResult<GenderDto>(result, null, count);
        }

        public async Task<GenderDto> GetAsync(int id)
        {
            var gender = await _repository.GetAsync<Gender>(id);

            return  _mapper.Map<GenderDto>(gender);
        }

        public async Task<int> SaveAsync(GenderDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(GenderDto entityDto)
        {
            var entity = _mapper.Map<Gender>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gender = await _repository.GetAsync<Gender>(id);

            if (gender == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(gender);

            return result;
        }
    }
}
