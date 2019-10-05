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
    public class FlagGroupService : IFlagGroupService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public FlagGroupService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FlagGroupDto>> GetAsync()
        {
            var list = await _repository.Get<FlagGroup>().ToListAsync();
            return _mapper.Map<IList<FlagGroupDto>>(list);
        }

        public async Task<bool> FlagGroupExistsAsync(int id)
        {
            return await _repository.Get<FlagGroup>().AnyAsync(p => p.Id == id);
        }

        public async Task<FlagGroupDto> GetAsync(int id)
        {
            var flag = await _repository.GetAsync<FlagGroup>(id);

            return  _mapper.Map<FlagGroupDto>(flag);
        }

        public async Task<int> SaveAsync(FlagGroupDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(FlagGroupDto entityDto)
        {
            var entity = _mapper.Map<FlagGroup>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var Flag = await _repository.GetAsync<FlagGroup>(id);

            if (Flag == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(Flag);

            return result;
        }
    }
}
