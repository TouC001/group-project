using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareBookList.Data;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Security.Claims;

namespace SoftwareBookList.Controllers
{
	[Authorize]
	public class UserProfileController : Controller
	{
		private readonly UserProfileService _userProfileServices;

		private readonly DataContext _context;

		public UserProfileController(DataContext userProfileServices, DataContext context)
		{
			_userProfileServices = new UserProfileService(userProfileServices);
			_context = context;
		}

		[HttpGet("Account")]
		public IActionResult Account()
		{

			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			UserProfileViewModel userProfileView = _userProfileServices.GetUserProfile(UserID);

			userProfileView.UserBookList = _userProfileServices.GetBookListForUser(UserID);

			if (userProfileView == null)
			{
				return NotFound();
			}

			return View(userProfileView);
		}

		[HttpGet("EditProfile")]
		public IActionResult EditProfile()
		{
			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			UserProfileViewModel userProfileView = _userProfileServices.GetUserProfile(UserID);

			userProfileView.UserBookList = _userProfileServices.GetBookListForUser(UserID);

			return View(userProfileView);
		}

		[HttpPost("EditProfile")]
		public IActionResult EditProfile(UserProfileViewModel userProfileView)
		{

			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


			if (!ModelState.IsValid)
			{

				return RedirectToAction("EditProfile", userProfileView);
			}


			UserAccount userAccount = _userProfileServices.UserProfile(UserID);

			userAccount.Bio = userProfileView.Bio;
			userAccount.UserName = userProfileView.UserName;
			userAccount.Birthday = userProfileView.Birthday;

			_userProfileServices.UpdateUserAccount(userAccount);

			userProfileView.UserBookList = _userProfileServices.GetBookListForUser(UserID);

			return View("Account", userProfileView);

		}

		[HttpPost("update-profile")]
		public async Task<IActionResult> UpdateProfile(UserProfileViewModel userProfile, IFormFile profilePicture)
		{
			if (!ModelState.IsValid)
			{
				return View("Account", userProfile);
			}

			int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			bool updateSuccess = _userProfileServices.UpdateUserProfile(userID, userProfile);

			if (profilePicture != null)
			{
				// Extract the UserID from the current user's claims.
				int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				// Retrieve the user's account information using the UserProfileService.
				UserAccount userAccount = _userProfileServices.UserProfile(UserID);

				// Check if the user's account doesn't exist. If so, add a model error and redirect to the "Account" action.
				if (userAccount == null)
				{
					return RedirectToAction("Account");
				}

				// Get the file extension of the uploaded profile picture.
				string fileExtension = System.IO.Path.GetExtension(profilePicture.FileName);

				// Define the file path where the profile picture will be stored.
				string stringPath = System.IO.Path.Combine("wwwroot/lib/ProfilePictures", userAccount.AccountID + fileExtension);

				// Check if a file with the same name already exists and delete it.
				if (System.IO.File.Exists(stringPath))
				{
					System.IO.File.Delete(stringPath);
				}

				// Copy the uploaded profile picture to the specified file path.
				using (FileStream f = System.IO.File.OpenWrite(stringPath))
				{
					profilePicture.CopyTo(f);
				}

				// Use the UserProfileService to update the user's profile picture.
				_userProfileServices.UpdateProfilePicture(userAccount, stringPath);
			}

			await HttpContext.SignOutAsync(
			CookieAuthenticationDefaults.AuthenticationScheme);

			var user = _userProfileServices.UserProfile(userID);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
				new Claim( "userName", user.UserName)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties { };

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);

			if (updateSuccess)
			{
				return RedirectToAction("Account");
			}
			else
			{
				ViewBag.ErrorMessage = "Failed to update profile. Please try again.";
				return View("Account", userProfile);
			}
		}
	}
}
