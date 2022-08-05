using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class User : IdentityUser
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Designation { get; set; }

        [Required]
        public string? Department { get; set; }

        [Required]
        public string? Role { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfJoining { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastLogin { get; set; }

        [Required]
        public string? OfficeLocation { get; set; }

        [Required]
        public string? Status { get; set; }

        public string? Access { get; set; }

        [Required]
        public string? Mobile { get; set; }
    }
}
