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
using UserDiary = CRM.DAL.Database.Entities.Employee.UserDiary;
namespace CRM.WebAPI.Services.Employee
{
    public class UserDiaryService : IUserDiaryService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserDiaryService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDiaryDto>> GetAsync()
        {
            var list = await _repository.Get<UserDiary>().ToListAsync();
            return _mapper.Map<IList<UserDiaryDto>>(list);
        }

        public async Task<bool> UserDiaryExistsAsync(int id)
        {
            return await _repository.Get<UserDiary>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserDiaryDto> GetAllAsync(ODataQueryOptions<UserDiary> options)
        {
                var items = _repository.Get<UserDiary>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserDiary>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserDiaryDto>>(items);
                return new PageResult<UserDiaryDto>(result, null, count);
        }

        public async Task<UserDiaryDto> GetAsync(int id)
        {
            var userDiary = await _repository.GetAsync<UserDiary>(id);

            return  _mapper.Map<UserDiaryDto>(userDiary);
        }

        public async Task<int> SaveAsync(UserDiaryDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserDiaryDto entityDto)
        {
            var entity = _mapper.Map<UserDiary>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userDiary = await _repository.GetAsync<UserDiary>(id);

            if (userDiary == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userDiary);
            return result;
        }
    }
}
