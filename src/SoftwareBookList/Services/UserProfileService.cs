using Microsoft.AspNetCore.Identity;
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

            if (userProfile == null)
            {
                userProfile = new UserAccount()
                {
                    UserName = "",
                    Bio = "",
                    Birthday = DateTime.MinValue,
                    ProfilePicture = "",
                    UserID = UserID

                };

                _dataContext.Accounts.Add(userProfile);
                _dataContext.SaveChanges();
            }

            UserProfileViewModel profileViewModel = new UserProfileViewModel()
            {
                UserName = userProfile.UserName,
                Bio = userProfile.Bio,
                Birthday = userProfile.Birthday,
                ProfilePicture = userProfile.ProfilePicture,
                UserID = userProfile.UserID
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

            if (userProfile != null)
            {
                // Update the profile with the new data
                userProfile.UserName = updatedProfile.UserName;
                userProfile.Bio = updatedProfile.Bio;
                userProfile.Birthday = updatedProfile.Birthday;
                // userProfile.ProfilePicture = updatedProfile.ProfilePicture;

                // Save changes to the database
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

        public List<Book> GetBooksInList(int BookListID)
        {
            List<int> BookID = _dataContext.BookInLists.Where(bl => bl.BookListID == BookListID).Select(bl => bl.BookID).ToList();

            List<Book> bookInList = _dataContext.Books.Where(book => BookID.Contains(book.BookID)).ToList();

            return bookInList;
        }

        //public List<Book> GetBooksInUserList(int ListID)
        //{
        //    // retrieves a list of BookID values from the BookLists table in the data context.
        //    List<int> BookID = _dataContext.BookLists.Where(bl => bl.LisID == ListID).Select(bl => bl.BookID).ToList();

        //    List<Book> bookInList = _dataContext.Books.Where(book => BookID.Contains(book.BookID)).ToList();

        //    return bookInList;
        //}
    }
}
