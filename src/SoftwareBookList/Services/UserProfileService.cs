using SoftwareBookList.Data;
using SoftwareBookList.Models;

namespace SoftwareBookList.Services
{
    public class UserProfileService
    {
        private readonly DataContext _dataContext;

        public UserProfileService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public UserAccount GetUserProfile(int userId)
        {
            // Retrieve the user's profile from the database.
            UserAccount userProfile = _dataContext.Accounts.FirstOrDefault(u => u.UserID == userId);

            return userProfile;
        }

        public void UpdateProfilePicture(UserAccount userProfile, string profilePicture)
        {
            // Update the user's profile picture in the database.
            if (userProfile != null)
            {
                // Update the profile picture property with the new byte array.
                userProfile.ProfilePicture = profilePicture;

                _dataContext.SaveChanges();
            }
        }

        public void UpdateUserName(int userId, string newUserName)
        {
            // Update the user's username in the database.
        }
    }
}
