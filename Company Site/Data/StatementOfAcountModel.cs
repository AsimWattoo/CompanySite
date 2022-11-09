namespace Company_Site.Data
{
    public class StatementOfAccountModel : BaseModel
    {

        public int AccountId{get;set;}
        public int TrustId{get;set;}
        public double CollectionTillDate{get;set;}
        public double ExpensesTillDate{get;set;}
        public string? Disbursement{get;set;} //If not required string?  If unknown String
        public int AccountNumber{get;set;}
        public string? Frequency{get;set;}  //Dropdown==string?
        public string? InterestCalculation{get;set;}
        public DateTime StatementAsOn{get;set;}=DateTime.Now;
    
    
    

    }
}
