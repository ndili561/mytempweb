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
using Message = CRM.DAL.Database.Entities.Employee.Message;
namespace CRM.WebAPI.Services.Employee
{
    public class MessageService : IMessageService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MessageService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> GetAsync()
        {
            var list = await _repository.Get<Message>().ToListAsync();
            return _mapper.Map<IList<MessageDto>>(list);
        }

        public async Task<bool> MessageExistsAsync(int id)
        {
            return await _repository.Get<Message>().AnyAsync(p => p.Id == id);
        }

        public PageResult<MessageDto> GetAllAsync(ODataQueryOptions<Message> options)
        {
                var items = _repository.Get<Message>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Message>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<MessageDto>>(items);
                return new PageResult<MessageDto>(result, null, count);
        }

        public async Task<MessageDto> GetAsync(int id)
        {
            var message = await _repository.GetAsync<Message>(id);
            return  _mapper.Map<MessageDto>(message);
        }

        public async Task<int> SaveAsync(MessageDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(MessageDto entityDto)
        {
            var entity = _mapper.Map<Message>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var message = await _repository.GetAsync<Message>(id);

            if (message == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(message);

            return result;
        }
    }
}
