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
                );
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
