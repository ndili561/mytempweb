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
    public class RelationshipService : IRelationshipService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public RelationshipService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RelationshipDto>> GetAsync()
        {
            var list = await _repository.Get<Relationship>().ToListAsync();
            return _mapper.Map<IList<RelationshipDto>>(list);
        }

        public async Task<bool> RelationshipExistsAsync(int id)
        {
            return await _repository.Get<Relationship>().AnyAsync(p => p.Id == id);
        }

        public PageResult<RelationshipDto> GetAllAsync(ODataQueryOptions<Relationship> options)
        {
                var items = _repository.Get<Relationship>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Relationship>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<RelationshipDto>>(items);
                return new PageResult<RelationshipDto>(result, null, count);
        }

        public async Task<RelationshipDto> GetAsync(int id)
        {
            var relationship = await _repository.GetAsync<Relationship>(id);

            return  _mapper.Map<RelationshipDto>(relationship);
        }

        public async Task<int> SaveAsync(RelationshipDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(RelationshipDto entityDto)
        {
            var entity = _mapper.Map<Relationship>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var relationship = await _repository.GetAsync<Relationship>(id);

            if (relationship == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(relationship);

            return result;
        }
    }
}
