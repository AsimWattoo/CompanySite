using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ContactPaymentHistoryModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Contact Id is required")]
        public string? ContactId { get; set; }

        [Required(ErrorMessage = "Bill Number is required")]
        public int BillNo { get; set; }

        [Required(ErrorMessage = "Service is required")]
        public string Service { get; set; }

        public double BillAmount { get; set; }

        public DateTime BillDate { get; set; } = DateTime.Now;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public double PaymentAmount { get; set; }

        public double AmountO_S { get; set; }
    }
}
