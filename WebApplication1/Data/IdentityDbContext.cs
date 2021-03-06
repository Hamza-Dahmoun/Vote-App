﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class IdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //calling the base method to avoid having the following error when adding a new migration:
            //The entity type 'IdentityUserLogin<string>' requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'.
            //But Why? According to Muhammad Adeel Zahid from Stackoverflow:
            //Basically the keys of Identity tables are mapped in OnModelCreating method of IdentityDbContext and if this method is not called, you will end up getting the error that you got. This method is not called if you derive from IdentityDbContext and provide your own definition of OnModelCreating as you did in your code. With this setup you have to explicitly call the OnModelCreating method of IdentityDbContext using base.OnModelCreating statement.
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Administrator' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole {Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });
            //Seeding a  'Voter' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2618884a-2189-48b5-a1ee-0077dcf92239", Name = "Voter", NormalizedName = "VOTER".ToUpper() });
            //Seeding a  'PreVoter' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "78962b34-8b8c-4059-837a-25a8941654c5", Name = "PreVoter", NormalizedName = "PreVoter".ToUpper() });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();


            //Seeding an admin user to AspNetUsers table
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "1788cddf-09e3-4ea6-ab17-133cf5b57310", // primary key
                    UserName = "myadmin",
                    NormalizedUserName = "MYADMIN",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                }
            );
            //Seeding the relation between our user and Admin role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                    UserId = "1788cddf-09e3-4ea6-ab17-133cf5b57310"
                }
            );


            //Seeding an admin user to AspNetUsers table
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "f304d35f-a3bd-4b63-bba3-3411331edc34", // primary key
                    UserName = "myvoter",
                    NormalizedUserName = "MYVOTER",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
                }
            );
            //Seeding the relation between our user and Voter role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2618884a-2189-48b5-a1ee-0077dcf92239",
                    UserId = "f304d35f-a3bd-4b63-bba3-3411331edc34"
                }
            );


        }
    }
}
