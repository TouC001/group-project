using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoftwareBookList.Data;
using SoftwareBookList.GoogleBooks;
using SoftwareBookList.Models;
using SoftwareBookList.Services;

namespace SoftwareBookList
{
    /// <summary>
    /// This class is responsible for the main configuration
    /// and setting up services (like our API).
    /// </summary>
    public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		// ConfigureServices is where application services are configured and added to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Configure services required by the application
			services.AddControllersWithViews();

			// Configure GoogleBooksSettings from appsettings.json
			services.Configure<GoogleBooksSettings>(_configuration.GetSection("GoogleBooksSettings"));

			// Register HttpClient wtih Google Books
			services.AddHttpClient<GoogleBooksService>();

			// Add other service configurations here, e.g., database, authentication
			services.AddDbContext<DataContext>((d) => d.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

			// Configure authentication services in the application.
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(options =>
			{
				// Define the path for the login page where users will be redirected if they need to authenticate.
				options.LoginPath = "/"; // I'll need to finish this later.

				// Define the path for the logout page where users will be redirected after logging out.
				options.LogoutPath = "/"; //I'll need to finish this later.

				// Define the path for the access denied page where users will be redirected if they don't have the necessary permissions.
				options.AccessDeniedPath = "/access-denied";

				// Specify the name of the authentication cookie used to store user authentication information.
				options.Cookie.Name = "UserAuth";

				// Set the expiration time for the authentication cookie. In this case, it's set to 2 days.
				options.ExpireTimeSpan = TimeSpan.FromDays(2);

				// Enable sliding expiration, which means that the expiration time of the cookie will be refreshed on each request,
				// as long as the user is active. If the user remains idle, the cookie will eventually expire.
				options.SlidingExpiration = true;
			});

			services.AddTransient<UserAccountServices>();
			services.AddTransient<BookMappingService>();
				
		}

		// Configure is where the application's request pipeline and middleware are set up.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				// Development mode exception page for detailed error information
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// Production mode error handling
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts(); // Enable HTTP Strict Transport Security for added security

				// Consider adding additional production-specific configurations here
			}

			// Enable HTTPS redirection for added security
			app.UseHttpsRedirection();

			// Serve static files (e.g., CSS, JavaScript, images)
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			// Configure endpoints for controllers and actions
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});	
		}
	}
}
