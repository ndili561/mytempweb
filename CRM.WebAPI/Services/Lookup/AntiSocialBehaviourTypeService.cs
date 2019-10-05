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
    public class AntiSocialBehaviourTypeService : IAntiSocialBehaviourTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AntiSocialBehaviourTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AntiSocialBehaviourTypeDto>> GetAsync()
        {
            var list = await _repository.Get<AntiSocialBehaviourType>().ToListAsync();
            return _mapper.Map<IList<AntiSocialBehaviourTypeDto>>(list);
        }

        public async Task<bool> AntiSocialBehaviourTypeExistsAsync(int id)
        {
            return await _repository.Get<AntiSocialBehaviourType>().AnyAsync(p => p.Id == id);
        }

        public async Task<AntiSocialBehaviourTypeDto> GetAsync(int id)
        {
            var alert = await _repository.GetAsync<AntiSocialBehaviourType>(id);

            return  _mapper.Map<AntiSocialBehaviourTypeDto>(alert);
        }

        public async Task<int> SaveAsync(AntiSocialBehaviourTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AntiSocialBehaviourTypeDto entityDto)
        {
            var entity = _mapper.Map<AntiSocialBehaviourType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _repository.GetAsync<AntiSocialBehaviourType>(id);

            if (alert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(alert);

            return result;
        }
    }
}
