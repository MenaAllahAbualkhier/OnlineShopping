using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.BL.Helper;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Extend;

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {
        #region Cons
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole>roleManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        #endregion


        #region Sign UP
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = mapper.Map<ApplicationUser>(model);
                    var result = await userManager.CreateAsync(user, model.Password);
                    var result2=await userManager.AddToRoleAsync(user,"USER" );

                    if (result.Succeeded&&result2.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }

                    }
                    return View(model);
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Data);
                    if(user==null)
                    {
                         user = await userManager.FindByNameAsync(model.Data);
                    }
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    if (result.Succeeded)
                    {
                        var role = string.Join("",await userManager.GetRolesAsync(user));
                        if (role == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalied Email Or Password");
                        return View(model);
                    }
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                        SendEmail.SendMaile(new EmailVM { Title = "Password Reset", Massage = passwordResetLink, Mail = model.Email });

                        //logger.Log(LogLevel.Warning, passwordResetLink);

                        return RedirectToAction("ForgotPasswordConfirm");
                    }
                    ModelState.AddModelError("", "Invalied Email");
                    return View(model);
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }


        public IActionResult ForgotPasswordConfirm()
        {
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ConfirmResetPassword");
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }

                    return View(model);
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
        #endregion


        #region Sign Out
        
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            HttpContext.Response.Cookies.Delete("cart");
            return RedirectToAction("Login");
        }

        #endregion
    }
}
