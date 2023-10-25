using SoftwareBookList.Data;
using SoftwareBookList.Models;

namespace SoftwareBookList.Services
{
	public class UserAccountServices
	{
		// Dependency Injection to provide the UserAccountService with the necessary dependencies it needs to work.
		private readonly DataContext _dataContext;

		public UserAccountServices(DataContext dataContext) 
		{
			_dataContext = dataContext;
		}

		public User AddUser(User user)
		{
			// adds it to the database
			_dataContext.Users.Add(user);
			_dataContext.SaveChanges();
			return user;
		}

		public User GetUser(string emailAddress)
		{
			// Ensure the email address is in lowercase for comparison
			string lowercaseEmail = emailAddress.ToLower();

			// Use LINQ to query the Users DbSet based on the lowercase email
			User user = _dataContext.Users.FirstOrDefault(u => u.EmailAddress.ToLower() == lowercaseEmail);

			return user;
		}

		public string GenerateRandomUserName()
		{
			Random random = new Random();

			string username = "user" + random.Next(10000, 99999).ToString();

			return username;
		}

		public bool IsUserNameUnique(string username)
		{
			// Check if there are any users in the database whose UserName property matches the provided username.
			bool isUnique = !_dataContext.Users.Any(u => u.UserName == username);

			// If no matching user with the same username is found, isUnique will be true.
			// Otherwise, if a user with the same username exists, isUnique will be false.
			return isUnique;
		}

		public string GenerateRandomListName()
		{
			Random random = new Random();

			string listName = "List" + random.Next(10000, 99999).ToString();

			return listName;
		}

		public void CreateList(User user)
		{
			string listName = GenerateRandomListName();

			List newList = new List
			{
				Name = listName,
				User = user,
				UserID = user.UserID
			};

			_dataContext.Lists.Add(newList);
			_dataContext.SaveChanges();
		}

	}
}
