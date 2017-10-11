﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace AuthorizationLab.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            const string Issuer = "https://contoso.com";
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Niladri", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim(ClaimTypes.Role, "Administrator", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim("EmployeeId", "123", ClaimValueTypes.String, Issuer));
            claims.Add(new Claim(ClaimTypes.DateOfBirth, "1989-10-24", ClaimValueTypes.Date));
            claims.Add(new Claim("BadgeNumber", "123456", ClaimValueTypes.String, Issuer));
            //claims.Add(new Claim("TemporaryBadgeExpiry",
            //         DateTime.Now.AddDays(1).ToString(),
            //         ClaimValueTypes.String,
            //         Issuer));
            var userIdentity = new ClaimsIdentity("SuperSecureLogin");
            userIdentity.AddClaims(claims);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });

            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public IActionResult Forbidden()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
