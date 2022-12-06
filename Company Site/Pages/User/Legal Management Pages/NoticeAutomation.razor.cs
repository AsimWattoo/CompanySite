using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

using System.Linq.Expressions;

namespace Company_Site.Pages.User.Legal_Management_Pages
{
    public partial class NoticeAutomation : ComponentBase
    {
        #region Private Members

        /// <summary>
        /// The list of saved drafts
        /// </summary>
        private List<DraftModel> SavedDrafts { get; set; } = new List<DraftModel>();

        private DraftModel NewDraft { get; set; } = new DraftModel();

        private List<string> _errors { get; set; } = new List<string>();

        private bool ShouldAddDraft { get; set; } = true;

        #endregion

        #region Injected Members

        [Inject]
        public ApplicationDbContext _dbContext { get; set; }

        [Inject]
        public ApplicationState _applicationState { get; set; }

        #endregion

        #region Overriden Methdos

        protected override void OnInitialized()
        {
            SavedDrafts = _dbContext.SavedDrafts.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).ToList();
            NewDraft.BorrowerCode = _applicationState.BorrowerCode;
        }

        #endregion

        #region Private Methods

        private void Edit(string _id)
        {
            ShouldAddDraft = false;
            int id = int.Parse(_id);
            NewDraft = _dbContext.SavedDrafts.Where(f => f.Id == id).First();
            StateHasChanged();
        }

        private void Delete(string _id)
        {
            int id = int.Parse(_id);
            DraftModel newDraft = _dbContext.SavedDrafts.Where(f => f.Id == id).First();
            if(newDraft != null)
            {
                _dbContext.Remove(newDraft);
                _dbContext.SaveChanges();
                SavedDrafts = _dbContext.SavedDrafts.ToList();
                NewDraft = new DraftModel();
                NewDraft.BorrowerCode = _applicationState.BorrowerCode;
                StateHasChanged();
            }
        }

        private void Clear()
        {
            ShouldAddDraft = true;
            NewDraft = new DraftModel();
            NewDraft.BorrowerCode = _applicationState.BorrowerCode;
        }

        /// <summary>
        /// Saves a draft in the database
        /// </summary>
        private void SaveDraft()
        {
            _errors.Clear();
            if (_applicationState.BorrowerCode < 0)
            {
                _errors.Add("Please select a borrower from the account management page");
                return;
            }

            if (ShouldAddDraft)
            {
                _dbContext.SavedDrafts.Add(NewDraft);
            }
            else
            {
                _dbContext.SavedDrafts.Update(NewDraft);
            }
            try
            {
                _dbContext.SaveChanges();
                SavedDrafts = _dbContext.SavedDrafts.ToList();
                NewDraft = new DraftModel();
                NewDraft.BorrowerCode = _applicationState.BorrowerCode;
            }
            catch (Exception ex)
            {
                _errors.Add(ex.Message);
            }
            StateHasChanged();
        }

        #endregion
    }
}
