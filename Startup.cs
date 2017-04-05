using FisherInsuranceApi.Data;
using FisherInsuranceApi.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;

namespace FisherInsuranceApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireNonAlphanumeric = false;
                config.Cookies.ApplicationCookie.AutomaticChallenge = false;
            })
            .AddEntityFrameworkStores<FisherContext>()
            .AddDefaultTokenProviders();

            services.AddDbContext<FisherContext>();
            services.AddSingleton<DbSeeder>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DbSeeder dbSeeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            

            app.UseDefaultFiles();
            app.UseStaticFiles();

            //add our JwtProvider that we created
            app.UseJwtProvider();

            //add the built in authentication 
             app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = JwtProvider.SecurityKey,
                    ValidIssuer = JwtProvider.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
            });

            //if we wanted to support cookies, we do this:
            //app.UseIdentity();
            //or this
            //app.UseCookieAuthentication();

            app.UseMvc();

            try{
                dbSeeder.SeedAsync().Wait();
            }
            catch (AggregateException e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}

