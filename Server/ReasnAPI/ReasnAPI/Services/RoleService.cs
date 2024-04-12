using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class RoleService(ReasnContext context) {
        private readonly ReasnContext _context = context;

        public RoleDto? CreateRole(RoleDto roleDto) {
            if (roleDto is null)
                return null;

            var role = new Role() {
                Name = roleDto.Name,
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return roleDto;
        }

        public RoleDto? UpdateRole(int roleId, RoleDto roleDto) {
            if (roleDto is null)
                return null;

            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role is null) 
                return null;

            role.Name = roleDto.Name;

            _context.SaveChanges();

            return MapToRoleDto(role);
        }

        public void DeleteRole(int roleId) {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role is not null) {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public RoleDto? GetRoleById(int roleId) {
            return MapToRoleDto(_context.Roles.FirstOrDefault(r => r.Id == roleId));
        }

        public IEnumerable<RoleDto?> GetRolesByFilter(Expression<Func<Role, bool>> filter) {
            return _context.Roles
                           .Where(filter)
                           .Select(role => MapToRoleDto(role))
                           .ToList();
        }

        public IEnumerable<RoleDto?> GetAllRoles() {
            return _context.Roles
                           .Select(role => MapToRoleDto(role))
                           .ToList();
        }

        private static RoleDto? MapToRoleDto(Role role) {
            if (role is null)
                return null;

            return new RoleDto {
                Name = role.Name
            };
        }
    }
}
