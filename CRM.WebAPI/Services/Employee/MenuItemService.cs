using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Repository;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MenuItem = CRM.DAL.Database.Entities.Employee.MenuItem;
namespace CRM.WebAPI.Services.Employee
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MenuItemService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAsync()
        {
            var list = await _repository.Get<MenuItem>().ToListAsync();
            return _mapper.Map<IList<MenuItemDto>>(list);
        }

        public async Task<bool> MenuItemExistsAsync(int id)
        {
            return await _repository.Get<MenuItem>().AnyAsync(p => p.Id == id);
        }

        public PageResult<MenuItemDto> GetAllAsync(ODataQueryOptions<MenuItem> options)
        {
                var items = _repository.Get<MenuItem>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<MenuItem>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<MenuItemDto>>(items);
                return new PageResult<MenuItemDto>(result, null, count);
        }

        public async Task<MenuItemDto> GetAsync(int id)
        {
            var menuItem = await _repository.GetAsync<MenuItem>(id);

            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task<int> SaveAsync(MenuItemDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(MenuItemDto entityDto)
        {
            var entity = _mapper.Map<MenuItem>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menuItem = await _repository.GetAsync<MenuItem>(id);

            if (menuItem == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(menuItem);

            return result;
        }
    }
}
