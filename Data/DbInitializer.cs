using Projektna.Models;
using System.Diagnostics;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Projektna.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            //context.Database.EnsureCreated();
            
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer{CustomerFirstName="Carson",CustomerLastName="Alexander"},
                new Customer{CustomerFirstName="Meredith",CustomerLastName="Alonso"},
                new Customer{CustomerFirstName="Arturo",CustomerLastName="Anand"},
                new Customer{CustomerFirstName="Gytis",CustomerLastName="Barzdukas"},
                new Customer{CustomerFirstName="Laura",CustomerLastName="Norman"}
            };

            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var sellers = new Seller[]
            {
                new Seller{SellerFirstName="Yan", SellerLastName="Li"},
                new Seller{SellerFirstName="Peggy", SellerLastName="Justice"},
                new Seller{SellerFirstName="Laura", SellerLastName="Norman"}
            };

            foreach (Seller s in sellers)
            {
                context.Sellers.Add(s);
            }
            context.SaveChanges();

            var branches = new Branch[]
            {
                new Branch{BranchName="Kranj", Address="", SellerID=1, PhoneNumber=111111111, Email="kranj@carstore.si"},
                new Branch{BranchName="Koper", Address="", SellerID=2, PhoneNumber=222222222, Email="koper@carstore.si"},
                new Branch{BranchName="Ljubljana", Address="", SellerID=3, PhoneNumber=333333333, Email="ljubljana@carstore.si"}
            };

            foreach (Branch b in branches)
            {
                context.Branches.Add(b);
            }
            context.SaveChanges();

            var trims = new Trim[]
            {
                new Trim{Name="Standard", AddPrice=0.0},
                new Trim { Name = "Sport", AddPrice = 400.0},
                new Trim{Name="Luxury", AddPrice=600.0}
            };
            foreach (Trim t in trims)
            {
                context.Trims.Add(t);
            }
            context.SaveChanges();

            var vehicles = new Vehicle[]
            {
                new Vehicle{Brand="Honda", Model="Civic", ModelYear=DateTime.Parse("2022-01-01"), TrimID=1},
                new Vehicle{Brand="Toyota", Model="Yaris", ModelYear=DateTime.Parse("2021-01-01"), TrimID=2},
                new Vehicle{Brand="Toyota", Model="Prius", ModelYear=DateTime.Parse("2018-01-01"), TrimID=3},
                new Vehicle{Brand="Toyota", Model="Hilux", ModelYear=DateTime.Parse("2018-01-01"), TrimID=1, Sold = true}
            };
            foreach (Vehicle v in vehicles)
            {
                context.Vehicles.Add(v);
            }
            context.SaveChanges();

            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="Seller"},
                new IdentityRole{Id="3", Name="Customer"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();

            var user = new ApplicationUser
            {
                FirstName = "John",
                LastName = "Doe",
                City = "Ljubljana",
                Email = "jd@store.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "jd@store.com",
                NormalizedUserName = "jd@store.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"passwd");
                user.PasswordHash = hashed;
                context.Users.Add(user);
                
            }

            context.SaveChanges();
            

            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);
            }



            context.SaveChanges();

        }
    }
}