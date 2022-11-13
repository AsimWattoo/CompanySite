using Company_Site.Base;
using Company_Site.DB;

using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class TrustRelationModel : BaseModel
    {
        public Trust? Trust { get; set; }

        [Required(ErrorMessage = "Trust code is required")]
        public string TrustCode { get; set; }

        public int AccountId { get; set; }
        [NotMapped]
        public Account? Account { get; set; }

        public int BorrowerCode { get; set; }

        #region Public Methods

        /// <summary>
        /// Populates the table
        /// </summary>
        public void Populate(ApplicationDbContext dbContext)
        {
            Trust = dbContext.Trusts.Where(f => f.TrustCode == TrustCode).FirstOrDefault();
            Account = dbContext.Accounts.Where(f => f.UserId == AccountId).FirstOrDefault();
        }

        #endregion
    }
}
