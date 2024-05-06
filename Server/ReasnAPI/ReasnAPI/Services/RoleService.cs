using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services
{
    public class RoleService(ReasnContext context)
    {
        private readonly ReasnContext _context = context;

        public RoleDto? CreateRole(RoleDto? roleDto)
        {
            if (roleDto is null)
            {
                return null;
            }

            // check if role with the same name already exists
            var roleDb = _context.Roles.FirstOrDefault(r => r.Name == roleDto.Name);

            if (roleDb is not null)
            {
                return null;
            }

            var role = new Role
            {
                Name = roleDto.Name,
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return roleDto;
        }

        public RoleDto? UpdateRole(int roleId, RoleDto? roleDto)
        {
            if (roleDto is null)
            {
                return null;
            }

            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role is null)
            {
                return null;
            }

            role.Name = roleDto.Name;

            _context.SaveChanges();

            return MapToRoleDto(role);
        }

        public bool DeleteRole(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role is null)
            {
                return false;
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return true;
        }

        public RoleDto? GetRoleById(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role is null)
            {
                return null;
            }

            return MapToRoleDto(role);
        }

        public IEnumerable<RoleDto?> GetRolesByFilter(Expression<Func<Role, bool>> filter)
        {
            return _context.Roles
                           .Where(filter)
                           .Select(role => MapToRoleDto(role))
                           .AsEnumerable();
        }

        public IEnumerable<RoleDto?> GetAllRoles()
        {
            return _context.Roles
                           .Select(role => MapToRoleDto(role))
                           .AsEnumerable();
        }

        private static RoleDto MapToRoleDto(Role role)
        {
            return new RoleDto
            {
                Name = role.Name
            };
        }
    }
}
