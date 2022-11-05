using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class CompanyPolicy
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Policy Name is required")]
        public string PolicyName { get; set; }

        [Required(ErrorMessage = "Category for the policy is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Version for the policy is required")]
        public string Version { get; set; }

        public string FilePath { get; set; }

    }
}
