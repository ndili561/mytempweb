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
using ChatMessage = CRM.DAL.Database.Entities.Employee.ChatMessage;
namespace CRM.WebAPI.Services.Employee
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ChatMessageService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ChatMessageDto>> GetAsync()
        {
            var list = await _repository.Get<ChatMessage>().ToListAsync();
            return _mapper.Map<IList<ChatMessageDto>>(list);
        }

        public async Task<bool> ChatMessageExistsAsync(int id)
        {
            return await _repository.Get<ChatMessage>().AnyAsync(p => p.Id == id);
        }

        public PageResult<ChatMessageDto> GetAllAsync(ODataQueryOptions<ChatMessage> options)
        {
                var items = _repository.Get<ChatMessage>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<ChatMessage>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<ChatMessageDto>>(items);
                return new PageResult<ChatMessageDto>(result, null, count);
        }

        public async Task<ChatMessageDto> GetAsync(int id)
        {
            var chatMessage = await _repository.GetAsync<ChatMessage>(id);

            return  _mapper.Map<ChatMessageDto>(chatMessage);
        }

        public async Task<int> SaveAsync(ChatMessageDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(ChatMessageDto entityDto)
        {
            var entity = _mapper.Map<ChatMessage>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var chatMessage = await _repository.GetAsync<ChatMessage>(id);

            if (chatMessage == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(chatMessage);

            return result;
        }
    }
}
