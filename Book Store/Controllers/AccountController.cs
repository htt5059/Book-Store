using Book_Store.Models.Domains;
using Book_Store.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Register(AccountVM model) 
        {
            var user = new IdentityUser {
                UserName = model.Account.UserName,
                Email = model.Account.Email,
            };
            
            var identityUserResult = await _userManager.CreateAsync(user, model.Account.Password);
            if (identityUserResult.Succeeded) {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded) {
                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                        return RedirectToPage(model.ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(new AccountVM { Account = model.Account, Result = identityUserResult.ToString()});
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new AccountVM { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountVM model) {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Account.UserName,
                model.Account.Password, false, false);
            if (signInResult != null && signInResult.Succeeded) {
                if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }
            
            return View(new AccountVM { Account = model.Account, Result = signInResult.ToString() });
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccessDenied() {
            return View();
        }
    }
}
