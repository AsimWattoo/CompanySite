using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class Vendor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string VendorName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public string GSTNumber { get; set; }

    }
}
