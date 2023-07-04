namespace Services;

using Data.ViewModel;
using Entities.Models;
public interface IAccountService
{
    Task<bool> IsExistUser(LoginViewModel login);
    Task<string> GetRoleName(LoginViewModel login);
}