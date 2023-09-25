using SoftwareBookList.Services;

namespace SoftwareBookList
{
    /// <summary>
    /// This class is responsible for the main configuration
    /// and setting up services (like our API).
    /// </summary>
    public class Startup
    {
        // ConfigureServices is where application services are configured and added to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services required by the application
            services.AddControllersWithViews();

            // Register HttpClient wtih Google Books
            services.AddHttpClient<GoogleBooksService>();

            // Add other service configurations here, e.g., database, authentication
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
