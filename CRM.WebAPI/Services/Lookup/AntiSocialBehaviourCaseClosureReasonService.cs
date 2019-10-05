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
    public class AntiSocialBehaviourCaseClosureReasonService : IAntiSocialBehaviourCaseClosureReasonService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AntiSocialBehaviourCaseClosureReasonService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AntiSocialBehaviourCaseClosureReasonDto>> GetAsync()
        {
            var list = await _repository.Get<AntiSocialBehaviourCaseClosureReason>().ToListAsync();
            return _mapper.Map<IList<AntiSocialBehaviourCaseClosureReasonDto>>(list);
        }

        public async Task<bool> AntiSocialBehaviourCaseClosureReasonExistsAsync(int id)
        {
            return await _repository.Get<AntiSocialBehaviourCaseClosureReason>().AnyAsync(p => p.Id == id);
        }

        public async Task<AntiSocialBehaviourCaseClosureReasonDto> GetAsync(int id)
        {
            var alert = await _repository.GetAsync<AntiSocialBehaviourCaseClosureReason>(id);

            return  _mapper.Map<AntiSocialBehaviourCaseClosureReasonDto>(alert);
        }

        public async Task<int> SaveAsync(AntiSocialBehaviourCaseClosureReasonDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AntiSocialBehaviourCaseClosureReasonDto entityDto)
        {
            var entity = _mapper.Map<AntiSocialBehaviourCaseClosureReason>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alert = await _repository.GetAsync<AntiSocialBehaviourCaseClosureReason>(id);

            if (alert == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(alert);

            return result;
        }
    }
}
