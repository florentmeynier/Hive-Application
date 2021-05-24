using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HivePSTL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Okta.AspNetCore;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace HivePSTL.Controllers
{
    public class AccountController : Controller
    {
        private readonly OktaClient oktaClient;
        
        public AccountController(IConfiguration configuration)
        {
            oktaClient = new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = configuration["Okta:Domain"],
                Token = configuration["Okta:ApiToken"]
            });
        }

        public IActionResult Login()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Challenge(OktaDefaults.MvcAuthenticationScheme);
            }

            return RedirectToAction("Index", "Home");
        }

        //if(UserProfile)
        public async Task<IActionResult> Profile()
        {
            var user = await GetOktaUser();

            return View(user);
        }

        public async Task<IActionResult> EditProfile()
        {
            var user = await GetOktaUser();
  
            return View(new UserProfileViewModel
            {
                MobilePhone = user.Profile.MobilePhone,
                Title = user.Profile.Title,
                PostalAddress = user.Profile.PostalAddress,
                City = user.Profile.City,
                Email = user.Profile.Email,
                CountryCode = user.Profile.CountryCode,
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                var user = await GetOktaUser();
                user.Profile.FirstName = profile.FirstName;
                user.Profile.LastName = profile.LastName;
                user.Profile.Email = profile.Email;
                user.Profile.MobilePhone = profile.MobilePhone;
                user.Profile.Title = profile.Title;
                user.Profile.PostalAddress = profile.PostalAddress;
                user.Profile.City = profile.City;
                user.Profile.CountryCode = profile.CountryCode;

                await oktaClient.Users.UpdateUserAsync(user, user.Id, null);

                return RedirectToAction("Profile");
            }
  
            return View(profile);
        }

        //Endif(UserProfile)

        public IActionResult Logout()
        {
            return new SignOutResult(new[]
            {
                OktaDefaults.MvcAuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme
            });
        }

        private async Task<IUser> GetOktaUser()
        {  
             
            var subject = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return await oktaClient.Users.GetUserAsync(subject);
        }

    }
}