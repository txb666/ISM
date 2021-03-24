using ISM.WebApp.DAO;
using ISM.WebApp.DAOImpl;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.Cookie.Name = "ISMCookie";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                });
            services.AddSession(options =>
            {
                options.Cookie.Name = "ISMSession";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.IsEssential = true;
            });
            services.AddControllersWithViews();
            services.AddScoped<RoleDAO, RoleDAOImpl>();
            services.AddScoped<UserDAO, UserDAOImpl>();
            services.AddScoped<AccountDAO, AccountDAOImpl>();
            services.AddScoped<StudentGroupDAO, StudentGroupDAOImpl>();
            services.AddScoped<PassportDAO, PassportDAOImpl>();
            services.AddScoped<ProgramDAO, ProgramDAOImpl>();
            services.AddScoped<CampusDAO, CampusDAOImpl>();
            services.AddScoped<VisaDAO, VisaDAOImpl>();
            services.AddScoped<VisaLetterDAO, VisaLettercsDAOImpl>();
            services.AddScoped<InsuranceDAO, InsuranceDAOImpl>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

            var fsOptions = new FileServerOptions();
            fsOptions.StaticFileOptions.OnPrepareResponse = (context) =>
            {
                // Disable caching of all static files.
                context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                context.Context.Response.Headers["Pragma"] = "no-cache";
                context.Context.Response.Headers["Expires"] = "-1";
            };
            app.UseFileServer(fsOptions);
        }
    }
}
