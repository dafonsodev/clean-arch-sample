using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IAuthenticate authenticate;

    public AccountController(IAuthenticate authenticate)
    {
        this.authenticate = authenticate;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var result = await authenticate.AuthenticateAsync(loginViewModel.Email, loginViewModel.Password);
        if (result)
        {
            if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(loginViewModel.ReturnUrl);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt (password must be strong).");
            return View(loginViewModel);
        }
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var result = await authenticate.RegisterUserAsync(registerViewModel.Email, registerViewModel.Password);
        if (result)
        {
            return Redirect("/");
        }
        else 
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong).");
            return View(registerViewModel); 
        }
    }

    public async Task<IActionResult> Logout()
    {
        await authenticate.LogoutAsync();
        return Redirect("/Account/Login");
    }
}
