using Microsoft.AspNetCore.Routing.Constraints;

namespace Company_Site.ViewModels
{
    public class StatementOfAccountViewModel
    {
        public string Trust_Code { get; set; }

        public string Trust_Name { get; set; }

        public int Borrower_Code { get; set; }

        public int Account_Number { get; set; }

        public string Account_Name { get; set; }

        public string Bank_Name { get; set; }

        /// <summary>
        /// From Debt Profile Restructuring
        /// </summary>
        public string Facility { get; set; }

        public DateTime SOADate { get; set; } = DateTime.Now;

        /// <summary>
        /// Same as distrbution table
        /// </summary>
        public double Opening_Balance { get; set; }

        /// <summary>
        /// From Debt Profile
        /// </summary>
        public double Interest { get; set; }

        /// <summary>
        /// From Debt Profile
        /// </summary>
        public double Penal_Interest { get; set; }

        public double Penal_Percent { get; set; }

        /// <summary>
        /// From Debt Profile
        /// </summary>
        public string Frequency { get; set; }

        /// <summary>
        /// From Debt Profile Page
        /// </summary>
        public string Calculation { get; set; }

        /// <summary>
        /// From Debt Profile Page
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// End Date from debt profile
        /// </summary>
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// From debt profile
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// From debt profile
        /// </summary>
        public string Checker { get; set; }

        /// <summary>
        /// Debt Profile
        /// </summary>
        public string StatusAs { get; set; }
    }
}
