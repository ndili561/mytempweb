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
using Address = CRM.DAL.Database.Entities.Customer.Address;

namespace CRM.WebAPI.Services.Customer
{
    public class AddressService : IAddressService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> GetAsync()
        {
            var list = await _repository.Get<Address>().ToListAsync();
            return _mapper.Map<IList<AddressDto>>(list);
        }

        public async Task<bool> AddressExistsAsync(int id)
        {
            return await _repository.Get<Address>().AnyAsync(p => p.Id == id);
        }

        public PageResult<AddressDto> GetAllAsync(ODataQueryOptions<Address> options)
        {
                var items = _repository.Get<Address>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<Address>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<AddressDto>>(items);
                return new PageResult<AddressDto>(result, null, count);
        }

        public async Task<AddressDto> GetAsync(int id)
        {
            var address = await _repository.GetAsync<Address>(id);

            return  _mapper.Map<AddressDto>(address);
        }

        public async Task<int> SaveAsync(AddressDto address)
        {
            var result = await SaveAndReturnEntityAsync(address);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(AddressDto entityDto)
        {
            var addressDb = _mapper.Map<Address>(entityDto);
            var result = await _repository.SaveAndReturnEntityAsync(addressDb);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var address = await _repository.GetAsync<Address>(id);

            if (address == null)
            {
                return false;
            }


            var result = await _repository.HardDeleteAsync(address);

            return result;
        }
    }
}
