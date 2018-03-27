using System;
using CogsLite.Core;
using Microsoft.AspNetCore.Mvc;

namespace CogsLite3.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserStore _userStore;

        public LoginController(IUserStore userStore)
        {
            _userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        public IActionResult Index(string message)
        {
            ViewData["Message"] = message;
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _userStore.Get(email);

            if (user != null && user.Password == password)
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Index", new { message = "Unable to logon" });
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string email, string displayName, string password)
        {
            User newUser = new User
            {
                EmailAddress = email,
                DisplayName = displayName,
                Password = password
            };
            _userStore.Add(newUser);
            return View();
        }
    }
}