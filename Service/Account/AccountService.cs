namespace Services;

using System.Text.Json;
using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Data.ViewModel;
public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public AccountService(AppDbContext context, ILogService logService)
    {
        _context = context;
        _logService = logService;
    }
    public async Task<bool> IsExistUser(LoginViewModel login)
    {
        try
        {
            return await _context.Users.AnyAsync(x => x.Mobile == login.Mobile && x.Password == login.Password);
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "AccountService.DeleteUserByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Account_Service.json");
            throw;

        }
    }

    public async Task<string> GetRoleName(LoginViewModel login)
    {
        var user =await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Mobile == login.Mobile);
        return user?.Role?.EnglishName;
    }


}