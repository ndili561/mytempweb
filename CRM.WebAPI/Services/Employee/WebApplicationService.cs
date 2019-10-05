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
using WebApplication = CRM.DAL.Database.Entities.Employee.WebApplication;
namespace CRM.WebAPI.Services.Employee
{
    public class WebApplicationService : IWebApplicationService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public WebApplicationService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WebApplicationDto>> GetAsync()
        {
            var list = await _repository.Get<WebApplication>().ToListAsync();
            return _mapper.Map<IList<WebApplicationDto>>(list);
        }

        public async Task<bool> WebApplicationExistsAsync(int id)
        {
            return await _repository.Get<WebApplication>().AnyAsync(p => p.Id == id);
        }

        public PageResult<WebApplicationDto> GetAllAsync(ODataQueryOptions<WebApplication> options)
        {
                var items = _repository.Get<WebApplication>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<WebApplication>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<WebApplicationDto>>(items);
                return new PageResult<WebApplicationDto>(result, null, count);
        }

        public async Task<WebApplicationDto> GetAsync(int id)
        {
            var webApplication = await _repository.GetAsync<WebApplication>(id);
            return  _mapper.Map<WebApplicationDto>(webApplication);
        }

        public async Task<int> SaveAsync(WebApplicationDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(WebApplicationDto entityDto)
        {
            var entity = _mapper.Map<WebApplication>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var webApplication = await _repository.GetAsync<WebApplication>(id);

            if (webApplication == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(webApplication);
            return result;
        }
    }
}
