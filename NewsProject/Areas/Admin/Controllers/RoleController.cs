using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace NewsProject.Areas.Admin;
[Area("Admin")]
[Authorize("Admin")]
public class RoleController : Controller
{
    #region Constructor
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    #endregion
    #region Index
    public async Task<IActionResult> Index()
    {
        var res = await _roleService.ShowRolesAsync();
        return View(res);
    }
    #endregion
    #region Create
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Role Role)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("EnglishName", "لطفا ورودی را درست وارد نمائید.");
            return View(Role);
        }
        var res = await _roleService.AddRoleAsync(Role);
        if (res.Id!=0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(Role);
    }
    #endregion
    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        var res = await _roleService.FindRoleByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Role Role)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("EnglishName", "لطفا ورودی را درست وارد نمائید.");
            return View(Role);
        }
        var res = await _roleService.UpdateRoleAsync(Role);
        if (res.Id != 0)
        {
            return RedirectToAction(nameof(Index));
        }
       return View(Role);
    }
    #endregion
    #region Delete
    public async Task<IActionResult> Delete(int id)
    {

         var res = await _roleService.FindRoleByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(Role Role)
    {
        if (Role.Id==0)      
            return View(Role);
        
        var res = await _roleService.DeleteRoleByIdAsync(Role.Id);
        if (res)       
            return RedirectToAction(nameof(Index));
        
       return View(Role);
    }
    #endregion

}