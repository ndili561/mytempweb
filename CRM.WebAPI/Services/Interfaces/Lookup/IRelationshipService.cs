using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Lookup
{
    public interface IRelationshipService
    {
        Task<RelationshipDto> GetAsync(int id);
        Task<int> SaveAsync(RelationshipDto user);
        Task<BaseEntity> SaveAndReturnEntityAsync(RelationshipDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RelationshipDto>> GetAsync();
        Task<bool> RelationshipExistsAsync(int id);
        PageResult<RelationshipDto> GetAllAsync(ODataQueryOptions<Relationship> options);
    }
}