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
    public class AlertGroupService : IAlertGroupService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlertGroupService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertGroupDto>> GetAsync()
        {
            var list = await _repository.Get<AlertGroup>().ToListAsync();
            return _mapper.Map<IList<AlertGroupDto>>(list);
        }

        public async Task<bool> AlertGroupExistsAsync(int id)
        {
            return await _repository.Get<AlertGroup>().AnyAsync(p => p.Id == id);
        }

        public async Task<AlertGroupDto> GetAsync(int id)
        {
            var alert = await _repository.GetAsync<AlertGroup>(id);

            return  _mapper.Map<AlertGroupDto>(alert);
        }

        public async Task<int> SaveAsync(AlertGroupDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AlertGroupDto entityDto)
        {
            var entity = _mapper.Map<AlertGroup>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _repository.GetAsync<AlertGroup>(id);

            if (alert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(alert);

            return result;
        }
    }
}
