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
    public class AntiSocialBehaviourCaseStatusService : IAntiSocialBehaviourCaseStatusService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AntiSocialBehaviourCaseStatusService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AntiSocialBehaviourCaseStatusDto>> GetAsync()
        {
            var list = await _repository.Get<AntiSocialBehaviourCaseStatus>().ToListAsync();
            return _mapper.Map<IList<AntiSocialBehaviourCaseStatusDto>>(list);
        }

        public async Task<bool> AntiSocialBehaviourCaseStatusExistsAsync(int id)
        {
            return await _repository.Get<AntiSocialBehaviourCaseStatus>().AnyAsync(p => p.Id == id);
        }

        public async Task<AntiSocialBehaviourCaseStatusDto> GetAsync(int id)
        {
            var alert = await _repository.GetAsync<AntiSocialBehaviourCaseStatus>(id);

            return  _mapper.Map<AntiSocialBehaviourCaseStatusDto>(alert);
        }

        public async Task<int> SaveAsync(AntiSocialBehaviourCaseStatusDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AntiSocialBehaviourCaseStatusDto entityDto)
        {
            var entity = _mapper.Map<AntiSocialBehaviourCaseStatus>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _repository.GetAsync<AntiSocialBehaviourCaseStatus>(id);

            if (alert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(alert);

            return result;
        }
    }
}
