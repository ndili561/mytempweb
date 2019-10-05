using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Lookup;
using CRM.DAL.Repository;
using CRM.Dto.Lookup;
using CRM.WebAPI.Services.Interfaces.Lookup;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Lookup
{
    public class PriorityService : IPriorityService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PriorityService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PriorityDto>> GetAsync()
        {
            var list = await _repository.Get<Priority>().ToListAsync();
            return _mapper.Map<IList<PriorityDto>>(list);
        }

        public async Task<bool> PriorityExistsAsync(int id)
        {
            return await _repository.Get<Priority>().AnyAsync(p => p.Id == id);
        }

        public async Task<PriorityDto> GetAsync(int id)
        {
            var flag = await _repository.GetAsync<Priority>(id);

            return  _mapper.Map<PriorityDto>(flag);
        }

        public async Task<int> SaveAsync(PriorityDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PriorityDto entityDto)
        {
            var entity = _mapper.Map<Priority>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var priority = await _repository.GetAsync<Priority>(id);

            if (priority == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(priority);

            return result;
        }
    }
}
