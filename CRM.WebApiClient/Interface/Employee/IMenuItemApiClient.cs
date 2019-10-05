using System.Threading.Tasks;
using CRM.Entity.Model.Employee;
using CRM.Entity.Search.Employee;

namespace CRM.WebApiClient.Interface.Employee
{
    /// <summary>
    /// </summary>
    public interface IMenuItemApiClient
    {
        Task<MenuItemSearchModel> GetMenuItems(MenuItemSearchModel model);
        Task<MenuItemDto> GetMenuItem(int menuItemId);
        Task<MenuItemDto> PostMenuItem(MenuItemDto model);
        Task<MenuItemDto> PutMenuItem(int id, MenuItemDto model);
    }
}