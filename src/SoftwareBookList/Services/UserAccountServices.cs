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
	}
}
