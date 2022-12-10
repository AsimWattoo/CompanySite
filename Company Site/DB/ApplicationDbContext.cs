using Company_Site.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using File = Company_Site.Data.File;

namespace Company_Site.DB
{
	public class ApplicationDbContext : IdentityDbContext<User, UserRole, int>
	{
		#region DbSets

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

		public DbSet<AccountDetailsModel> AccountDetails { get; set; }

		public DbSet<BorrowerDetail> BorrowerDetails { get; set; }

		public DbSet<DebtProfileModel> DebtProfiles { get; set; }

		public DbSet<Employee> Employees { get; set; }
		
		public DbSet<FinancialDataModel> FinancialDatas { get; set; }

		public DbSet<CashFlowProjectsModel> CashFlows { get; set; }

		public DbSet<DocumentsListsModel> DocumentLists { get; set; }

		public DbSet<TrustRelationModel> TrustRelations { get; set; }

		public DbSet<DistributionEntry> DistributionEnteries { get; set; }

		public DbSet<InterestRateChangeModel> InterestRateChangeEntries { get; set; }

		public DbSet<DisburmentModel> Disburments { get; set; }

		public DbSet<ResolutionStatusModel> ResolutionStatusModels { get; set; }

		public DbSet<NcltNetModel> NcltNets { get; set; }

		public DbSet<RADetailsModel> RADetails { get; set; }

		public DbSet<ClaimsAndShareModel> ClaimsAndShares { get; set; }

		public DbSet<TimelineModel> Timelines { get; set; }

		public DbSet<AccountNetModel> AccountNets { get; set; }

		public DbSet<ContactsModel> Contacts { get; set; }

		public DbSet<ContactAccessGrantModel> AccessGrants { get; set; }

		public DbSet<ContactAccountDetailsModel> ContactAccountDetails { get; set; }

		public DbSet<ContactPaymentHistoryModel> ContactPaymentHistories { get; set; }

		public DbSet<EventTimelineModel> EventTimeLines { get; set; }

		public DbSet<ApprovalsAndMemoModel> ApprovalsAndMemoModels { get; set; }

		public DbSet<NoticeAutomationModel> NoticeAutomations { get; set; }

		public DbSet<DraftModel> SavedDrafts { get; set; }

		public DbSet<AssetDetailsModel> AssetDetails { get; set; }

		public DbSet<AuctionDetails> AuctionDetails { get; set; }

		public DbSet<ValuationDetails> ValuationDetails { get; set; }

		public DbSet<ExpenseAdjustmentModel> ExpenseAdjustments { get; set; }

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

			builder.Entity<Trust>()
				.HasIndex(f => f.TrustCode)
				.IsUnique();

			builder.Entity<Account>()
				.HasIndex(f => f.BorrowerCode)
				.IsUnique();
		}

		#endregion
	}
}
