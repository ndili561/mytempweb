using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonContactDetailService : IPersonContactDetailService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonContactDetailService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonContactDetailDto>> GetAsync()
        {
            var list = await _repository.Get<PersonContactDetail>().ToListAsync();
            return _mapper.Map<IList<PersonContactDetailDto>>(list);
        }

        public async Task<bool> PersonContactDetailExistsAsync(int id)
        {
            return await _repository.Get<PersonContactDetail>().AnyAsync(p => p.Id == id);
        }

        public PageResult<PersonContactDetailDto> GetAllAsync(ODataQueryOptions<PersonContactDetail> options)
        {
                var items = _repository.Get<PersonContactDetail>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<PersonContactDetail>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<PersonContactDetailDto>>(items);
                return new PageResult<PersonContactDetailDto>(result, null, count);
        }

        public async Task<PersonContactDetailDto> GetAsync(int id)
        {
            var personContactDetail = await _repository.GetAsync<PersonContactDetail>(id);

            return  _mapper.Map<PersonContactDetailDto>(personContactDetail);
        }

        public async Task<int> SaveAsync(PersonContactDetailDto personContactDetail)
        {
            var result = await SaveAndReturnEntityAsync(personContactDetail);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonContactDetailDto entityDto)
        {
            var addressDb = _mapper.Map<PersonContactDetail>(entityDto);
            var persistedEntity = _repository.CRMContext.PersonContactDetails.AsNoTracking().FirstOrDefault(x => x.Id == addressDb.Id);
            addressDb.RowVersion = persistedEntity?.RowVersion;
            
            var result = await _repository.SaveAndReturnEntityAsync(addressDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personContactDetail = await _repository.GetAsync<PersonContactDetail>(id);

            if (personContactDetail == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personContactDetail);

            return result;
        }
    }
}
