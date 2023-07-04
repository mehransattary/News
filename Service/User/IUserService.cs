namespace Services;
using Entities.Models;
public interface IUserService
{
    Task<IEnumerable<User>> ShowUsersAsync();
    Task<User> FindUserByIdAsync(int id);

    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserByIdAsync(int id);
}