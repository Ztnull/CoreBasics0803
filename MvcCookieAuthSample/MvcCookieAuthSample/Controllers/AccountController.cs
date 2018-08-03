using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace MvcCookieAuthSample.Controllers
{
    /// <summary>
    /// 添加认证授权cookie控制器
    /// </summary>
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// cookie添加【登录】
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            //首先创建一个Claim集合
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,"DreamNull"),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return View();
        }


        /// <summary>
        /// cookie清除【注销】
        /// </summary>
        /// <returns></returns>
        public IActionResult LoginOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }






    }
}