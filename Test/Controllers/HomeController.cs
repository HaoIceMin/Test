using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Models;
using Test.Models.Database.master;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly masterContext _masterContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, masterContext masterContext)
        {
            _logger = logger;
            _masterContext = masterContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string Account, string pd, string ReturnUrl)
        {
            var account = _masterContext.Account
                .AsNoTracking()
                .Where(x => x.Account1 == Account
                                            && x.Password == MD5Hash(pd))
                .FirstOrDefault();
            if (account == null)
            {
                ViewBag.errMsg = "account not found or password error";
                return PartialView();
            }

            Claim[] claims = new[] { new Claim("Account", Account) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(principal,
                new AuthenticationProperties()
                {
                    IsPersistent = false
                });

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Home");
        }


        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
