using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class DebtProfile : BaseAddPage<DebtProfileModel>, ITable<DebtProfileModel, int>
    {
        protected override void Setup()
        {
            _dbSet = _dbContext.DebtProfiles;
        }

        public Dictionary<string, Func<DebtProfileModel, string>> Headers { get; set; } = new Dictionary<string, Func<DebtProfileModel, string>>()
        {
            ["Lender Name"] = p => p.Lender_Name,
            ["Facility"] = p => p.Facilty,
            ["Account Number"] = p => p.Account_Number,
            ["Lender Name"] = p => p.POS_As_on.ToString("dd/MM/yyyy"),
            ["Lender Name"] = p => p.Lender_Name,
        };

        public int GetId(DebtProfileModel t)
        {
            return t.Id;
        }

        private bool _searchItem(DebtProfileModel d)
        {
            return d.Lender_Name.Contains(_text) || d.Facilty.Contains(_text);
        }

        private string _text { get; set; }

        public List<DebtProfileModel> Search(List<DebtProfileModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(_searchItem).ToList();
        }
    }
}
