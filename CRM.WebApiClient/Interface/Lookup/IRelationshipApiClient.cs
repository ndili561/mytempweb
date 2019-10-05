using System.Threading.Tasks;
using CRM.Entity.Model.Lookup;
using CRM.Entity.Search.Lookup;

namespace CRM.WebApiClient.Interface.Lookup
{
    /// <summary>
    /// </summary>
    public interface IRelationshipApiClient
    {
        Task<RelationshipSearchModel> GetRelationships(RelationshipSearchModel model);
        Task<RelationshipDto> GetRelationship(int id);
        Task<RelationshipDto> PostRelationship(RelationshipDto model);
        Task<RelationshipDto> PutRelationship(int id,RelationshipDto model);
    }
}