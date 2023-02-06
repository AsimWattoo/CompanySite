using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company_Site.Helpers
{
	public static class Seeder
	{

		public static async void SeedData(WebApplication app)
		{
			UserManager<User>? userManager;
			ApplicationDbContext? dbContext;
			RoleManager<UserRole> roleManager;
			using (IServiceScope scope = app.Services.CreateScope())
			{
				userManager = scope.ServiceProvider.GetService<UserManager<User>>();
				dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
				roleManager = scope.ServiceProvider.GetService<RoleManager<UserRole>>();

				if (userManager == null)
				{
					Console.WriteLine("--> Unable to seed data. No user manager found");
					return;
				}
				else if (dbContext == null)
				{
					Console.WriteLine("--> Unable to seed data. No db context found");
					return;
				}

				//Performing migration
                try
                {
					Console.WriteLine("--> Creating Data");
					dbContext.Database.EnsureCreated();
                }
                catch (Exception exc)
                {
                    Console.WriteLine($"--> Error while seeding data: {exc.ToString()}");
                    return;
                }

                //No user exists then create a user as admin
                if (!userManager.Users.Any())
				{
                    Console.WriteLine("--> Seeding Data");
					User user = new User()
					{
						UserName = "ParasGandhi",
						Email = "admin@debtnet.in",
						Department = "Legal",
						Designation="CEO",
						OfficeLocation = "Office",
						FirstName = "Paras",
						LastName="Gandhi",
                        Role = "Admin",
						Id = Guid.NewGuid().ToString(),
						Address = "",
						City = "",
						State = "",
						MobileNumber = "",
						EmployeeCode = ""
					};

					IdentityResult res = await userManager.CreateAsync(user, "pass@1234");

					if (res.Succeeded)
						Console.WriteLine("--> Successfully created a user");
					else
					{
						Console.WriteLine($"--> Failed to create a user: {res.Errors.First().Description}");
						return;
					}
				}
			}

		}

	}
}
