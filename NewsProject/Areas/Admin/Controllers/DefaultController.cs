using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NewsProject.Areas.Admin;
[Area("Admin")]
[Authorize("Admin")]
public class DefaultController:Controller
{

   public IActionResult Index()
   {
    return View();
   }
}