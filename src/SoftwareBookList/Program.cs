using SoftwareBookList;
using SoftwareBookList.GoogleBooks.Tests;

/*
 * Program.cs is our entry point for our app.
 * We are creating a Host object. The host is an object that encapsulates
 * the app's litetime, configuration and services.
 * It's responsible for managing the startup, execution and shutdown of our app.
 */

var host = Host.CreateDefaultBuilder(args)
    // ConfigureWebHostDefaults sets up the web server, HTTP request processing and other web configurations.
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>(); // Use the Startup class to configure the application
    })
    // After we configure the host, we call Build() to create an instance of it (it is now prepared for execution).
    .Build();

host.Run(); // Start the application by running the host (after it's been built)

/*
 * The host will listen for incoming HTTP requests and handle them according to our configurations
 * we setup in Startup.cs.
 */
