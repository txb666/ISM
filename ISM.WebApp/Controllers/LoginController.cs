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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ISM.WebApp.Constant;

namespace ISM.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AccountDAO _accountDAO;
        public LoginController(ILogger<LoginController> logger, AccountDAO accountDAO)
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
            if (string.IsNullOrEmpty(txtAccount) || string.IsNullOrEmpty(txtPassword))
            {
                return RedirectToAction("Index");
            }
            Account newAccount = new Account();
            var account = _accountDAO.GetAccount(txtAccount,txtPassword);
            newAccount = account;
            newAccount.haveDegree = _accountDAO.haveDegree(newAccount.user_id);
            newAccount.totalNotification = _accountDAO.GetTotalNotification(newAccount.user_id);
            newAccount.webNotifications = _accountDAO.GetWebNotifications(newAccount.user_id);

            if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_name.Equals("Admin")) && (newAccount.status == true))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, txtAccount),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
                return View("Views/Admin/Homepage/AdminHomepage.cshtml");
            }
            else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_name.Equals("Staff")) && (newAccount.status == true))
            {
                var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, txtAccount),
                     new Claim(ClaimTypes.Role, "Staff")
                 };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
                return View("Views/Admin/Homepage/AdminHomepage.cshtml");
            }
            else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_name.Equals("Degree")) && (newAccount.status == true))
            {
                var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, txtAccount),
                     new Claim(ClaimTypes.Role, "Degree")
                 };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
                return View("Views/Admin/Homepage/AdminHomepage.cshtml");
            }
            else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
                && (newAccount.role_name.Equals("Mobility")) && (newAccount.status == true))
            {
                var claims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, txtAccount),
                     new Claim(ClaimTypes.Role, "Mobility")
                 };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
                return View("Views/Admin/Homepage/AdminHomepage.cshtml");
            }
            else
            {
                return View();
            }
        }

        //[HttpPost, ActionName("Index")]
        //public async Task<IActionResult> Login(string txtAccount, string txtPassword)
        //{
        //    if (string.IsNullOrEmpty(txtAccount) || string.IsNullOrEmpty(txtPassword))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    Account newAccount = new Account();
        //    var accounts = _accountDAO.GetAccounts();
        //    foreach (Account account in accounts)
        //    {
        //        if ((txtAccount.ToLower() == account.username.ToLower()) && (txtPassword == account.password))
        //        {
        //            newAccount = account;
        //            newAccount.haveDegree = _accountDAO.haveDegree(newAccount.user_id);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }

        //    if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
        //        && (newAccount.role_name.Equals("Admin")) && (newAccount.status == true))
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, txtAccount),
        //            new Claim(ClaimTypes.Role, "Admin")
        //        };
        //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(identity);
        //        var props = new AuthenticationProperties();
        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        //        HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
        //        return View("Views/Admin/Homepage/AdminHomepage.cshtml");
        //    }
        //    else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
        //        && (newAccount.role_name.Equals("Staff")) && (newAccount.status == true))
        //    {
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name, txtAccount),
        //             new Claim(ClaimTypes.Role, "Staff")
        //         };
        //         var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //         var principal = new ClaimsPrincipal(identity);
        //         var props = new AuthenticationProperties();
        //         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        //         HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
        //         return View("Views/Staff/Homepage/StaffHomepage.cshtml");
        //    }
        //    else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
        //        && (newAccount.role_name.Equals("Degree")) && (newAccount.status == true))
        //    {
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name, txtAccount),
        //             new Claim(ClaimTypes.Role, "Degree")
        //         };
        //         var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //         var principal = new ClaimsPrincipal(identity);
        //         var props = new AuthenticationProperties();
        //         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        //         HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
        //         return View("Views/Degree/Homepage/DegreeHomepage.cshtml");
        //    }
        //    else if ((txtAccount.ToLower() == newAccount.username) && (txtPassword == newAccount.password)
        //        && (newAccount.role_name.Equals("Mobility")) && (newAccount.status == true))
        //    {
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name, txtAccount),
        //             new Claim(ClaimTypes.Role, "Mobility")
        //         };
        //         var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //         var principal = new ClaimsPrincipal(identity);
        //         var props = new AuthenticationProperties();
        //         await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        //         HttpContext.Session.SetString(LoginConst.SessionKeyName, JsonConvert.SerializeObject(newAccount));
        //         return View("Views/Mobility/Homepage/MobilityHomepage.cshtml");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [HttpPost, ActionName("Logout")]
        public async Task<IActionResult> Logout()
        {
            foreach (var item in Request.Cookies.Keys)
            {
                if (item == "ISMSession")
                {
                    Response.Cookies.Delete(item);
                }
            }
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        public bool AccountIsActive(string username, string password)
        {
            bool result = _accountDAO.checkAccountInactive(username, password);
            return result;
        }

        public bool AccountIsExist(string username, string password)
        {
            bool result = _accountDAO.checkLogin(username, password);
            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}