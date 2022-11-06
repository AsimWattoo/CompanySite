using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class BorrowerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Borrower Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Borrower Position is required")]
        public string? Position { get; set; }

        [Required(ErrorMessage = "Borrower mobile number is required")]
        public string Mobile { get; set; }

        public string? Email { get; set; }

        public DateTime DOB { get; set; } = DateTime.Now;

        public string? PAN { get; set; }

        public string? CIN { get; set; }

        public string? Aadhar { get; set; }

        public string? Wilful_Defaulter { get; set; }
        
        public string? Net_Worth { get; set; }

        public long NumberOfShares { get; set; }

        public DateTime WillFullAsOn { get; set; } = DateTime.Now;

        public DateTime NetWorthAsOn { get; set; } = DateTime.Now;

        public double PercentOfShareHeld { get; set; }

        public string? Additional_Details { get; set; }

        public string? Address { get; set; }

    }
}
