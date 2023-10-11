using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.Models;
using SoftwareBookList.Services;
using System.Security.Claims;

namespace SoftwareBookList.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserProfileService _userProfileServices;

        public UserProfileController(DataContext userProfileServices)
        {
            _userProfileServices = new (userProfileServices);
        }

        [HttpGet("Account")]
        [Authorize]
        public IActionResult Account()
        {
            return View();
        }

        [HttpPost("upload-picture")]
        public IActionResult UpdateProfilePicture(IFormFile profilePicture)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            UserAccount userAccount = _userProfileServices.GetUserProfile(userId);

            if (userAccount == null)
            {
                ModelState.AddModelError("Error", "This Profile does not exist.");

                return RedirectToAction("Account");
            }

            string fileExtension = System.IO.Path.GetExtension(profilePicture.Name);

            string stringPath = System.IO.Path.Combine("wwwroot/lib/ProfilePictures", userAccount.AccountID + fileExtension);

            if (System.IO.File.Exists(stringPath))
            {
                System.IO.File.Delete(stringPath);
            }

            using (FileStream f = System.IO.File.OpenWrite(stringPath))
            {
                profilePicture.CopyTo(f);
            }

            _userProfileServices.UpdateProfilePicture(userAccount, stringPath);

            return RedirectToAction("Account");
        }
    }
}
