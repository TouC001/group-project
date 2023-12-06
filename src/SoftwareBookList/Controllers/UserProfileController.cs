using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareBookList.Data;
using SoftwareBookList.Model_View;
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

			userProfileView.EmailAddress = _userProfileServices.GetUserEmailAddress(UserID);

			DateTime dateJoined = _userProfileServices.GetJoinedDate(UserID);

			if (userProfileView == null)
			{
				return NotFound();
			}

			userProfileView.DateJoin = dateJoined;

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

			User user = _userProfileServices.User(UserID);

			userAccount.Bio = userProfileView.Bio;
			userAccount.UserName = userProfileView.UserName;
			userAccount.Birthday = userProfileView.Birthday ?? DateTime.MinValue;
			userAccount.EmailAddress = userProfileView.EmailAddress;

			_userProfileServices.UpdateUserAccount(userAccount);

			userProfileView.UserBookList = _userProfileServices.GetBookListForUser(UserID);

			return View("Account", userProfileView);

		}

		[HttpPost("update-profile")]
		public async Task<IActionResult> UpdateProfile(UserProfileViewModel userProfile, IFormFile profilePicture)
		{
			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
			UserAccount userAccount = _userProfileServices.UserProfile(UserID);

			if (profilePicture != null)
			{
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
			// Check if the user had an existing profile picture
			else if (!string.IsNullOrEmpty(userAccount.ProfilePicture))
			{
				// Keep the existing profile picture
				userProfile.ProfilePicture = userAccount.ProfilePicture;
			}
			else
			{
				if (userAccount != null && !string.IsNullOrEmpty(userAccount.ProfilePicture))
				{
					_userProfileServices.UpdateProfilePicture(userAccount, userAccount.ProfilePicture);
				}
			}

			ModelState.Remove("ProfilePicture");

			if (!ModelState.IsValid)
			{
				return View("Account", userProfile);
			}

			bool updateSuccess = _userProfileServices.UpdateUserProfile(UserID, userProfile);

			await HttpContext.SignOutAsync(
			CookieAuthenticationDefaults.AuthenticationScheme);

			var user = _userProfileServices.UserProfile(UserID);

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
				return View("Account", userProfile);
			}
		}

		[HttpPost("AddComment")]
		public async Task<IActionResult> AddComment(AddCommentViewModel addComment)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("BookDetails");
			}

			int UserID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			Comment newComment = new Comment()
			{
				UserID = addComment.UserID,
				Commentor = _context.GetUserNameFromId(UserID),
				BookID = _context.GetBookIDByGoogleID(addComment.BookID),
				Content = addComment.textContent,
				CreatedAt = DateTime.Now
			};

			_context.comments.Add(newComment);
			_context.SaveChanges();

			return RedirectToAction("BookDetails", "Books", new { googleID = addComment.BookID});
		}
	}
}
