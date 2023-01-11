using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User
{
    public partial class VDR : ComponentBase
    {
        #region Public Properties

        /// <summary>
        /// The id of the currently logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The list of file items
        /// </summary>
        private List<FileItem> _fileItems = new List<FileItem>();

        #endregion

        #region Injected Members

        /// <summary>
        /// The injected db context
        /// </summary>
        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        /// <summary>
        /// Fires when the item is initialized
        /// </summary>
        protected override void OnInitialized()
        {
            if (UserId.HasValue)
            {
                //The list of files for the user id
                _fileItems = _dbContext.FileItems.ToList();
            }
        }

        #endregion
    }
}
