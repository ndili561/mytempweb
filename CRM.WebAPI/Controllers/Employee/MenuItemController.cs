using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI.Controllers.Employee
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MenuItemController  : BaseController<BaseEntity>
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _menuItemService.GetAsync());
        }
        [HttpGet("{options}", Name = "GetMenuItems")]
        public PageResult<MenuItemDto> GetMenuItems(ODataQueryOptions<MenuItem> options)
        {
            return _menuItemService.GetAllAsync(options);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            return await GetSingleAsync(async () => await _menuItemService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MenuItemDto menuItem)
        {
            if (menuItem.Id != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Identity insert is not permitted.");
            }

            return await SaveAndReturnEntityAsync(async () => await _menuItemService.SaveAndReturnEntityAsync(menuItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]MenuItemDto menuItem)
        {
            if (id == 0 || menuItem.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Id needs to be greater than 0.");
            }

            return await SaveAndReturnEntityAsync(async () => await _menuItemService.SaveAndReturnEntityAsync(menuItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await DeleteAsync(
                async () => await _menuItemService.MenuItemExistsAsync(id),
                async () => await _menuItemService.DeleteAsync(id));
        }
    }
}