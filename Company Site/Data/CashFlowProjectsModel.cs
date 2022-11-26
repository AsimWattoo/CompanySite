using System;
using Company_Site.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Company_Site.Data
{
    public class CashFlowProjectsModel : BaseModel
    {
        [Required(ErrorMessage = "Year is required!")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Quarter is required!")]
        public string? quarter { get; set; }

        [Required(ErrorMessage = "Amount is required!")]
        public long amount { get; set; }

        [Required(ErrorMessage = "Facility is required!")]
        public int Facilty { get; set; }

        [NotMapped]
        public string? FacilityName { get; set; }

        [Required(ErrorMessage = "Source is required!")]
        public string source { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj is CashFlowProjectsModel cash)
            {
                return cash.Year == Year;
            }
            return false;
        }
    }
}
