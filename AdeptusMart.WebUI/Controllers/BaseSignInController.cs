using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdeptusMart05.WebUI.Controllers
{
    public class BaseSignInController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetString("UserId");

            var userIdStr = HttpContext.Session.GetString("UserId");

            Console.WriteLine("Session UserId: " + userIdStr);

            var controller = context.Controller as Controller;

            if (string.IsNullOrEmpty(userId))
            {
                if(controller != null)
                {
                    controller.ViewBag.IsLoginSuccess = false;
                }
                
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            if (controller != null)
            {
                controller.ViewBag.IsLoginSuccess = true;
                controller.ViewBag.UserId = userId;
            }
                

            base.OnActionExecuting(context);
        }
    }
}
