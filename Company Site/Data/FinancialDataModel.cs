using Company_Site.Base;
using System;
using System.ComponentModel.DataAnnotations;


namespace Company_Site.Data
{
    public class FinancialDataModel : BaseModel
    {
        public string? Year { get; set; }

        public long? Equity { get; set; }

        public long? Reserves { get; set; }

        public string? Networth { get; set; }

        public long? LTB { get; set; }

        public long? STB { get; set; }

        public long? Other_Current_Liabilities { get; set; }

        public string? Total_liabilities { get; set; }

        public long? Fixed_Assets { get; set; }

        public long? Total_Non_Current_Assets { get; set; }

        public long? Total_Current_Assets { get; set; }

        public string? Total_Assets { get; set; }

        public long? Revenue { get; set; }

        public long? Operating_and_Direct_Expenses { get; set; }

        public long? Changes_in_inventries { get; set; }

        public long? Employee_Benefit_Expenses { get; set; }

        public long? Other_Expenses { get; set; }

        public string? EBDITA { get; set; }

        public string? EBDITA_per { get; set; }

        public string? Total_Expenses { get; set; }

        public long? Finance_Costs { get; set; }

        public long? Depreciation_And_Amortisation_Expenses { get; set; }

        public long? Exceptional_items { get; set; }

        public long? Tax { get; set; }

        public string? Profit_Loss { get; set; }

    }
}
