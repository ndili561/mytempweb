using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Repository;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using User = CRM.DAL.Database.Entities.Employee.User;
namespace CRM.WebAPI.Services.Employee
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAsync()
        {
            var list = await _repository.Get<User>().ToListAsync();
            return _mapper.Map<IList<UserDto>>(list);
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _repository.Get<User>().AnyAsync(p => p.Id == id);
        }

        public PageResult<UserDto> GetAllAsync(ODataQueryOptions<User> options)
        {
            var items = _repository.Get<User>().Include(x => x.Applications).AsQueryable();
            var count = 0;
            if (options.OrderBy != null)
                items = options.OrderBy.ApplyTo(items);  //perform sort 
            if (options.Filter != null)
                items = options.Filter.ApplyTo(items, new ODataQuerySettings()).Cast<User>();  //perform filter 

            count = items.ToList().Count;


            if (options.Skip != null)
                items = options.Skip.ApplyTo(items, new ODataQuerySettings());  //perform skip 
            if (options.Top != null)
                items = options.Top.ApplyTo(items, new ODataQuerySettings());  //perform take 
            var result = _mapper.Map<List<UserDto>>(items);
            return new PageResult<UserDto>(result, null, count);
        }

        public async Task<UserDto> UpdatePatch(int id, Delta<User> userPatch)
        {
            var user = await _repository.GetAsync<User>(id);
            if (user == null)
            {
                var model = new UserDto { ErrorMessage = "The user does not exist." };
                return model;
            }
            userPatch.CopyChangedValues(user);
            var result = await _repository.SaveAndReturnEntityAsync(user);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _repository.Get<User>()
                        .Include(x => x.Applications)
                        .Include(x => x.Tasks)
                        .FirstOrDefaultAsync(x => x.Id == id);
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task<int> SaveAsync(UserDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(UserDto entityDto)
        {
            var entity = _mapper.Map<User>(entityDto);
            if (entity.Id > 0)
            {
                if (!entity.Applications.Any()&& !entity.Roles.Any())
                {
                    entity = await _repository.SaveAndReturnEntityAsync(entity);
                }
                if (entity.Roles.Any())
                {
                    await UpdateUserApplicationRoleLink(entityDto, entity);
                }
                if (entity.Applications.Any())
                {
                    await UpdateUserApplicationLink(entityDto, entity);
                }
            }
            else
            {
                entity = await _repository.SaveAndReturnEntityAsync(entity);
            }

            return entity;
        }

        private async Task UpdateUserApplicationLink(UserDto entityDto, User entity)
        {
            var persistedEntity = _repository.CRMContext.Users.Include(x => x.Applications).AsNoTracking()
                .FirstOrDefault(x => x.Id == entityDto.Id);
            var persistedLinkedApplicationIds = persistedEntity.Applications.Select(x => x.ApplicationId).ToList();
            var currentLinkedApplicationIds = entityDto.Applications.Select(x => x.ApplicationId).ToList();
            var applicationLinkAdded = entity.Applications.Where(x => !persistedLinkedApplicationIds.Contains(x.ApplicationId)).ToList();
            var applicationLinkRemoved = persistedEntity.Applications.Where(x => !currentLinkedApplicationIds.Contains(x.ApplicationId))
                .ToList();

            foreach (var applicationUser in applicationLinkAdded)
            {
                await _repository.SaveAndReturnEntityAsync(applicationUser);
            }
            foreach (var applicationUser in applicationLinkRemoved.Where(x => x.ApplicationId > 0))
            {
                await _repository.HardDeleteAsync(applicationUser);
            }
        }
        private async Task UpdateUserApplicationRoleLink(UserDto entityDto, User entity)
        {
            var persistedEntity = _repository.CRMContext.Users.Include(x => x.Roles).ThenInclude(x => x.Role).AsNoTracking()
                .FirstOrDefault(x => x.Id == entityDto.Id);
            var persistedLinkedApplicationRoleIds = persistedEntity.Roles.Select(x => x.ApplicationRoleId).ToList();
            var currentLinkedApplicationRoleIds = entityDto.Roles.Select(x => x.Id).ToList();
            var applicationUserRoleLinkAdded = entity.Roles.Where(x => !persistedLinkedApplicationRoleIds.Contains(x.Id)).ToList();
            var applicationUserRoleLinkRemoved = persistedEntity.Roles.Where(x => !currentLinkedApplicationRoleIds.Contains(x.Id))
                .ToList();

            foreach (var applicationUserRole in applicationUserRoleLinkAdded)
            {
                await _repository.SaveAndReturnEntityAsync(applicationUserRole);
            }
            foreach (var applicationUserRole in applicationUserRoleLinkRemoved.Where(x => x.ApplicationRoleId > 0))
            {
                await _repository.HardDeleteAsync(applicationUserRole);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetAsync<User>(id);

            if (user == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(user);
            return result;
        }
    }
}
