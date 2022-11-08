using Company_Site.Data;

using File = Company_Site.Data.File;

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

		public DbSet<Memo> Memos { get; set; }

		public DbSet<File> Files { get; set; }

		public DbSet<UserMemoReference> MemoReferences { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Vendor> Vendors { get; set; }

		public DbSet<ExpenseEntry> Expenses { get; set; }

		public DbSet<CollectionEntry> Collections { get; set; }

		public DbSet<Trust> Trusts { get; set; }

		public DbSet<CompanyPolicy> Policies { get; set; }

		public DbSet<Account> Accounts { get; set; }

		public DbSet<BorrowerDetail> BorrowerDetails { get; set; }

		public DbSet<DebtProfileModel> DebtProfiles { get; set; }

<<<<<<< HEAD
		public DbSet<Employee> Employees { get; set; }

=======
		public DbSet<FinancialDataModel> FinancialDatas { get; set; }

		public DbSet<CashFlowProjects> CashFlows { get; set; }

		public DbSet<DocumentListModel> DocumentLists { get; set; }
>>>>>>> 2384d4c727356542d77c5cd3dd9d2e2aa169c4e7
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

		#region Overriden Methods

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<UserMemoReference>()
				.HasKey("Id", "MemoId", "UserId");

			builder.Entity<File>()
				.HasKey(f => f.FileId);

			builder.Entity<Comment>()
				.HasKey(k => k.CommentId);
		}

		#endregion
	}
}
