namespace Services;

using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
public class UserService : IUserService
{
    private readonly AppDbContext _context;
        private readonly ILogService _logService;

    public UserService(AppDbContext context,ILogService logService)
    {
        _context = context;
        _logService=logService;
    }
    public async Task<User> FindUserByIdAsync(int id)
    {
        try
        {
            var res = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
                return res;
            else
                return new User();
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "UserService.FindUserByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_User_Service.json");
            throw;
        }
    }
    public async Task<IEnumerable<User>> ShowUsersAsync()
    {
        try
        {
            var result = await _context.Users.Include(x => x.Role).Select(x => new User()
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Mobile = x.Mobile,
                Role = new Role() { PersianName = x.Role.PersianName }
            }).ToListAsync();
            return result;
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "UserService.ShowUsers",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
             _logService.AddLog(log, "log_User_Service.json");
            throw;
        }

    }
    public async Task<User> AddUserAsync(User User)
    {
        try
        {
            User.CreateDate = DateTime.Now;
            User.UpdateDate = User.CreateDate;
            await _context.Users.AddAsync(User);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return User;
            }
            else
            {
                return new User();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "UserService.AddUserAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
      _logService.AddLog(log, "log_User_Service.json");
            throw;
        }
    }
    public async Task<User> UpdateUserAsync(User User)
    {
        try
        {
            User.CreateDate = User.CreateDate;
            User.UpdateDate = DateTime.Now;
            _context.Users.Update(User);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return User;
            }
            else
            {
                return new User();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "UserService.UpdateUserAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
        _logService.AddLog(log, "log_User_Service.json");
            throw;
        }
    }

    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        try
        {
            var User = await FindUserByIdAsync(id);
            _context.Users.Remove(User);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
                return true;
            else
                return false;


        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "UserService.DeleteUserByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
          _logService.AddLog(log, "log_User_Service.json");
            throw;
        }
    }


}