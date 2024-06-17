﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration.UserSecrets;
using Prabin_SMS.Infrastructure;
using Prabin_SMS.Infrastructure.IRepository;
using Prabin_SMS.Models.Entity;
using Prabin_SMS.Models.ViewModels;
using Prabin_SMS.web.Data;
using Prabin_SMS.web.Models;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using Microsoft.AspNetCore.Authorization;

namespace Prabin_SMS.web.Controllers
{
    [Authorize(Roles="SUPERADMIN")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICRUDServices<Course> _course;
        private readonly ICRUDServices<Student> _student;
        private readonly ICRUDServices<Teacher> _teacher;
        private readonly ICRUDServices<Degree> _degree;
        private readonly UserManager<ApplicationUser> _user;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly ICRUDServices<StudentCourse> _studentCourse;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterViewModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IRawSqlRepository rawSqlRepository;

        public AccountController(ICRUDServices<Course> course, ICRUDServices<Student> student, UserManager<ApplicationUser> user, ICRUDServices<StudentCourse> studentCourse, ICRUDServices<Teacher> teacher, ICRUDServices<Degree> degree, SignInManager<ApplicationUser> signInManager, ILogger<RegisterViewModel> logger, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, IUserStore<ApplicationUser> userStore, IRawSqlRepository rawSqlRepository)
        {
            _course = course;
            _student = student;
            _user = user;
            _studentCourse = studentCourse;
            _teacher = teacher;
            _degree = degree;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _roleManager = roleManager;
            _emailSender = emailSender;
            this.rawSqlRepository = rawSqlRepository;
        }

        public async Task<IActionResult> Index()
        {
            var UserId = _user.GetUserId(HttpContext.User);
            var user = await _user.FindByIdAsync(UserId);
            IEnumerable<UserInfo> UserInfos = new List<UserInfo>();
            var users = _user.Users.ToList();
            var results =rawSqlRepository.FromSql<UserInfo>("_getEmployee");
            UserInfos = results;
            return View(UserInfos);
        }

        public async Task<IActionResult> AddUser()
        {
            var Ad_userId = _user.GetUserId(HttpContext.User);
            var Ad_user = await _user.FindByIdAsync(Ad_userId);
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.IsActive = true;
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterViewModel registerViewModel)
        {
            var Ad_userId = _user.GetUserId(HttpContext.User);
            var Ad_user = await _user.FindByIdAsync(Ad_userId);
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.IsActive = true;
                user.FirstName = registerViewModel.FirstName;
                user.LastName = registerViewModel.LastName;
                user.Address = registerViewModel.Address;
                user.ProfileUrl = registerViewModel.profileUrl;
                user.CreatedBy = Ad_user.Id;
                user.CreatedDate = DateTime.Now;

                var returnUrl = Url.Content("~/");
                var role = _roleManager.FindByNameAsync(registerViewModel.UserRoleId).Result;
                user.UserRoleId = role.Id;

                await _userStore.SetUserNameAsync(user, registerViewModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, registerViewModel.Email, CancellationToken.None);
                await _user.SetPhoneNumberAsync(user, registerViewModel.PhoneNumber);
                var result = await _user.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    if (role != null)
                    {
                        IdentityResult roleresult = await _user.AddToRoleAsync(user, role.Name);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _user.GetUserIdAsync(user);
                    var code = await _user.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(registerViewModel.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> EditUser(string Id)
        {
            var Ad_userId = _user.GetUserId(HttpContext.User);
            var Ad_user = await _user.FindByIdAsync(Ad_userId);
            var user = await _user.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{Id}'.");
            }
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.Id = Id;
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(RegisterViewModel registerViewModel)
        {
            var user = await _user.FindByIdAsync(registerViewModel.Id);

            if(user == null)
            {
                return RedirectToAction(nameof(EditUser));
            }

            user.FirstName = registerViewModel.FirstName;
            user.LastName = registerViewModel.LastName;
            user.Email = registerViewModel.Email;
            user.Address = registerViewModel.Address;
            user.PhoneNumber = registerViewModel.PhoneNumber;
            user.ProfileUrl = registerViewModel.profileUrl;

            await _user.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ResetPassword(string Id)
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            registerViewModel.Id = Id;
            var user =await _user.FindByIdAsync(Id);
            var code =await _user.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            registerViewModel.Code = code;
            return View(registerViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(RegisterViewModel registerViewModel)
        {
            var user = await _user.FindByIdAsync(registerViewModel.Id);
            var reset = _user.ResetPasswordAsync(user, registerViewModel.Code, registerViewModel.Password);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UserStatus(string Id)
        {
            var user = await _user.FindByIdAsync(Id);
            if(user.IsActive == true)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            await _user.UpdateAsync(user);
            return RedirectToAction(nameof(Index));

        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_user.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
