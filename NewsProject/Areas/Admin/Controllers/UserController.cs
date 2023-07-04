using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace NewsProject.Areas.Admin;
[Area("Admin")]
[Authorize("Admin")]
public class UserController : Controller
{
    #region Constructor
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;

    }
    #endregion
    #region Index
    public async Task<IActionResult> Index()
    {
        var res = await _userService.ShowUsersAsync();
        return View(res);
    }
    #endregion
    #region Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Roles = new SelectList(await _roleService.ShowRolesAsync(), "Id", "PersianName");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = new SelectList(await _roleService.ShowRolesAsync(), "Id", "PersianName");

            ModelState.AddModelError("Name", "لطفا ورودی را درست وارد نمائید.");
            return View(user);
        }
        var res = await _userService.AddUserAsync(user);
        if (res.Id != 0)
            return RedirectToAction(nameof(Index));

        ViewBag.Roles = new SelectList(await _roleService.ShowRolesAsync(),"Id", "PersianName");
        return View(User);
    }
    #endregion
    #region Edit
    public async Task<IActionResult> Edit(int id)
    {
        var res = await _userService.FindUserByIdAsync(id);
        ViewBag.Roles = new SelectList(await _roleService.ShowRolesAsync(), "Id", "PersianName", res.RoleId);

        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = new SelectList(await _roleService.ShowRolesAsync(), "Id", "PersianName", user.RoleId);

            ModelState.AddModelError("Name", "لطفا ورودی را درست وارد نمائید.");
            return View(user);
        }
        var res = await _userService.UpdateUserAsync(user);
        if (res.Id != 0)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Roles = new SelectList(await _roleService.ShowRolesAsync(), "Id", "PersianName", user.RoleId);
        return View(user);
    }
    #endregion
    #region Delete
    public async Task<IActionResult> Delete(int id)
    {
        var res = await _userService.FindUserByIdAsync(id);
        return View(res);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(User User)
    {
        if (User.Id == 0)
            return View(User);

        var res = await _userService.DeleteUserByIdAsync(User.Id);
        if (res)
            return RedirectToAction(nameof(Index));

        return View(User);
    }
    #endregion

}