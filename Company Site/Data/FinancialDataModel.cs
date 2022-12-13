using Company_Site.Base;


namespace Company_Site.Data
{
    public class FinancialDataModel : BaseModel<int>
    {
        /// <summary>
        /// The borrower to which this is related
        /// </summary>
        public int BorrowerCode { get; set; }

        public int Year { get; set; }

        public double Equity { get; set; }

        public double Reserves { get; set; }

        public double Networth => Equity + Reserves;

        public double LTB { get; set; }

        public double STB { get; set; }

        public double Other_Current_Liabilities { get; set; }

        public double TotalLiabilities => LTB + STB + Other_Current_Liabilities;

        public double Fixed_Assets { get; set; }

        public double Total_Non_Current_Assets { get; set; }

        public double Total_Current_Assets { get; set; }

        public double Total_Assets => Fixed_Assets + Total_Non_Current_Assets + Total_Non_Current_Assets;

        public double Revenue { get; set; }

        public double Expenses { get; set; }

        public double Operating_and_Direct_Expenses { get; set; }

        public double Changes_in_inventries { get; set; }

        public double Employee_Benefit_Expenses { get; set; }

        public double Other_Expenses { get; set; }

        public double EBDITA => Revenue + Expenses + Operating_and_Direct_Expenses + Changes_in_inventries + Employee_Benefit_Expenses + Other_Expenses;

        public double EBDITA_per { get; set; }

        public double Total_Expenses => EBDITA + EBDITA_per;

        public double Finance_Costs { get; set; }

        public double Depreciation_And_Amortisation_Expenses { get; set; }

        public double Exceptional_items { get; set; }

        public double Tax { get; set; }

        public double Profit_Loss => Finance_Costs + Depreciation_And_Amortisation_Expenses + Exceptional_items + Tax;

    }
}
