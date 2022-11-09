namespace Company_Site.Data
{
    public class DebtProfileModel : BaseModel
    {
        [Required(ErrorMessage = "Borrower Name is required")]
        public string LenderName{get;set;}

        [Required(ErrorMessage = "Borrower Name is required")]
        public string Facility{get;set;}
        
        [Required(ErrorMessage = "Borrower Name is required")]
        public long AccountNumber{get;set;}
        
        public DateTime NPA_Date{get;set;}=DateTime.Now;

        [Required(ErrorMessage = "Borrower Name is required")]
        public string POS{get;set;}
        
        public string? O/SInterest{get;set;} 
        
        [Required(ErrorMessage = "Borrower Name is required")]
        public double BaseInterestRate{get;set;}
        
        [Required(ErrorMessage = "Borrower Name is required")]
        public double Spread {get;set;} 
        
        [Required(ErrorMessage = "Borrower Name is required")]
        public string? Calculation{get;set;}

        [Required(ErrorMessage = "Borrower Name is required")]
        public string? Frequency{get;set;}

        public double PenalAmount {get;set;}  

        public double PenalInterest {get;set;}  

        public int Tenor {get;set;} 

        [Required(ErrorMessage = "Borrower Name is required")]
        public DateTime POS_AsOn {get;set;}=DateTime.Now;

        public string? Label1 {get;set;} //Label input type therefore string? 

        public string? Status {get;set;} //Active/Restructed?Settled input type therefore string?

        public string note {get;set;} 

        public double EMI_Installment {get;set;} 

        public double EMI_O/S {get;set;} 

         



        
    
    
    

    }
}
