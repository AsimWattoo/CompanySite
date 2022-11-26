using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class Documentslist : BaseAddPage<DocumentsListsModel>, ITable<DocumentsListsModel, int>
    {
        #region Public Properties

        public Dictionary<string, Func<DocumentsListsModel, string>> Headers { get; set; } = new Dictionary<string, Func<DocumentsListsModel, string>>()
        {
            ["Title"] = p => p.Title,
            ["Description"] = p => p.Description,
            ["Type"] = p => p.Type,
            ["Document Date"] = p => p.Document_Date.ToString("dd-MMM-yyyy"),
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.DocumentLists;
        }

        #endregion

        #region Public Methods

        public int GetId(DocumentsListsModel t) => t.Id;

        public bool SearchItem(DocumentsListsModel e)
        {
            return e.Title.Contains(_text) || e.Description.Contains(_text) || e.Type.Contains(_text);
        }

        private string _text;

        public List<DocumentsListsModel> Search(List<DocumentsListsModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        #endregion
    }
}
