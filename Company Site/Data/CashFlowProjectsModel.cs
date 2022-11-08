using System;
using Company_Site.Base;
using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class CashFlowProjects : BaseModel
    {
        [Required(ErrorMessage = "Year is required!")]
        public string? Year { get; set; }

        [Required(ErrorMessage = "Quarter is required!")]
        public string? quarter { get; set; }

        [Required(ErrorMessage = "Amount is required!")]
        public long amount { get; set; }

        [Required(ErrorMessage = "Facility is required!")]
        public string? facilty { get; set; }

        [Required(ErrorMessage = "Source is required!")]
        public string source { get; set; }
    }
}
