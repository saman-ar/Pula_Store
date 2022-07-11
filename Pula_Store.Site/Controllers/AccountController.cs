using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repo;
using Entities.AppIdentities;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Pula_Store.Site.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Site.Models;
using Site.Models.ViewModel;
using Site.Models.ViewModels;
using WebFramework.Extensions;
using System.IO;
using WebFramework.ActionFilters;
using System.Security.Claims;
using Data.AppContext;

namespace Site.Controllers
{
    [Authorize]
    //[Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private IPasswordValidator<AppUser> _passwordValidator;

        public AccountController(
             AppDbContext context,
             UserManager<AppUser> userManager,
             SignInManager<AppUser> signInManager,
             //IEmailSender emailSender,
             ILogger<AccountController> logger,
             IPasswordValidator<AppUser> passwordValidator)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            _logger = logger;
            _passwordValidator = passwordValidator;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        //[ajax("Post")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong Info!!");
                return View(model);
            }
            ///ابتدا signout میکنیم تا همه session های موجود بسته شود و سپس
            ///دوباره signin میکنیم
            await _signInManager.SignOutAsync();
            //var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            ///this code use before "signin" for checking email confirm checking
            var isPasswordTrue = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordTrue)
            {
                ModelState.AddModelError("", "Wrong Info");
                return View(model);
            }

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //	return View("Info", "Email Is Not Confirmed!!!");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "مشکلی در ورود شما وجود دارد");
                return View(model);
            }


            returnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/";
            return Redirect(returnUrl);

            #region test
            ///استفاده از Cliam-base identity
            ///claimIdentity بر اساس یک schema خاص cookies ساخته میشود
            //var identity = new ClaimsIdentity("Cookies");
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            //await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(identity));

            ///Redirect to /home/index 
            #endregion
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        //{
        //	// Ensure the user has gone through the username & password screen first
        //	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

        //	if (user == null)
        //	{
        //		throw new ApplicationException($"Unable to load two-factor authentication user.");
        //	}

        //	var model = new LoginWith2faViewModel { RememberMe = rememberMe };
        //	ViewData["ReturnUrl"] = returnUrl;

        //	return View(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View(model);
        //	}

        //	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //	if (user == null)
        //	{
        //		throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //	}

        //	var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

        //	var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

        //	if (result.Succeeded)
        //	{
        //		_logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
        //		return RedirectToLocal(returnUrl);
        //	}
        //	else if (result.IsLockedOut)
        //	{
        //		_logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
        //		return RedirectToAction(nameof(Lockout));
        //	}
        //	else
        //	{
        //		_logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
        //		ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
        //		return View();
        //	}
        //}


        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        //{
        //	// Ensure the user has gone through the username & password screen first
        //	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //	if (user == null)
        //	{
        //		throw new ApplicationException($"Unable to load two-factor authentication user.");
        //	}

        //	ViewData["ReturnUrl"] = returnUrl;

        //	return View();
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        //{
        //	if (!ModelState.IsValid)
        //	{
        //		return View(model);
        //	}

        //	var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        //	if (user == null)
        //	{
        //		throw new ApplicationException($"Unable to load two-factor authentication user.");
        //	}

        //	var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

        //	var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

        //	if (result.Succeeded)
        //	{
        //		_logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
        //		return RedirectToLocal(returnUrl);
        //	}
        //	if (result.IsLockedOut)
        //	{
        //		_logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
        //		return RedirectToAction(nameof(Lockout));
        //	}
        //	else
        //	{
        //		_logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
        //		ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
        //		return View();
        //	}
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Lockout()
        //{
        //	return View();
        //}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View(model);

            ///check if user exist in database
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
                return View(model);

            var newUser = new AppUser();
            newUser.UserName = model.UserName;
            //newUser.FullName = model.FullName;
            newUser.Email = model.Email;

            var userWithExistEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithExistEmail != null)
            {
                ModelState.TryAddModelError("", "این ایمیل قبلا ثبت نام کرده است");
                return View(model);
            }

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "مشکلی در ثبت نام شما بوجود امده است لطفا دوباره سعی کنید");
                return View(model);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmEmail = Url.Action("ConfirmEmail", "Account", new { token = token, email = newUser.Email }, Request.Scheme);

            System.IO.File.WriteAllText("c://confirmemail.txt", confirmEmail);

            return View("~/Views/Shared/Info.cshtml");
            //return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            //_logger.LogInformation("User logged out.");

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public IActionResult ExternalLogin(string provider, string returnUrl = null)
        //{
        //	// Request a redirect to the external login provider.
        //	var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        //	var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //	return Challenge(properties, provider);
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //	if (remoteError != null)
        //	{
        //		ErrorMessage = $"Error from external provider: {remoteError}";
        //		return RedirectToAction(nameof(Login));
        //	}
        //	var info = await _signInManager.GetExternalLoginInfoAsync();
        //	if (info == null)
        //	{
        //		return RedirectToAction(nameof(Login));
        //	}

        //	// Sign in the user with this external login provider if the user already has a login.
        //	var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //	if (result.Succeeded)
        //	{
        //		_logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
        //		return RedirectToLocal(returnUrl);
        //	}
        //	if (result.IsLockedOut)
        //	{
        //		return RedirectToAction(nameof(Lockout));
        //	}
        //	else
        //	{
        //		// If the user does not have an account, then ask the user to create an account.
        //		ViewData["ReturnUrl"] = returnUrl;
        //		ViewData["LoginProvider"] = info.LoginProvider;
        //		var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //		return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
        //	}
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		// Get the information about the user from the external login provider
        //		var info = await _signInManager.GetExternalLoginInfoAsync();
        //		if (info == null)
        //		{
        //			throw new ApplicationException("Error loading external login information during confirmation.");
        //		}
        //		var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //		var result = await _userManager.CreateAsync(user);
        //		if (result.Succeeded)
        //		{
        //			result = await _userManager.AddLoginAsync(user, info);
        //			if (result.Succeeded)
        //			{
        //				await _signInManager.SignInAsync(user, isPersistent: false);
        //				_logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
        //				return RedirectToLocal(returnUrl);
        //			}
        //		}
        //		AddErrors(result);
        //	}

        //	ViewData["ReturnUrl"] = returnUrl;
        //	return View(nameof(ExternalLogin), model);
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
                return RedirectToAction(nameof(HomeController.Index), "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");

            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email Address not exist");
                return View();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resultUrl = Url.Action("ResetPassword", "Account", new { token = token, email = user.Email }, Request.Scheme);

            ///send email with this link to user
            System.IO.File.WriteAllText("c://resetlink.txt", resultUrl);

            return View("info", "success");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string email, string token)
        {
            if (!ModelState.IsValid)
                return View("ErrorPage");

            var model = new ResetPasswordViewModel { Email = email, Code = token };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction(nameof(ResetPasswordConfirmation));
                ModelState.AddModelError(string.Empty, "wrong Info!");
                return View(model);
            }

            var passwordValidationResult = await _passwordValidator.ValidateAsync(_userManager, user, model.Password);
            if (passwordValidationResult.Errors.Any())
            {
                foreach (var error in passwordValidationResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        public IActionResult Profile()
        {
            var userId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = new AppUser();

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Index", "Home");

            user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
                return BadRequest();

            var model = new ProfileShowViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }

    //   public class AccountController : Controller
    //   {
    //	private readonly UserRepository _userRepo;
    //	public AccountController(UserRepository userRepo)
    //	{
    //		_userRepo = userRepo;
    //	}

    //	public IActionResult Register()
    //	{
    //		return View();
    //	}

    //	[AcceptVerbs("Post")]
    //	public IActionResult Create(UserDto user)
    //	{
    //		return View();
    //	}

    //	public IActionResult Login()
    //	{
    //		return View();
    //	}

    //	[AcceptVerbs("Post")]
    //	public IActionResult Login(UserDto user)
    //	{
    //		return View();
    //	}


    //	public IActionResult Logout()
    //	{
    //		return View();
    //	}


    //	public IActionResult Edit()
    //	{
    //		return View();
    //	}

    //	[AcceptVerbs("Post")]
    //	public IActionResult Edit(UserDto user)
    //	{
    //		return View();
    //	}


    //	public IActionResult ResetPassword()
    //	{
    //		return View();
    //	}

    //	[AcceptVerbs("Post")]
    //	public IActionResult ResetPassword(string email)
    //	{
    //		return View();
    //	}

    //	public IActionResult ForgetPassword()
    //	{
    //		return View();
    //	}

    //	[AcceptVerbs("Post")]
    //	public IActionResult ForgetPassword(string email)
    //	{
    //		return View();
    //	}


    //}


}