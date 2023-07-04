namespace Services;

using System.Text.Json;
using Data.Context;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class RoleService : IRoleService
{
    private readonly AppDbContext _context;
    private readonly ILogService _logService;

    public RoleService(AppDbContext context, ILogService log)
    {
        _context = context;
        _logService = log;

    }
    public async Task<Role> FindRoleByIdAsync(int id)
    {
        try
        {
            var res = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (res != null)
                return res;
            else
                return new Role();
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "RoleService.FindRoleByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
         _logService.AddLog(log, "log_Role_Service.json");
            throw;
        }
    }
    public async Task<IEnumerable<Role>> ShowRolesAsync()
    {
        try
        {
            var result = await _context.Roles.ToListAsync();
            return result;
        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "RoleService.ShowRoles",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Role_Service.json");
            throw;
        }

    }
    public async Task<Role> AddRoleAsync(Role Role)
    {
        try
        {
            Role.CreateDate = DateTime.Now;
            Role.UpdateDate = Role.CreateDate;
            await _context.Roles.AddAsync(Role);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Role;
            }
            else
            {
                return new Role();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "RoleService.AddRoleAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
             _logService.AddLog(log, "log_Role_Service.json");
            throw;
        }
    }
    public async Task<Role> UpdateRoleAsync(Role Role)
    {
        try
        {
            Role.CreateDate = Role.CreateDate;
            Role.UpdateDate = DateTime.Now;
            _context.Roles.Update(Role);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Role;
            }
            else
            {
                return new Role();
            }

        }
        catch (System.Exception ex)
        {
            var log = new Log()
            {
                Text = ex.Message,
                Service = "RoleService.UpdateRoleAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Role_Service.json");
            throw;
        }
    }

    public async Task<bool> DeleteRoleByIdAsync(int id)
    {
        try
        {
            var Role = await FindRoleByIdAsync(id);
            _context.Roles.Remove(Role);
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
                Service = "RoleService.DeleteRoleByIdAsync",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
            _logService.AddLog(log, "log_Role_Service.json");
            return false;
        }
    }


}