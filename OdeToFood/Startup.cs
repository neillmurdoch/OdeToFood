using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGreeter, Greeter>();
            // Scope to entirity of http request
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            // One list...NOT a good idea when multi user, but ok for test
            services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
                              IHostingEnvironment env, 
                              IGreeter greeter,
                              ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                // Interrogate this variable to get what environment we are running on. This comes from an environment variable called ASPNETCORE_ENVIRONMENT which 
                // can be set in launchSettings.json. These profiles can also be set in OdeToFood project settings\Debug tab.

                // env.EnvironmentName
                app.UseDeveloperExceptionPage();
            }

            // All these app. calls are to various types of 'middleware' that the request gets routed through

#region Sample Code
            /*
            //app.UseDefaultFiles();

            //app.UseStaticFiles();

            // Can use this, to do both of the above.
            app.UseFileServer();


            //app.Use(next =>
            //{
            //    return async context =>
            //    {
            //        logger.LogInformation("Request incoming");
            //        if (context.Request.Path.StartsWithSegments("/mym"))
            //        {
            //            await context.Response.WriteAsync("Hit!!");
            //            logger.LogInformation("Request handled");
            //        }
            //        else
            //        {
            //            await next(context);
            //            logger.LogInformation("Response outgoing");
            //        }
            //    };
            //});


            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path="/wp"
            //});

    */
#endregion

            app.UseStaticFiles();

            //app.WithDefaultRoute();
            app.UseMvc(ConfigureRoutes); 

            app.Run(async (context) =>
            {
                //throw new Exception("error!!");

                var greeting = greeter.GetMessageOfTheDay();
                //await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not found");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index          "{controller}/{action}");
            // admin/Home/Index     "admin/{controller}/{action}");     - Using literal text 'admin'
            // "{controller}/{action}{id?}");                           - With option id parameter. ? makes it optional
            // "{controller=Home}/{action=Index}/{id?}");               - Adding default values for controller and action so root of website will go to the HomeController

            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
