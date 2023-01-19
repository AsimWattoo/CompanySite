using Company_Site.Base;
using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System.Linq.Expressions;

namespace Company_Site.Pages.User.Legal_Management_Pages
{
    public partial class NoticeAutomation : BaseAddPage<NoticeAutomationModel, int>
    {
        #region Injected Members

        /// <summary>
        /// An instance representing the host environment of the application
        /// </summary>
        [Inject]
        private IWebHostEnvironment _webHostEnvironment { get; set; }

        #endregion

        #region Overriden Methdos

        protected override void Setup()
        {
            _dbSet = _dbContext.NoticeAutomations;
        }

        #endregion

        #region Private Methods

        private void Edit(string _id)
        {
            int id = int.Parse(_id);
            EditRecord(id);
        }

        private void Delete(string _id)
        {
            int id = int.Parse(_id);
            DeleteRecord(id);
        }

        #endregion
    }
}
