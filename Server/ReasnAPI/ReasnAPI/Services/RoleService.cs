using ReasnAPI.Models.Database;
using ReasnAPI.Models.DTOs;
using System.Linq.Expressions;

namespace ReasnAPI.Services {
    public class RoleService(ReasnContext context) {
        private readonly ReasnContext _context = context;

        public RoleDto CreateRole(RoleDto roleDto) {
            var role = new Role() {
                Name = roleDto.Name,
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return roleDto;
        }

        public RoleDto UpdateRole(int roleId, RoleDto roleDto) {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role == null) {
                return null;
            }

            role.Name = roleDto.Name;

            _context.SaveChanges();

            return MapToRoleDto(role);
        }

        public void DeleteRole(int roleId) {
            var role = _context.Roles.FirstOrDefault(r => r.Id == roleId);

            if (role != null) {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public RoleDto GetRoleById(int roleId) {
            return MapToRoleDto(_context.Roles.FirstOrDefault(r => r.Id == roleId));
        }

        public List<RoleDto> GetRolesByFilter(Expression<Func<Role, bool>> filter) {
            return _context.Roles.Where(filter).Select(role => MapToRoleDto(role)).ToList();
        }

        public List<RoleDto> GetAllRoles() {
            return _context.Roles.Select(role => MapToRoleDto(role)).ToList();
        }

        private static RoleDto MapToRoleDto(Role role) {
            return new RoleDto {
                Name = role.Name
            };
        }
    }
}
