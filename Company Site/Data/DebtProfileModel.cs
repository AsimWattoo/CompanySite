using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class DebtProfileModel : BaseModel
    {
        [Required(ErrorMessage ="Lender Name is required!")]
        public string Lender_Name { get; set; }

        [Required(ErrorMessage ="Facility is required!")]
        public string Facilty{ get; set; }

        [Required(ErrorMessage ="Account Number is required!")]
        public string Account_Number{ get; set; }

        public DateTime? NPA_Date{ get; set; }= DateTime.Now;

        [Required(ErrorMessage ="POS is required!")]
        public long POS { get; set; }

        public double? OS_interest { get; set; }

        [Required(ErrorMessage ="Base Interest is required!")]
        public double Base_Interest{ get; set; }

        [Required(ErrorMessage ="Spread is required!")]
        public double Spread{ get; set; }

        [Required(ErrorMessage ="Calculation is required!")]
        public string? Calculation{ get; set; }

        [Required(ErrorMessage ="Frequency is required!")]
        public string frequency{ get; set; }

        public long? penal_amount{ get; set; }

        public double? penal_interest { get; set; }

        public DateTime? Tenor { get; set; }

        [Required(ErrorMessage ="POS as on  is required!")]
        public DateTime POS_As_on{ get; set; }= DateTime.Now;

        public string? label_1{ get; set; }

        public string? Status{ get; set; }

        public string? Note{ get; set; }

        public string? EMI_install{ get; set; }
    }
}
