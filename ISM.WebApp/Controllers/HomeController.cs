using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountDAO _accountDAO;

        public HomeController(ILogger<HomeController> logger, AccountDAO accountDAO)
        {
            _logger = logger;
            _accountDAO = accountDAO;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> Login(string txtAccount, string txtPassword)
        {
            Account newAccount = new Account();
            var accounts = _accountDAO.GetAccounts();
            foreach (Account account in accounts)
            {
                if ((txtAccount.ToLower() == account.username.ToLower()) && (txtPassword == account.password))
                {
                    newAccount = account;
                }
            }

            if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_id == 2) && (newAccount.status == true))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, txtAccount)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                return View("Views/Staff/Homepage/StaffHomepage.cshtml");
            }
            else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_id == 1) && (newAccount.status == true))
            {
                 var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, txtAccount)
                 };
                 var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                 var principal = new ClaimsPrincipal(identity);
                 var props = new AuthenticationProperties();
                 await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                 return View("Views/Admin/Homepage/AdminHomepage.cshtml");
            }
            else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_id == 3) && (newAccount.status == true))
            {
                 var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, txtAccount)
                 };
                 var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                 var principal = new ClaimsPrincipal(identity);
                 var props = new AuthenticationProperties();
                 await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                 return View("Views/Degree/Homepage/DegreeHomepage.cshtml");
            }
            else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_id == 4) && (newAccount.status == true))
            {
                 var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, txtAccount)
                 };
                 var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                 var principal = new ClaimsPrincipal(identity);
                 var props = new AuthenticationProperties();
                 await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                 return View("Views/Mobility/Homepage/MobilityHomepage.cshtml");
            }
            else
            {
                return View();
            }
        }

        [HttpPost, ActionName("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
