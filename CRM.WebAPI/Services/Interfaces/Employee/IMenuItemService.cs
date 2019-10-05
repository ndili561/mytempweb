using System.Collections.Generic;
using System.Threading.Tasks;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;

namespace CRM.WebAPI.Services.Interfaces.Employee
{
    public interface IMenuItemService
    {
        Task<MenuItemDto> GetAsync(int id);
        Task<int> SaveAsync(MenuItemDto menuItem);
        Task<BaseEntity> SaveAndReturnEntityAsync(MenuItemDto entityDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MenuItemDto>> GetAsync();
        Task<bool> MenuItemExistsAsync(int id);
        PageResult<MenuItemDto> GetAllAsync(ODataQueryOptions<MenuItem> options);
    }
}