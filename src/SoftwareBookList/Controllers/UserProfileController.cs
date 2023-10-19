using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Security.Claims;

namespace SoftwareBookList.Controllers
{
	[Authorize]
	//The Controller is like a workshop that we will be using to handle all the task related to the users account.
	public class UserProfileController : Controller
	{
		// Dependecy Injection of the UserProfileServive that will have all the logic and services we will need for our controller such as methods.
		// _userProfileServices is a way for us to use it to access the methods and logic within the UserProfileService class.
		private readonly UserProfileService _userProfileServices;

		// Our Constructor that is invoked when an instance of the UserProfileController is created.
		// It takes a DataContext type because it assumes that the UserProfileService Class depends on the DataContext class which it does.
		public UserProfileController(DataContext userProfileServices)
		{
			// Constructor then initializes the private field by creating a new instance of the UserProfileService and passing in the DataContext.
			// This will then make it appearent that this class will rely on UserProfileService class.
			_userProfileServices = new UserProfileService(userProfileServices);
		}

		// When someone access the "Account" page via web browser.
		[HttpGet("Account")]
		//This will return an IActionResult which will return a result of the rendered page.
		public IActionResult Account()
		{
			// Finding the UserID from the current user's claim. Claims are pieces of information that hold information about the user.
			// It is looking for a claim with the name of "NameIdetifier that would have the UserID stashed in it.
			// NameIdetifier will be found in out AccountController in the Login HTTP Post.
			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			// Using the GetUserProfile Method from the UserProfileService anad storing it in the variable of type UserProfileViewModel.
			UserProfileViewModel userProfileView = _userProfileServices.GetUserProfile(UserID);

			return View(userProfileView);
		}

		[HttpGet("EditProfile")]
		public IActionResult EditProfile()
		{
			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			UserProfileViewModel userProfileView = _userProfileServices.GetUserProfile(UserID);

			return View(userProfileView);
		}

		[HttpPost("EditProfile")]
		public IActionResult EditProfile(UserProfileViewModel userProfileView)
		{

			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			if (!ModelState.IsValid)
			{
				UserProfileViewModel editProfileView = _userProfileServices.GetUserProfile(UserID);

				return View(editProfileView);
			}


			UserAccount userAccount = _userProfileServices.UserProfile(UserID);

			userAccount.Bio = userProfileView.Bio;
			userAccount.UserName = userProfileView.UserName;
			userAccount.Birthday = userProfileView.Birthday;

			_userProfileServices.UpdateUserAccount(userAccount);

			return View(userProfileView);

		}


		[HttpPost("upload-picture")]
		public IActionResult UpdateProfilePicture(IFormFile profilePicture)
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


			return RedirectToAction("EditProfile");
		}

		[HttpPost("update-profile")]
		public async Task<IActionResult> UpdateProfile(UserProfileViewModel userProfile)
		{
			if (!ModelState.IsValid)
			{
				return View("Account", userProfile);
			}

			int userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			bool updateSuccess = _userProfileServices.UpdateUserProfile(userID, userProfile);

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

		//[HttpPost("update-birthday")]
		//public async Task<IActionResult> UpdateUserBirthday(DateTime newBirthday)
		//{
		//    int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

		//    IdentityResult updateResult = await _userProfileServices.UpdateUserBirthday(UserID, newBirthday);

		//    if (updateResult.Succeeded)
		//    {
		//        return RedirectToAction("Account");
		//    }
		//    else
		//    {
		//        ViewBag.ErrorMessage = "Failed to Update Birthday. Please Try again.";
		//        return RedirectToAction("Account");
		//    }
		//}

		//[HttpPost("update-bio")]
		//public async Task<IActionResult> UpdateUserBio(string newBio)
		//{
		//    int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

		//    if (!string.IsNullOrEmpty(newBio))
		//    {
		//        IdentityResult updatedBio = await _userProfileServices.UpdateUserBio(UserID, newBio);

		//        if (updatedBio.Succeeded)
		//        {
		//            return RedirectToAction("Account");
		//        }
		//    }

		//    ViewBag.ErrorMessage = "Failed to Update Bio. Please try again.";
		//    return RedirectToAction("Account");
		//}

		//[HttpPost("update-username")]
		//public async Task<IActionResult> UpdateUserName(string newUserName)
		//{
		//    int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

		//    if (!string.IsNullOrEmpty(newUserName))
		//    {
		//        IdentityResult updatedUserName = await _userProfileServices.UpdateUserName(UserID, newUserName);

		//        if (updatedUserName.Succeeded)
		//        {
		//            return RedirectToAction("Account");
		//        }
		//    }

		//    ViewBag.ErrorMessage = "Failed to Update UserName. Please try again.";
		//    return RedirectToAction("Account");
		//}
	}
}
