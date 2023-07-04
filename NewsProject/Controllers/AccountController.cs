using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NewsProject.Models;
using Data.ViewModel;
using Services;

namespace NewsProject.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginmodel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(loginmodel.Mobile, "موبایل و رمز عبور را وارد نمائید.");

            return View(loginmodel);
        }
        if (await _accountService.IsExistUser(loginmodel))
        {
            string roleName = await _accountService.GetRoleName(loginmodel);
            #region Authentication          

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.MobilePhone, loginmodel.Mobile));
            claims.Add(new Claim(ClaimTypes.Name, loginmodel.Mobile));
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            //ClaimIdentity
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //ClaimsPrincipal
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            //SignIn
            await HttpContext.SignInAsync(claimsPrincipal);

            #endregion
            if (roleName == "Admin")
                return Redirect("/Admin/Default/index");

            else if (roleName == "User")
                return Redirect("/Admin/Category/index");

        }
        ModelState.AddModelError(loginmodel.Mobile, "نام کاربری یا رمز عبور صحیح نمی باشد!");
        return View(loginmodel);

    }


    public IActionResult AccessDenied()
    {
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return Redirect("/");
    }

}
