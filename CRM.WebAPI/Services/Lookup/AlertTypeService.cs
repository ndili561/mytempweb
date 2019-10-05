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
    public class AlertTypeService : IAlertTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlertTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlertTypeDto>> GetAsync()
        {
            var list = await _repository.Get<AlertType>().ToListAsync();
            return _mapper.Map<IList<AlertTypeDto>>(list);
        }

        public async Task<bool> AlertTypeExistsAsync(int id)
        {
            return await _repository.Get<AlertType>().AnyAsync(p => p.Id == id);
        }

        public async Task<AlertTypeDto> GetAsync(int id)
        {
            var alert = await _repository.GetAsync<AlertType>(id);

            return  _mapper.Map<AlertTypeDto>(alert);
        }

        public async Task<int> SaveAsync(AlertTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AlertTypeDto entityDto)
        {
            var entity = _mapper.Map<AlertType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _repository.GetAsync<AlertType>(id);

            if (alert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(alert);

            return result;
        }
    }
}
