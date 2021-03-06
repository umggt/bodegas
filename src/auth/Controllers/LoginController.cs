﻿using System.Threading.Tasks;
using Bodegas.Auth.Models;
using IdentityServer4.Core;
using IdentityServer4.Core.Services;
using Microsoft.AspNet.Mvc;
using System.Security.Claims;
using IdentityModel;
using System;
using Bodegas.Auth.Services;
using System.Linq;
using Microsoft.AspNet.Authorization;

namespace Bodegas.Auth.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private readonly SignInInteraction signInInteraction;

        public LoginController(
            LoginService loginService,
            SignInInteraction signInInteraction)
        {
            _loginService = loginService;
            this.signInInteraction = signInInteraction;
        }

        [HttpGet(Constants.RoutePaths.Login, Name = "Login")]
        public async Task<IActionResult> Index(string id)
        {
            var vm = new LoginViewModel();

            if (id != null)
            {
                var request = await signInInteraction.GetRequestAsync(id);
                if (request != null)
                {
                    vm.Username = request.LoginHint;
                    vm.SignInId = id;
                }
            }

            return View(vm);
        }

        [HttpPost(Constants.RoutePaths.Login)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (_loginService.ValidateCredentials(model.Username, model.Password))
                {
                    var user = _loginService.FindByUsername(model.Username);

                    var name = user.NombreCompleto ?? user.Login;

                    var claims = new Claim[] {
                        new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                        new Claim(JwtClaimTypes.Name, name),
                        new Claim(JwtClaimTypes.IdentityProvider, "idsvr"),
                        new Claim(JwtClaimTypes.AuthenticationTime, DateTime.UtcNow.ToEpochTime().ToString()),
                    };
                    var ci = new ClaimsIdentity(claims, "password", JwtClaimTypes.Name, JwtClaimTypes.Role);
                    var cp = new ClaimsPrincipal(ci);

                    await HttpContext.Authentication.SignInAsync(Constants.PrimaryAuthenticationType, cp);

                    if (model.SignInId != null)
                    {
                        return new SignInResult(model.SignInId);
                    }

                    return Redirect("~/");
                }

                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }

            var vm = new LoginViewModel(model);
            return View(vm);
        }
    }
}
