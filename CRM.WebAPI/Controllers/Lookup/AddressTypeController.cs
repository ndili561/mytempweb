using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
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
    public class AddressTypeController  : BaseController<BaseEntity>
    {
        private readonly IAddressTypeService _addressTypeService;

        public AddressTypeController(IAddressTypeService addressTypeService)
        {
            _addressTypeService = addressTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _addressTypeService.GetAsync());
        }
       
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _addressTypeService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddressTypeDto addressType)
        {
            if (addressType.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _addressTypeService.SaveAndReturnEntityAsync(addressType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AddressTypeDto addressType)
        {
            if (id == 0 || addressType.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _addressTypeService.SaveAndReturnEntityAsync(addressType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _addressTypeService.AddressTypeExistsAsync(id),
                async () => await _addressTypeService.DeleteAsync(id));
        }
    }
}