using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class DraftModel: BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        [Required(ErrorMessage = "Draft message is required")]
        public string Draft { get; set; }
    }
}
