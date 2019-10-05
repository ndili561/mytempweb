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
    public class FlagTypeService : IFlagTypeService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public FlagTypeService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FlagTypeDto>> GetAsync()
        {
            var list = await _repository.Get<FlagType>().ToListAsync();
            return _mapper.Map<IList<FlagTypeDto>>(list);
        }

        public async Task<bool> FlagTypeExistsAsync(int id)
        {
            return await _repository.Get<FlagType>().AnyAsync(p => p.Id == id);
        }

        public async Task<FlagTypeDto> GetAsync(int id)
        {
            var Flag = await _repository.GetAsync<FlagType>(id);

            return  _mapper.Map<FlagTypeDto>(Flag);
        }

        public async Task<int> SaveAsync(FlagTypeDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(FlagTypeDto entityDto)
        {
            var entity = _mapper.Map<FlagType>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(entity);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var flag = await _repository.GetAsync<FlagType>(id);

            if (flag == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(flag);

            return result;
        }
    }
}
