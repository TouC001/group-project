using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public UserProfileViewModel GetUserProfile(int UserID)
        {
            // Retrieve the user's profile from the database.
            UserAccount userProfile = _dataContext.Accounts.FirstOrDefault(u => u.UserID == UserID);

            User user = _dataContext.Users.FirstOrDefault(u => u.UserID == UserID);

            if (userProfile == null)
            {
                userProfile = new UserAccount()
                {
                    UserName = "",
                    Bio = "",
                    Birthday = DateTime.MinValue,
                    ProfilePicture = "",
                    UserID = UserID,
                    EmailAddress = "",


                };

                _dataContext.Accounts.Add(userProfile);
                _dataContext.SaveChanges();
            }

            UserProfileViewModel profileViewModel = new UserProfileViewModel()
            {
                UserName = user.UserName,
                Bio = string.IsNullOrEmpty(userProfile.Bio) ? "Bio Here" : userProfile.Bio,
                Birthday = userProfile.Birthday,
                ProfilePicture = userProfile.ProfilePicture,
                UserID = userProfile.UserID,
                EmailAddress = user.EmailAddress

            };

            return profileViewModel;
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

        public bool UpdateUserProfile(int userId, UserProfileViewModel updatedProfile)
        {
            // Retrieve the user's profile from the database
            UserAccount userProfile = _dataContext.Accounts.FirstOrDefault(u => u.UserID == userId);

            User user = _dataContext.Users.FirstOrDefault(u => u.UserID == userId);

            if (userProfile != null && user != null)

            {
                if (!string.IsNullOrEmpty(updatedProfile.UserName))
                {
                    userProfile.UserName = updatedProfile.UserName;
                    user.UserName = updatedProfile.UserName;
                }

                if (!string.IsNullOrEmpty(updatedProfile.Bio))
                {
                    userProfile.Bio = updatedProfile.Bio;
                }

                userProfile.Birthday = updatedProfile.Birthday ?? DateTime.MinValue;
                userProfile.EmailAddress = updatedProfile.EmailAddress;
                user.EmailAddress = updatedProfile.EmailAddress;


                _dataContext.SaveChanges();

                return true;
            }

            return false;
        }

        public UserAccount UpdateUserAccount(UserAccount account)
        {
            _dataContext.Accounts.Update(account);
            _dataContext.SaveChanges();
            return account;
        }

        public UserAccount UserProfile(int UserID)
        {
            return _dataContext.Accounts.FirstOrDefault(u => u.UserID == UserID);
        }

        public User User(int UserID)
        {
            return _dataContext.Users.FirstOrDefault(u => u.UserID == UserID);
        }

        public BookList GetBookListForUser(int UserID)
        {
            return _dataContext.BookLists
                .Include(bl => bl.BookInLists)
                    .ThenInclude(bil => bil.Book)
                .Include(bl => bl.BookInLists)
                    .ThenInclude(bil => bil.Status)
                .FirstOrDefault(bl => bl.UserID == UserID);
        }

        public DateTime GetJoinedDate(int UserID)
        {
            User user = _dataContext.Users
               .Where(u => u.UserID == UserID)
               .FirstOrDefault();

            // Return the join date if available, or DateTime.MinValue if the user or join date is null
            return user?.DateJoin ?? DateTime.MinValue;
        }

        public string GetUserEmailAddress(int UserID)
        {
            User user = _dataContext.Users
                .Where(u => u.UserID == UserID)
                .FirstOrDefault();

            // Return the email address if available, or null if the user is null
            return user?.EmailAddress;
        }
    }
}
