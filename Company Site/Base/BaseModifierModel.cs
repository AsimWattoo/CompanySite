using System.ComponentModel.DataAnnotations;

namespace Company_Site.Base
{
    public class BaseModifierModel<Id> : BaseModel<Id>
    {
        [Required(ErrorMessage = "Creator Name is required")]
        public string CreatorName { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string? Modifier { get; set; }

        public DateTime ModificationDate { get; set; } = DateTime.Now;
    }
}
