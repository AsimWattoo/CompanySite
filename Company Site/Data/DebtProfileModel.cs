using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class DebtProfileModel : BaseModifierModel<int>
    {
        #region Debt Profile Fields

        /// <summary>
        /// The borrower code with which this debt profile is linked to
        /// </summary>
        [Required(ErrorMessage = "Borrower code is required")]
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Lender Name is required")]
        public string LenderName { get; set; }

        [Required(ErrorMessage = "Facility is required")]
        public string Facility { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        public int AccountNumber { get; set; }

        public DateTime NPA_Date { get; set; } = DateTime.Now;

        [Range(1, double.MaxValue, ErrorMessage = "POS is required")]
        public double POS { get; set; }

        public double OS_Interest { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Base Interest Rate is required")]
        public double BaseInterestRate { get; set; }

        [Required(ErrorMessage = "Spread is required")]
        public double Spread { get; set; }

        [Required(ErrorMessage = "Calculation is required")]
        public string? Calculation { get; set; }

        [Required(ErrorMessage = "Frequency is required")]
        public string? Frequency { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Amount is required")]
        public double PenalAmount { get; set; }

        public double PenalInterest { get; set; }

        public int Tenor { get; set; }

        [Required(ErrorMessage = "POS As On is required")]
        public DateTime POS_AsOn { get; set; } = DateTime.Now;

        public string? Status { get; set; } //Active/Restructed?Settled input type therefore string?

        public string? note { get; set; }

        public double EMI_Installment { get; set; }

        public double EMI_OS { get; set; }

        #endregion

        #region Restructuring Fields

        //Restructuring fields
        public string? Res_Facility { get; set; }

        public string? Res_ImplementationDate { get; set; }

        public int Res_TenorMonths { get; set; }

        public DateTime Res_EndDate { get; set; } = DateTime.Now;

        public double Res_POS { get; set; }

        public double Res_Interest { get; set; }

        public DateTime Res_POSAsOn { get; set; } = DateTime.Now;

        public double Res_PrincipleMonutorium { get; set; }

        public string? Res_InterestCalculation { get; set; }

        public string? Res_PrincipalFrequency { get; set; }

        public double Res_InterestRate { get; set; }

        public double Res_DefaultInterest { get; set; }

        public double Res_BulletPayment { get; set; }

        public string? Res_Status { get; set; }

        public double Res_Upfront { get; set; }

        public double Res_InterestMonutorium { get; set; }

        public string? Res_Terms { get; set; }

        public double TOS { get; set; }

        #endregion
    }
}
