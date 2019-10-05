using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Common;
using CRM.DAL.Repository;
using CRM.Dto.Common;
using CRM.WebAPI.Services.Interfaces.Common;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Common
{
    public class SmsService : ISmsService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SmsService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SmsDto>> GetAsync()
        {
            var list = await _repository.Get<Sms>().ToListAsync();
            return _mapper.Map<IList<SmsDto>>(list);
        }

        public async Task<bool> SmsExistsAsync(int id)
        {
            return await _repository.Get<Sms>().AnyAsync(p => p.Id == id);
        }

        public PageResult<SmsDto> GetAllAsync(ODataQueryOptions<Sms> options)
        {
                var items = _repository.Get<Sms>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Sms>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<SmsDto>>(items);
                return new PageResult<SmsDto>(result, null, count);
        }

        public async Task<SmsDto> GetAsync(int id)
        {
            var sms = await _repository.GetAsync<Sms>(id);

            return  _mapper.Map<SmsDto>(sms);
        }

        public async Task<int> SaveAsync(SmsDto sms)
        {
            var result = await SaveAndReturnEntityAsync(sms);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(SmsDto entityDto)
        {
            var smsDb = _mapper.Map<Sms>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(smsDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sms = await _repository.GetAsync<Sms>(id);

            if (sms == null)
            {
                return false;
            }


            var result = await _repository.HardDeleteAsync(sms);

            return result;
        }
    }
}
