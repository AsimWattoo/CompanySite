using Microsoft.AspNetCore.Routing.Constraints;

namespace Company_Site.ViewModels
{
    public class StatementOfAccountViewModel
    {
        public int SRNo { get; set; }

        public string Trust_Code { get; set; }

        public string Trust_Name { get; set; }

        public string Borrower_Code { get; set; }

        public string Account_Number { get; set; }

        public string Account_Name { get; set; }

        public string Bank_Name { get; set; }

        public string Facility { get; set; }

        public DateTime SOADate { get; set; }

        public double Opening_Balance { get; set; }

        public double Interest { get; set; }

        public double Penal_Interest { get; set; }

        public double Penal_Percent { get; set; }

        public string Frequency { get; set; }

        public string Calculation { get; set; }

        public string Note { get; set; }

        public DateTime ClosedDate { get; set; }

        public string Maker { get; set; }

        public string Checker { get; set; }

        public DateTime EntryTime { get; set; }

        public string StatusAs { get; set; }
    }
}
