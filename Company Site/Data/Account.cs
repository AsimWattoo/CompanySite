using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class Account : BaseModel<int>
    {
        [Required(ErrorMessage = "Borrower code is required")]
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string Company { get; set; }
   }
}
