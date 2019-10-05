using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Customer
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddressController  : BaseController<BaseEntity>
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _addressService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetAddresses")]
        public PageResult<AddressDto> GetAddresses(ODataQueryOptions<Address> options)
        {
            return _addressService.GetAllAsync(options);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _addressService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddressDto addressDto)
        {
            if (addressDto.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _addressService.SaveAndReturnEntityAsync(addressDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]AddressDto addressDto)
        {
            if (id == 0 || addressDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _addressService.SaveAndReturnEntityAsync(addressDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _addressService.AddressExistsAsync(id),
                async () => await _addressService.DeleteAsync(id));
        }
    }
}