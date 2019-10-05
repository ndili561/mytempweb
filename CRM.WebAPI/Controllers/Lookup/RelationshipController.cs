using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Lookup
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RelationshipController  : BaseController<BaseEntity>
    {
        private readonly IRelationshipService _relationshipService;

        public RelationshipController(IRelationshipService relationshipService)
        {
            _relationshipService = relationshipService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _relationshipService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetRelationships")]
        public PageResult<RelationshipDto> GetRelationships(ODataQueryOptions<Relationship> options)
        {
            return _relationshipService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _relationshipService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RelationshipDto relationship)
        {
            if (relationship.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _relationshipService.SaveAndReturnEntityAsync(relationship));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]RelationshipDto relationship)
        {
            if (id == 0 || relationship.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _relationshipService.SaveAndReturnEntityAsync(relationship));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _relationshipService.RelationshipExistsAsync(id),
                async () => await _relationshipService.DeleteAsync(id));
        }
    }
}