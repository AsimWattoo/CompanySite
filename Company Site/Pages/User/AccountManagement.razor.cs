using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class AccountManagement : BaseAddPage<TrustRelationModel>
    {

        #region Public Properties

        /// <summary>
        /// The user id of the logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Injected Members

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The account of the logged in user
        /// </summary>
        private Account? _Account { get; set; }

        private Dictionary<string, Func<TrustRelationModel, string>> Headers { get; set; } = new Dictionary<string, Func<TrustRelationModel, string>>()
        {
            ["Trust"] = t => t.Trust.Trust_Name,
            ["Total SR Issued"] = t => t.Trust.SRIssued.ToString(),
        };

        /// <summary>
        /// The list of available trusts
        /// </summary>
        private List<Trust> Trusts { get; set; } = new List<Trust>();

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when it is initialized
        /// </summary>
        protected override void Setup()
        {
            _dbSet = _dbContext.TrustRelations;
            _Account = _dbContext.Accounts.Where(a => a.UserId == UserId).FirstOrDefault();
            Trusts = _dbContext.Trusts.ToList();
        }

        #endregion

        #region Private Methods

        protected override void SaveResetup()
        {
            NewEntry = new TrustRelationModel();
            NewEntry.Account = _dbContext.Accounts.Where(a => a.UserId == UserId.Value).FirstOrDefault();
            NewEntry.AccountId = UserId.Value;
            NewEntry.Trust = new Trust();
        }

        protected override bool SaveSetup()
        {
            if(NewEntry.Account == null)
            {
                _errors.Add("No Account exists");
                return false;
            }
            else
            {
                return base.SaveSetup();
            }
        }

        private int GetId(TrustRelationModel model) => model.Id;

        private void TrustCodeChanged(string trustCode)
        {
            NewEntry.TrustCode = trustCode;
            NewEntry.Trust = _dbContext.Trusts.Where(t => t.TrustCode == trustCode).FirstOrDefault();
        }

        protected override void LoadData()
        {
            Enteries = _dbContext.TrustRelations.ToList();
            Enteries.ForEach(e =>
            {
                e.Account = _dbContext.Accounts.Where(a => a.UserId == e.AccountId).FirstOrDefault();
                e.Trust = _dbContext.Trusts.Where(t => t.TrustCode == e.TrustCode).FirstOrDefault();
            });
        }

        #endregion

    }
}
