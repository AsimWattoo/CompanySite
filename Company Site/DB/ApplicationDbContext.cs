using Company_Site.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Company_Site.DB
{
	public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
	{
		#region DbSets

		/// <summary>
		/// The list of access for each user defined for pages
		/// </summary>
		public DbSet<UserAccess> AccessList { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor to initialize database
		/// </summary>
		/// <param name="options"></param>
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		#endregion
	}
}
