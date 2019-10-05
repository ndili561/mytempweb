using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRM.DAL.Database.Entities;
using CRM.DAL.Database.Entities.Employee;
using CRM.DAL.Repository;
using CRM.Dto.Employee;
using CRM.WebAPI.Services.Interfaces.Employee;
using Microsoft.EntityFrameworkCore;
using Role = CRM.DAL.Database.Entities.Employee.Role;
using Task = System.Threading.Tasks.Task;

namespace CRM.WebAPI.Services.Employee
{
    public class RoleService : IRoleService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAsync()
        {
            var list = await _repository.Get<Role>().ToListAsync();
            return _mapper.Map<IList<RoleDto>>(list);
        }

        public async Task<bool> RoleExistsAsync(int id)
        {
            return await _repository.Get<Role>().AnyAsync(p => p.Id == id);
        }


        public async Task<RoleDto> GetAsync(int id)
        {
            var role = await _repository.CRMContext.Roles.Include(x=>x.ApplicationRoles).ThenInclude(a=>a.RolePermissions).FirstOrDefaultAsync(x=>x.Id==id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<int> SaveAsync(RoleDto entityDto)
        {
            var result = await SaveAndReturnEntityAsync(entityDto);
            return result.Id;
        }

        public async Task<BaseEntity> SaveAndReturnEntityAsync(RoleDto entityDto)
        {
            var entity = _mapper.Map<Role>(entityDto);
            if (entity.ApplicationRoles != null && entity.ApplicationRoles.Any())
            {
                await SaveRolePermission(entityDto);
            }
            else
            {
                entity = await _repository.SaveAndReturnEntityAsync(entity);
            }
            entity.SuccessMessage = "The permission is updated successfully.";
            return entity;
        }

        private async Task SaveRolePermission(RoleDto entityDto)
        {
            foreach (var entityApplicationRole in entityDto.ApplicationRoles)
            {
                var applicationRole = _repository.CRMContext.ApplicationRoles
                    .Include(x => x.RolePermissions)
                    .FirstOrDefault(x => x.ApplicationId == entityApplicationRole.ApplicationId
                                        && x.RoleId == entityApplicationRole.RoleId) ??
                await _repository.SaveAndReturnEntityAsync(
                    new ApplicationRole { ApplicationId = entityDto.ApplicationId, RoleId = entityDto.Id });
                entityApplicationRole.Id = applicationRole.Id;
                foreach (var rolePermission in entityApplicationRole.RolePermissions)
                {
                    rolePermission.ApplicationRoleId = applicationRole.Id;
                }
                await UpdateRolePermission(entityApplicationRole);
            }

        }

        private async Task UpdateRolePermission(ApplicationRoleDto entity)
        {
            var persistedEntity = _repository.CRMContext.ApplicationRoles.Include(x => x.RolePermissions).AsNoTracking()
                .FirstOrDefault(x => x.Id == entity.Id);
            var persistedPermissionIds = persistedEntity.RolePermissions.Select(x => x.MenuItemId).ToList();
            var currentPermissionIds = entity.RolePermissions.Select(x => x.MenuItemId).ToList();
            var permissionAdded =  _mapper.Map<List<Permission>>(entity.RolePermissions.Where(x => !persistedPermissionIds.Contains(x.MenuItemId)).ToList());
            var permissionRemoved = _mapper.Map<List<Permission>>(persistedEntity.RolePermissions.Where(x => !currentPermissionIds.Contains(x.MenuItemId))
                .ToList());

            foreach (var rolePermission in permissionAdded)
            {

                await _repository.SaveAndReturnEntityAsync(rolePermission);
            }
            foreach (var rolePermission in permissionRemoved.Where(x => x.MenuItemId > 0))
            {
                try
                {
                    var permission = _repository.CRMContext.Permissions.FirstOrDefault(x => x.Id == rolePermission.Id);
                    await _repository.HardDeleteAsync(permission);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _repository.GetAsync<Role>(id);

            if (role == null)
            {
                return false;
            }
            var result = await _repository.HardDeleteAsync(role);

            return result;
        }
    }
}
