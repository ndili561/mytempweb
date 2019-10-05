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
    public class AlertStatusService : IAlertStatusService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlertStatusService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertStatusDto>> GetAsync()
        {
            var list = await _repository.Get<AlertStatus>().ToListAsync();
            return _mapper.Map<IList<AlertStatusDto>>(list);
        }

        public async Task<bool> AlertStatusExistsAsync(int id)
        {
            return await _repository.Get<AlertStatus>().AnyAsync(p => p.Id == id);
        }

        public async Task<AlertStatusDto> GetAsync(int id)
        {
            var alert = await _repository.GetAsync<AlertStatus>(id);

            return  _mapper.Map<AlertStatusDto>(alert);
        }

        public async Task<int> SaveAsync(AlertStatusDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AlertStatusDto entityDto)
        {
            var entity = _mapper.Map<AlertStatus>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _repository.GetAsync<AlertStatus>(id);

            if (alert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(alert);

            return result;
        }
    }
}
