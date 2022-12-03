using Company_Site.Data;

using Microsoft.AspNetCore.Components;
using System.Xml.Linq;

namespace Company_Site.Base
{
    public partial class BaseModifierAddPage<T, Id> : BaseAddPage<T, Id>
        where T: BaseModifierModel<Id>, new()
    {
        #region Public Properties

        [CascadingParameter(Name = "UserId")]
        public int? UserId { get; set; }

        #endregion

        #region Protected Members

        /// <summary>
        /// The account of the logged in user
        /// </summary>
        protected User? User { get; set; }

        #endregion

        #region Overriden Methods

        protected override void BeforeSetup()
        {
            User = _dbContext.Users.Where(a => a.Id == UserId.Value).FirstOrDefault();
        }

        protected override void SaveResetup()
        {
            NewEntry = new T();
            RecordData();
        }

        protected virtual void RecordData()
        {
            if (User == null)
                return;
            string name = $"{User.FirstName} {User.LastName}";
            if (NewEntry.CreatorName == null)
            {
                NewEntry.CreatorName = name;
                NewEntry.CreationDate = DateTime.Now;
            }
            else
            {
                NewEntry.Modifier = name;
                NewEntry.ModificationDate = DateTime.Now;
            }
        }

        public override void OnEdit(Id id)
        {
            RecordData();
        }

        #endregion
    }
}
