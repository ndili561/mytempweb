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
using UserMessage = CRM.DAL.Database.Entities.Employee.UserMessage;
namespace CRM.WebAPI.Services.Employee
{
    public class UserMessageService : IUserMessageService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserMessageService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserMessageDto>> GetAsync()
        {
            var list = await _repository.Get<UserMessage>().ToListAsync();
            return _mapper.Map<IList<UserMessageDto>>(list);
        }

        public async Task<bool> UserMessageExistsAsync(int id)
        {
            return await _repository.Get<UserMessage>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserMessageDto> GetAllAsync(ODataQueryOptions<UserMessage> options)
        {
                var items = _repository.Get<UserMessage>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<UserMessage>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<UserMessageDto>>(items);
                return new PageResult<UserMessageDto>(result, null, count);
        }

        public async Task<UserMessageDto> GetAsync(int id)
        {
            var userMessage = await _repository.GetAsync<UserMessage>(id);

            return  _mapper.Map<UserMessageDto>(userMessage);
        }

        public async Task<int> SaveAsync(UserMessageDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserMessageDto entityDto)
        {
            var entity = _mapper.Map<UserMessage>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userMessage = await _repository.GetAsync<UserMessage>(id);

            if (userMessage == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(userMessage);
            return result;
        }
    }
}
