using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Repository;
using CRM.Dto.Customer;
using CRM.WebAPI.Services.Interfaces.Customer;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebAPI.Services.Customer
{
    public class MergePersonService : IMergePersonService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public MergePersonService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MergePersonDto>> GetAsync()
        {
            var list = await _repository.Get<MergePerson>().ToListAsync();
            return _mapper.Map<IList<MergePersonDto>>(list);
        }

        public async Task<bool> MergePersonExistsAsync(int id)
        {
            return await _repository.Get<MergePerson>().AnyAsync(p => p.Id == id);
        }

        public PageResult<MergePersonDto> GetAllAsync(ODataQueryOptions<MergePerson> options)
        {
                var items = _repository.Get<MergePerson>().AsQueryable();
                var count = items.Count();
                if (options.OrderBy != null)
                    items = options.OrderBy.ApplyTo(items);  //perform sort 
                if (options.Filter != null)
                    items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<MergePerson>();  //perform filter 

                count = items.ToList().Count;


                if (options.Skip != null)
                    items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
                if (options.Top != null)
                    items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
                var result = _mapper.Map<List<MergePersonDto>>(items);
                return new PageResult<MergePersonDto>(result, null, count);
        }

        public async Task<MergePersonDto> GetAsync(int id)
        {
            var mergePerson = await _repository.GetAsync<MergePerson>(id);

            return  _mapper.Map<MergePersonDto>(mergePerson);
        }

        public async Task<int> SaveAsync(MergePersonDto mergePerson)
        {
            var result = await SaveAndReturnEntityAsync(mergePerson);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(MergePersonDto entityDto)
        {
            var mergePersonDb = _mapper.Map<MergePerson>(entityDto);
            mergePersonDb = ValidateMergePerson(mergePersonDb);
            if (string.IsNullOrWhiteSpace(entityDto.ErrorMessage))
            {
                var result = await _repository.SaveAndReturnEntityAsync(mergePersonDb);
                return result;
            }
            return mergePersonDb;
        }
        private MergePerson ValidateMergePerson(MergePerson mergePerson)
        {
            if (mergePerson.CorrectPersonId == 0)
            {
                mergePerson.ErrorMessage = "The correct person Id cannot be zero.";
            }
            if (mergePerson.DuplicatePersonId == 0)
            {
                mergePerson.ErrorMessage = "The duplicate person Id cannot be zero.";
            }
            if (mergePerson.CorrectPersonId == mergePerson.DuplicatePersonId)
            {
                mergePerson.ErrorMessage = "The correct person Id cannot be equal to duplicate person.";
            }
            if (_repository.Get<MergePerson>().Any(x => x.CorrectPersonId == mergePerson.DuplicatePersonId))
            {
                mergePerson.ErrorMessage = "The duplicate person is already set as correct person.";
            }
            if (_repository.Get<MergePerson>().Any(x => x.CorrectPersonId == mergePerson.CorrectPersonId && x.DuplicatePersonId == mergePerson.DuplicatePersonId))
            {
                mergePerson.ErrorMessage = "The duplicate person is already set as duplicate to current person.";
            }
            return mergePerson;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mergePerson = await _repository.GetAsync<MergePerson>(id);

            if (mergePerson == null)
            {
                return false;
            }


            var result = await _repository.HardDeleteAsync(mergePerson);

            return result;
        }
    }
}
