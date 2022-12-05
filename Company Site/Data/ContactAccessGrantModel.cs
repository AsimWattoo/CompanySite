using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class ContactAccessGrantModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Contact Id is required")]
        public string? ContactId { get; set; }

        [Required(ErrorMessage = "Access Given by is required")]
        public string? AccessGivenBy { get; set; }

        [Required(ErrorMessage = "Access to is required")]
        public string? AccessTo { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
