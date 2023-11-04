﻿using Microsoft.AspNetCore.Identity;
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

        public BookList GetBookListForUser(int UserID)
        {
            return _dataContext.BookLists
                .Include(bl => bl.BookInLists)
                    .ThenInclude(bil => bil.Book)
                .Include(bl => bl.BookInLists)
                    .ThenInclude(bil => bil.Status)
                .FirstOrDefault(bl => bl.UserID == UserID);
        }

    }
}
