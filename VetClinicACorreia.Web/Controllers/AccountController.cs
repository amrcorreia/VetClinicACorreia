﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VetClinicACorreia.Web.Data.Entities;
using VetClinicACorreia.Web.Data.Repositories;
using VetClinicACorreia.Web.Helpers;
using VetClinicACorreia.Web.Models;

namespace VetClinicACorreia.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly ICountryRepository _countryRepository;
        private readonly IMailHelper _mailHelper;

        public AccountController(IUserHelper userHelper,
            IConfiguration configuration,
            ICountryRepository countryRepository,
            IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return this.Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }
            this.ModelState.AddModelError(string.Empty, "Failed to login.");
            return this.View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel
            {
                //Countries = _countryRepository.GetComboCountries(),
                //Cities = _countryRepository.GetComboCities(0)
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                if (user == null)
                {
                    //var city = await _countryRepository.GetCityAsync(model.CityId);
                    //var speciality = await _specialityRepository.GetSpecialitiesAsync(model.SpecialityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        PhoneNumber = model.PhoneNumber,
                        TIN = model.TIN,
                        //CityId = model.CityId,
                        //City = city,
                        //SpecialityId = model.SpecialityId,

                    };

                    var result = await this._userHelper.AddUserAsync(user, model.Password); //guarda o user
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    if (user.TIN == null)
                    {
                        var tokenLink = this.Url.Action("ConfirmEmailDoctor", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                            $"To allow the user, " +
                            $"plase click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                        this.ViewBag.Message = "The instructions to allow your user has been sent to email.";

                        return this.View(model);
                    }
                    else
                    {                      
                        var tokenLink1 = this.Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Email Confirmation</h1>" +
                            $"To allow the user, " +
                            $"plase click in this link:</br></br><a href = \"{tokenLink1}\">Confirm Email</a>");
                        this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                        return this.View(model);
                    }
                   
                }

                this.ModelState.AddModelError(string.Empty, "The username is already registered.");
            }

            return this.View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }
            //TODO
            //Atribuir o role a Customer
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Customer");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Customer");
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmailDoctor(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }
            //TODO
            //Atribuir o role a Doctor
            var isInRole1 = await _userHelper.IsUserInRoleAsync(user, "Doctor");

            if (!isInRole1)
            {
                await _userHelper.AddUserToRoleAsync(user, "Doctor");
            }

            return View();
        }


        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;

                //var city = await _countryRepository.GetCityAsync(user.CityId);
                //if (city != null)
                //{
                //    var country = await _countryRepository.GetCountryAsync(city);
                //    if (country != null)
                //    {
                //        model.CountryId = country.Id;
                //        model.Cities = _countryRepository.GetComboCities(country.Id);
                //        model.Countries = _countryRepository.GetComboCountries();
                //        model.CityId = user.CityId;
                //    }
                //}
            }

            //model.Cities = _countryRepository.GetComboCities(model.CountryId);
            //model.Countries = _countryRepository.GetComboCountries();
            return this.View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    var respose = await _userHelper.UpdateUserAsync(user);
                    if (respose.Succeeded)
                    {
                        this.ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, respose.Errors.
                            FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return this.View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User no found.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }
            return this.BadRequest();
        }

        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "Shop Password Reset", $"<h1>Shop Password Reset</h1>" +
                $"To reset the password click in this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");
                this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
                return this.View();
            }

            return this.View(model);
        }


        public IActionResult ResetPassword(string token)
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return this.View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }


        //public async Task<JsonResult> GetCitiesAsync(int countryId)
        //{
        //    var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
        //    return this.Json(country.Cities.OrderBy(c => c.Name));
        //}

    }
}
