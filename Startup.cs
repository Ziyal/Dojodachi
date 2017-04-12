using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace YourNamespace
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMvc();
        }
         
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            loggerFactory.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseSession();
            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    name: "Default", // The route's name is only for our own reference
                    template: "", // The pattern that the route matches
                    defaults: new {controller = "Hello", action = "Index"} // The controller and method to execute
                );
            });
        }
    }
}