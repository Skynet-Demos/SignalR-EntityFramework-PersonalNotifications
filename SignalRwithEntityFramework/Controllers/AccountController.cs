using Microsoft.AspNetCore.Mvc;
using SignalRwithEntityFramework.Repos;

namespace SignalRwithEntityFramework.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepo userRepo;

        public AccountController(UserRepo userRepo)
        {
            this.userRepo = userRepo;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var userFromDb = await userRepo.GetUserDetails(username, password);

            if(userFromDb == null)
            {
                ModelState.AddModelError("Login", "Invalid credentials");
                return View();
            }

            HttpContext.Session.SetString("Username", userFromDb.Username);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignOut()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction(nameof(SignIn));
        }
    }
}
