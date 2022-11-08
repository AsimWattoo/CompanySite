using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class Employee
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "First name for an employee is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = " Last name is required for an employee")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        public DateTime JoiningDate { get; set; } = DateTime.Now;

        public DateTime Birthday { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Email for an employee is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        public string MobileNumber { get; set; }

        public string? AadharCard { get; set; }

        public string? LandlineNumber { get; set; }

        [Required(ErrorMessage = "Department for an employee is required")]
        public string Department { get; set; }

        public string? PanCard { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Role for an employee is required")]
        public string Role { get; set; }

        public string? OfficeLocation { get; set; }

        public string? Position { get; set; }

        public string? Access { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public string? ExitDate { get; set; }

        public string? Status { get; set; }

        public string? ProfileImage { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
    }
}
