using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class CompanyPolicy
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PolicyName { get; set; }

        public string Category { get; set; }

        public string Version { get; set; }

        public string FilePath { get; set; }

    }
}
