using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company_Site.Migrations
{
    public partial class TrustUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessList",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Page = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    Officers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plant_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfDefault = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolutionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Products = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Payment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Constitution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operating_Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NPA_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acquisition_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Statutory_Dues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasuresOfReconstruction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DD_Official = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DD_Firm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DD_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason_NPA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Case_Exit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Case_Exit_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfJoining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Access = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aadhar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wilful_Defaulter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Net_Worth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfShares = table.Column<long>(type: "bigint", nullable: false),
                    WillFullAsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NetWorthAsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PercentOfShareHeld = table.Column<double>(type: "float", nullable: false),
                    Additional_Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowerDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashFlows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quarter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false),
                    facilty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    source = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Borrower = table.Column<int>(type: "int", nullable: false),
                    Trust_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BorrowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditAmount = table.Column<double>(type: "float", nullable: false),
                    CreditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfRecovery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KYC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoneSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoneSellerShare = table.Column<double>(type: "float", nullable: false),
                    AdjustToward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proportionately = table.Column<bool>(type: "bit", nullable: false),
                    Distribution_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionSubEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionEntryId = table.Column<int>(type: "int", nullable: false),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrustName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Share = table.Column<float>(type: "real", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionSubEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    MemoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                });

            migrationBuilder.CreateTable(
                name: "DebtProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false),
                    LenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Facility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    NPA_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    POS = table.Column<double>(type: "float", nullable: false),
                    OS_Interest = table.Column<double>(type: "float", nullable: false),
                    BaseInterestRate = table.Column<double>(type: "float", nullable: false),
                    Spread = table.Column<double>(type: "float", nullable: false),
                    Calculation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PenalAmount = table.Column<double>(type: "float", nullable: false),
                    PenalInterest = table.Column<double>(type: "float", nullable: false),
                    Tenor = table.Column<int>(type: "int", nullable: false),
                    POS_AsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMI_Installment = table.Column<double>(type: "float", nullable: false),
                    EMI_OS = table.Column<double>(type: "float", nullable: false),
                    Res_Facility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Res_ImplementationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Res_TenorMonths = table.Column<int>(type: "int", nullable: false),
                    Res_EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Res_POS = table.Column<double>(type: "float", nullable: false),
                    Res_Interest = table.Column<double>(type: "float", nullable: false),
                    Res_POSAsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Res_PrincipleMonutorium = table.Column<double>(type: "float", nullable: false),
                    Res_InterestCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Res_PrincipalFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Res_InterestRate = table.Column<double>(type: "float", nullable: false),
                    Res_DefaultInterest = table.Column<double>(type: "float", nullable: false),
                    Res_BulletPayment = table.Column<double>(type: "float", nullable: false),
                    Res_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Res_Upfront = table.Column<double>(type: "float", nullable: false),
                    Res_InterestMonutorium = table.Column<double>(type: "float", nullable: false),
                    Res_Terms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOS = table.Column<double>(type: "float", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributionEnteries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trust_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trust_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SRIssued = table.Column<int>(type: "int", nullable: false),
                    SROutStanding = table.Column<int>(type: "int", nullable: false),
                    Opening = table.Column<double>(type: "float", nullable: false),
                    Collections = table.Column<double>(type: "float", nullable: false),
                    TSFee = table.Column<double>(type: "float", nullable: false),
                    RFees = table.Column<double>(type: "float", nullable: false),
                    CFee = table.Column<double>(type: "float", nullable: false),
                    OtherExpense = table.Column<double>(type: "float", nullable: false),
                    Adjustment = table.Column<double>(type: "float", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    TDS_Decimal = table.Column<double>(type: "float", nullable: false),
                    Advance_TDS_Decimal = table.Column<double>(type: "float", nullable: false),
                    Distribution_AMT = table.Column<double>(type: "float", nullable: false),
                    NormalDist = table.Column<double>(type: "float", nullable: false),
                    Surplus = table.Column<double>(type: "float", nullable: false),
                    Provision = table.Column<double>(type: "float", nullable: false),
                    ClosingBalance = table.Column<double>(type: "float", nullable: false),
                    OPFV = table.Column<double>(type: "float", nullable: false),
                    CLFV = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionEnteries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doc_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Document_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stamp_Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Original_Held_With = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Execution_Place = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandlineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Access = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Borrower_Code = table.Column<int>(type: "int", nullable: false),
                    Trust_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distribution_Id = table.Column<int>(type: "int", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillAmount = table.Column<double>(type: "float", nullable: false),
                    Vendor_Id = table.Column<int>(type: "int", nullable: false),
                    Vendor_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Proportionately = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equity = table.Column<long>(type: "bigint", nullable: true),
                    Reserves = table.Column<long>(type: "bigint", nullable: true),
                    Networth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LTB = table.Column<long>(type: "bigint", nullable: true),
                    STB = table.Column<long>(type: "bigint", nullable: true),
                    Other_Current_Liabilities = table.Column<long>(type: "bigint", nullable: true),
                    Total_liabilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fixed_Assets = table.Column<long>(type: "bigint", nullable: true),
                    Total_Non_Current_Assets = table.Column<long>(type: "bigint", nullable: true),
                    Total_Current_Assets = table.Column<long>(type: "bigint", nullable: true),
                    Total_Assets = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revenue = table.Column<long>(type: "bigint", nullable: true),
                    Operating_and_Direct_Expenses = table.Column<long>(type: "bigint", nullable: true),
                    Changes_in_inventries = table.Column<long>(type: "bigint", nullable: true),
                    Employee_Benefit_Expenses = table.Column<long>(type: "bigint", nullable: true),
                    Other_Expenses = table.Column<long>(type: "bigint", nullable: true),
                    EBDITA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EBDITA_per = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total_Expenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finance_Costs = table.Column<long>(type: "bigint", nullable: true),
                    Depreciation_And_Amortisation_Expenses = table.Column<long>(type: "bigint", nullable: true),
                    Exceptional_items = table.Column<long>(type: "bigint", nullable: true),
                    Tax = table.Column<long>(type: "bigint", nullable: true),
                    Profit_Loss = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MemoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemoStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoReferences", x => new { x.Id, x.MemoId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Memos",
                columns: table => new
                {
                    MemoNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WriterId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Financial = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Periodicity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToId = table.Column<int>(type: "int", nullable: false),
                    ThroughId = table.Column<int>(type: "int", nullable: false),
                    FromId = table.Column<int>(type: "int", nullable: false),
                    ValidTill = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemoStatus = table.Column<int>(type: "int", nullable: false),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memos", x => x.MemoNumber);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trusts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrustCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Trust_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SRIssued = table.Column<int>(type: "int", nullable: false),
                    SRIssuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SRHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrusteeshipBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Face_Value = table.Column<int>(type: "int", nullable: false),
                    IssuerShare = table.Column<double>(type: "float", nullable: false),
                    HolderShare = table.Column<double>(type: "float", nullable: false),
                    TrusteeshipLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionFeeBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrustSetupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NavBand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerUpsideShare = table.Column<double>(type: "float", nullable: false),
                    HolderUpsideShare = table.Column<double>(type: "float", nullable: false),
                    TrusteeshipCollection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionFee = table.Column<double>(type: "float", nullable: false),
                    TrustDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchNameAndAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IFSCCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SROs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionFee = table.Column<double>(type: "float", nullable: false),
                    InterestMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    T_Year1 = table.Column<double>(type: "float", nullable: false),
                    T_Year2 = table.Column<double>(type: "float", nullable: false),
                    T_Year3 = table.Column<double>(type: "float", nullable: false),
                    T_Year4 = table.Column<double>(type: "float", nullable: false),
                    T_Year5 = table.Column<double>(type: "float", nullable: false),
                    T_Year6 = table.Column<double>(type: "float", nullable: false),
                    T_Year7 = table.Column<double>(type: "float", nullable: false),
                    T_Year8 = table.Column<double>(type: "float", nullable: false),
                    T_Year9 = table.Column<double>(type: "float", nullable: false),
                    R_Year1 = table.Column<double>(type: "float", nullable: false),
                    R_Year2 = table.Column<double>(type: "float", nullable: false),
                    R_Year3 = table.Column<double>(type: "float", nullable: false),
                    R_Year4 = table.Column<double>(type: "float", nullable: false),
                    R_Year5 = table.Column<double>(type: "float", nullable: false),
                    R_Year6 = table.Column<double>(type: "float", nullable: false),
                    R_Year7 = table.Column<double>(type: "float", nullable: false),
                    R_Year8 = table.Column<double>(type: "float", nullable: false),
                    R_Year9 = table.Column<double>(type: "float", nullable: false),
                    PanCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SRInvestor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvestorShare = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trusts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrustRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrustId = table.Column<int>(type: "int", nullable: true),
                    TrustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    BorrowerCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrustRelations_Trusts_TrustId",
                        column: x => x.TrustId,
                        principalTable: "Trusts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BorrowerCode",
                table: "Accounts",
                column: "BorrowerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TrustRelations_TrustId",
                table: "TrustRelations",
                column: "TrustId");

            migrationBuilder.CreateIndex(
                name: "IX_Trusts_TrustCode",
                table: "Trusts",
                column: "TrustCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessList");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BorrowerDetails");

            migrationBuilder.DropTable(
                name: "CashFlows");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionSubEntries");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DebtProfiles");

            migrationBuilder.DropTable(
                name: "DistributionEnteries");

            migrationBuilder.DropTable(
                name: "DocumentLists");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "FinancialDatas");

            migrationBuilder.DropTable(
                name: "MemoReferences");

            migrationBuilder.DropTable(
                name: "Memos");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "TrustRelations");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Trusts");
        }
    }
}
