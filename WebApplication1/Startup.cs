using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Models;
using WebApplication1.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;



namespace WebApplication1
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
            //Lets register the Voter Repository so that it could be used in VoterController
            services.AddTransient<IRepository<Voter>, VoterRepository>();
            //Lets register the State Repository so that it could be used in StateController
            services.AddTransient<IRepository<State>, StateRepository>();
            //Lets register the Candidate Repository so that it could be used in CandidateRepository
            services.AddTransient<IRepository<Candidate>, CandidateRepository>();
            //Lets register the Vote Repository so that it could be used in VoteRepository
            services.AddTransient<IRepository<Vote>, VoteRepository>();
            //Lets register the Election Repository so that it could be used in ElectionRepository
            services.AddTransient<IRepository<Election>, ElectionRepository>();
            //Lets register Memory Cach services
            services.AddMemoryCache();


            //1- in IdentityHostingStartup.cs file:
            //I registered IdentityDbContext service
            //2- in Startup.cs file:
            //I registered the DefaultIdentity, the Roles and the EntityFrameworkstores
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>();

            services.AddDbContext<VoteDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VoteDBConnection")));

            services.AddControllersWithViews(
                o =>
                {
                    //Create a policy that accepts only athenticated users
                    var mySinglePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                    //adding the created policy as a filter
                    o.Filters.Add(new AuthorizeFilter(mySinglePolicy));
                }
                ).AddViewLocalization(options => { options.ResourcesPath = "Ressources"; }).AddDataAnnotationsLocalization(); ;
            services.AddRazorPages();

            services.AddAuthorization(
                //we are going to add the policies with their roles
                //'ManageElections' policy is going to have 'Administrator' role
                //'DoVote' policy is going to have 'Voter' role
                o =>
                {
                    //o.AddPolicy(policyName, policyBuilder);
                    o.AddPolicy(VoteAppPolicies.ManageElections.ToString(),
                        pb =>
                        {
                            pb.RequireAuthenticatedUser()
                            .RequireRole("Administrator")
                            //.RequireRole("Administrator", "RefAdmin", "Administrateurs")
                            .Build();
                        }
                        );

                    //o.AddPolicy(policyName, policyBuilder);
                    o.AddPolicy(VoteAppPolicies.DoVote.ToString(),
                        pb =>
                        {
                            pb.RequireAuthenticatedUser()
                            .RequireRole("Voter")
                            .Build();
                        }
                        );
                }
                );


            //Configure LocalizationOptions to use RessourcesPath "Ressourses" in the project
            services.Configure<LocalizationOptions>(options => options.ResourcesPath = "Ressources");

            //Configureing RequestLocalizationOptions
            var cultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("fr")
            };
            services.Configure<RequestLocalizationOptions>(options =>
            {
                //set the supported cultures list
                options.SupportedCultures = cultures;

                //set the SupportedUICultures list
                options.SupportedUICultures = cultures;

                //define DefaultRequestCulture to be en-EN
                options.DefaultRequestCulture = new RequestCulture("en-EN");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //the below middleware is one of the diagnosis middlewares and it redirect the user to the welcome page whatever
            //the action he's trying to do
            //app.UseWelcomePage();


            //add RequestLocalization middleware
            app.UseRequestLocalization();



            //the below middleware is one of the diagnosis middlewares and it displays the below text with the http response code as a plain text            
            app.UseStatusCodePages("text/plain", "Status Code Page, status code:{0}");
            //the placeholder {0} reresents the http response status code

            
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
