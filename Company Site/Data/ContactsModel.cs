using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class ContactsModel : BaseModel<int>
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Position is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }


        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }


        public string? Landline_Number { get; set; }


        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }


        [Required(ErrorMessage = "Mobile Number is required")]
        public string Mobile { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }


        public string? Company_Name { get; set; }


        public string? Service1 { get; set; }


        [Required(ErrorMessage = "Service is required")]
        public string Service2 { get; set; }  //DropDown


        public string? Empanelled { get; set; }


        public long Number_Assignment { get; set; }


        public string? KYC { get; set; }


        public string? rating { get; set; }


        public string? Note { get; set; }

        public long Bill_Number { get; set; }

        public long Bill_Amount { get; set; }

        public DateTime Bill_Date { get; set; } = DateTime.Now; 

        public DateTime Payment_Date { get; set; } = DateTime.Now; 

        public long Payment_Amount { get; set; }

        public long Amount_OS { get; set; }

    }
}
