using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftwareBookList.Models;
using SoftwareBookList.Services;

namespace SoftwareBookList.Controllers;

public class AccountController : Controller
{
	private readonly UserAccountServices _userAccountServices;

	public AccountController(UserAccountServices uerAccountServices)
	{
		_userAccountServices = uerAccountServices;
	}

	[HttpGet("sign-up")]
	[AllowAnonymous]
	public IActionResult SignUp()
	{
		return View();
	}

	[HttpPost("sign-up")]
	[AllowAnonymous]
	public IActionResult SignUp(SignUpViewModel signUpViewModel)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		User existingUser = _userAccountServices.GetUser(signUpViewModel.EmailAddress);
		if (existingUser != null)
		{
			// Set email address already in use error message.
			ModelState.AddModelError("Error", "An account already exists with that email address.");

			return View();
		}

		PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

		// Generate a random username
		string randomUsername = _userAccountServices.GenerateRandomUserName();

		// Check if the username is unique
		bool isUniqueUsername = _userAccountServices.IsUserNameUnique(randomUsername);

		// If not unique, generate a new one until it's unique
		while (!isUniqueUsername)
		{
			randomUsername = _userAccountServices.GenerateRandomUserName();
			isUniqueUsername = _userAccountServices.IsUserNameUnique(randomUsername);
		}

		User user = new User()
		{
			FirstName = signUpViewModel.FirstName,
			LastName = signUpViewModel.LastName,
			EmailAddress = signUpViewModel.EmailAddress,
			PasswordHash = passwordHasher.HashPassword(null, signUpViewModel.Password),
			UserName = randomUsername,
		};


		_userAccountServices.AddUser(user);

		_userAccountServices.CreateList(user);

		return RedirectToAction(nameof(LogIn));
	}

	[HttpGet("sign-in")]
	[AllowAnonymous]
	public IActionResult LogIn()
	{
		return View();
	}

	[HttpPost("sign-in")]
	[AllowAnonymous]
	public async Task<IActionResult> LogIn(LoginViewModel loginViewModel, string? returnUrl)
	{
		if (!ModelState.IsValid)
		{
			return View();
		}

		User user = _userAccountServices.GetUser(loginViewModel.EmailAddress);

		if (user == null)
		{
			// Set email address not registered error message.
			ModelState.AddModelError("Error", "An account does not exist with that email address.");

			return View();
		}

		PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

		if (user.PasswordHash == "green")
		{
			user.PasswordHash = passwordHasher.HashPassword(null, user.PasswordHash);
		}

		PasswordVerificationResult passwordVerificationResult =
			passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginViewModel.Password);

		if (passwordVerificationResult == PasswordVerificationResult.Failed)
		{
			// Set invalid password error message.
			ModelState.AddModelError("Error", "Invalid password.");

			return View();
		}

		// Add the user's ID (NameIdentifier), first name to the claims that will be put in the cookie.
		var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
				new Claim( "userName", user.UserName),
				new Claim(ClaimTypes.Name, user.FirstName),
				new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
			};

		var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties { };

		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity),
			authProperties);

		if (string.IsNullOrEmpty(returnUrl))
		{
			return RedirectToAction("Index", "Home");
		}
		else
		{
			return Redirect(returnUrl);
		}
	}

	[HttpGet("sign-out")]
	[Route("sign-out")]
	[AllowAnonymous]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(
			CookieAuthenticationDefaults.AuthenticationScheme);

		return RedirectToAction("Index", "Home");
	}


}
