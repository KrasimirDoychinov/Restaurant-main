using Microsoft.AspNetCore.Identity;
using Restaurant.Data;
using Restaurant.Data.Models;
using System.Linq;

namespace Restaurant.Services.Seeder
{
    public static class DataSeeder
    {
        public static void SeedData(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedProducts(context);
            SeedCoupons(context);
        }

        public static void SeedProducts(ApplicationDbContext context)
        {
            if (context.Products.Count() <= 0)
            {
                context.Products.AddRange(new Product[]
                {
                    new Product{ Name = "Neapolitan Pizza", Price = 5, Weight = 250, Ingredients = "Flour, tomatoes, mozarella, meat", ProductType = Data.Enums.ProductType.MainDish, ImagePath = "pizza.jpeg", Id = "1"},
                    new Product{ Name = "French Fries", Price = 3, Weight = 150, Ingredients = "Potatoes, ketchup, cheese", ProductType = Data.Enums.ProductType.SideDish, ImagePath = "frenchFries.jpeg", Id = "2"},
                    new Product{ Name = "Pancakes", Price = 2, Weight = 100, Ingredients = "Milk, chocolate", ProductType = Data.Enums.ProductType.Desert, ImagePath = "pancake.jpeg", Id = "3"},
                    new Product{ Name = "Shopska salad", Price = 1, Weight = 400, Ingredients = "Tomatoes, cucumbers, cheese", ProductType = Data.Enums.ProductType.Salad, ImagePath = "shopskaSalad.jpeg", Id = "4"},
                    new Product{ Name = "Spaghetti", Price = 5, Weight = 250, Ingredients = "Flour, tomatoes, beef", ProductType = Data.Enums.ProductType.MainDish, ImagePath = "spaghetti.jpeg", Id = "5"},
                    new Product{ Name = "Cake", Price = 10, Weight = 500, Ingredients = "Chocolate, milk", ProductType = Data.Enums.ProductType.Desert, ImagePath = "cake.jpeg", Id = "6"},
                });
            }

            context.SaveChanges();
        }

        public static void SeedCoupons(ApplicationDbContext context)
        {
            if (context.Coupons.Count() <= 0)
            {
                context.Coupons.AddRange(new Coupon[]
                {
                    new Coupon{Name = "10discount", Discount = 10},
                    new Coupon{Name = "20discount", Discount = 20},
                    new Coupon{Name = "30discount", Discount = 30}
                });
            }

            context.SaveChanges();
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("TestUser1").Result == null)
            {
                var testUser = new User
                {
                    UserName = "TestUser1",
                    EmailConfirmed = true,
                    Email = "testUser@gmail.com"
                };

                var result = userManager.CreateAsync(testUser, "TestUser1.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(testUser, "User").Wait();
                }
            }

            if (userManager.FindByNameAsync("TestAdmin1").Result == null)
            {
                var testAdmin = new User
                {
                    UserName = "TestUser2",
                    EmailConfirmed = true,
                    Email = "testUser2@gmail.com"
                };

                var result = userManager.CreateAsync(testAdmin, "TestAdmin2.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(testAdmin, "User").Wait();
                }
            }

            if (userManager.FindByNameAsync("TestAdmin1").Result == null)
            {
                var testAdmin = new User
                {
                    UserName = "TestAdmin1",
                    EmailConfirmed = true,
                    Email = "testAdmin@gmail.com"
                };

                var result = userManager.CreateAsync(testAdmin, "TestAdmin1.").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(testAdmin, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var roleUser = new IdentityRole() { Name = "User" };
                roleManager.CreateAsync(roleUser).Wait();
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var roleAdmin = new IdentityRole() { Name = "Admin" };
                roleManager.CreateAsync(roleAdmin).Wait();
            }
        }
    }
}
