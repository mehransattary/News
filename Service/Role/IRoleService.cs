namespace Services;
using Entities.Models;
public interface IRoleService
{
    Task<IEnumerable<Role>> ShowRolesAsync();
    Task<Role> FindRoleByIdAsync(int id);

    Task<Role> AddRoleAsync(Role Role);
    Task<Role> UpdateRoleAsync(Role Role);
    Task<bool> DeleteRoleByIdAsync(int id);
}