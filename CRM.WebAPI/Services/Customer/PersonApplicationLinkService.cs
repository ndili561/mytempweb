using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using PersonApplicationLink = CRM.DAL.Database.Entities.Customer.PersonApplicationLink;

namespace CRM.WebAPI.Services.Customer
{
    public class PersonApplicationLinkService : IPersonApplicationLinkService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PersonApplicationLinkService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonApplicationLinkDto>> GetAsync()
        {
            var list = await _repository.Get<PersonApplicationLink>().ToListAsync();
            return _mapper.Map<IList<PersonApplicationLinkDto>>(list);
        }

        public async Task<bool> PersonApplicationLinkExistsAsync(int id)
        {
            return await _repository.Get<PersonApplicationLink>().AnyAsync(p => p.Id == id);
        }

        public PageResult<PersonApplicationLinkDto> GetAllAsync(ODataQueryOptions<PersonApplicationLink> options)
        {
                var items = _repository.Get<PersonApplicationLink>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<PersonApplicationLink>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<PersonApplicationLinkDto>>(items);
                return new PageResult<PersonApplicationLinkDto>(result, null, count);
        }

        public async Task<PersonApplicationLinkDto> GetAsync(int id)
        {
            var personApplicationLink = await _repository.GetAsync<PersonApplicationLink>(id);

            return  _mapper.Map<PersonApplicationLinkDto>(personApplicationLink);
        }

        public async Task<int> SaveAsync(PersonApplicationLinkDto personApplicationLink)
        {
            var result = await SaveAndReturnEntityAsync(personApplicationLink);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(PersonApplicationLinkDto entityDto)
        {
            var addressDb = _mapper.Map<PersonApplicationLink>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(addressDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var personApplicationLink = await _repository.GetAsync<PersonApplicationLink>(id);

            if (personApplicationLink == null)
            {
                return false;
            }

            var result = await _repository.HardDeleteAsync(personApplicationLink);

            return result;
        }
    }
}
