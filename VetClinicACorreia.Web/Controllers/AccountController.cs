using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
using VetClinicACorreia.Web.Data;
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
        private readonly IVetAssistantRepository _vetAssistantRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccountController(IUserHelper userHelper,
            IConfiguration configuration,
            ICountryRepository countryRepository,
            IMailHelper mailHelper,
            IVetAssistantRepository vetAssistantRepository,
            ICustomerRepository customerRepository)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _countryRepository = countryRepository;
            _mailHelper = mailHelper;
            _vetAssistantRepository = vetAssistantRepository;
            _customerRepository = customerRepository;
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
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    //var city = await _countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        TIN = model.TIN,
                        PhoneNumber = model.PhoneNumber,
                        //CityId = model.CityId,
                        //City = city
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return this.View(model);
                    }

                    if (model.Password == "VetAssistant")
                    {
                        User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
                        await _userHelper.AddUserToRoleAsync(userInDB, "VetAssistant");

                        VetAssistant vetAssistant = new VetAssistant
                        {

                            User = userInDB
                        };
                        await _vetAssistantRepository.CreateAsync(vetAssistant);
                        //_context.VetAssistants.Add(vetAssistant);
                        //await _context.SaveChangesAsync();


                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    
                        var tokenLink = this.Url.Action("ConfirmEmailVetAssistant", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.Username, "YourVet - Email confirmation", $"<h1>Veterinary Assistant Email Confirmation</h1>" +
                            $"<br/>" +
                            $"Welcome to YouVet, you password is VetAssistant. Please change you password as soon as possible." +
                            $"To allow the user, " +
                            $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                        this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    }
                    else
                    {
                        User userInDB = await _userHelper.GetUserByEmailAsync(user.UserName);
                        await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                        Customer customer = new Customer
                        {
                            Pets = new List<Pet>(),
                            User = userInDB
                        };
                        await _customerRepository.CreateAsync(customer);
                        //_context.Customers.Add(owner);
                        //await _context.SaveChangesAsync();

                        var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                        var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                        {
                            userid = user.Id,
                            token = myToken
                        }, protocol: HttpContext.Request.Scheme);

                        _mailHelper.SendMail(model.Username, "YourVet - Email confirmation", $"<h1>Customer Email Confirmation</h1>" +
                            $"<br/>" +
                            $"Welcome to YouVet!!" +
                            $"To allow the user, " +
                            $"please click in this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                        this.ViewBag.Message = "The instructions to allow your user has been sent to email.";
                    }
                    
                    return this.View(model);
                }

                this.ModelState.AddModelError(string.Empty, "The user already exists.");
            }

            return View(model);
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

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Customer");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Customer");
            }

            return View();
        }

        public async Task<IActionResult> ConfirmEmailVetAssistant(string userId, string token)
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

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "VetAssistant");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "VetAssistant");
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
                model.Username = user.UserName;
                model.PhoneNumber = user.PhoneNumber;
                model.TIN = user.TIN;

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
                    user.UserName = model.Username;
                    user.PhoneNumber = model.PhoneNumber;
                    user.TIN = model.TIN;

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

                _mailHelper.SendMail(model.Email, "YourVet - Password Reset", $"<h1>YourVet - Veterinary Clinic</h1>" +
                    $"<h3>Password Reset Information</h3>" +
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
                    this.ViewBag.Message = "Password reset successfuly.";
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
