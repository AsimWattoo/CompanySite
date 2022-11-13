using Company_Site.Data;
using Company_Site.DB;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

namespace Company_Site.Base
{
	public partial class BaseAddPage<T> : ComponentBase
		where T: BaseModel, new()
	{

        #region Protected Properties

        public List<T> Enteries { get; set; } = new List<T>();

		protected T NewEntry { get; set; } = new T();

		protected List<string> _errors = new List<string>();

		protected DbSet<T> _dbSet { get; set; }

        protected bool ShouldAdd { get; set; } = true;

        #endregion

        #region Injected Members

        [Inject]
		protected ApplicationDbContext _dbContext { get; set; }

		[Inject]
		protected ProtectedSessionStorage _storage { get; set; }

		[Inject]
		protected NavigationManager _navigationManager { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
			Setup();
            SaveResetup();
            LoadData();
        }

		#endregion

		#region Protected Methods

        protected virtual void LoadData()
        {
            Enteries = _dbSet.ToList();
        }

        protected virtual void Setup(){}

		protected virtual void Save()
		{
            _errors.Clear();
            if(SaveSetup())
            {
                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    //TODO: Show the exception to the user
                    Console.WriteLine(ex);
                }
                LoadData();
                ShouldAdd = true;
                SaveResetup();
                StateHasChanged();
            }
		}

        protected virtual bool SaveSetup()
        {
            if (ShouldAdd)
                _dbSet.Add(NewEntry);
            else
                _dbSet.Update(NewEntry);
            return true;
        }

        protected virtual void SaveResetup()
        {
            NewEntry = new T();
        }

        public void EditRecord(int id)
        {
            ShouldAdd = false;
            NewEntry = _dbSet.Where(t => t.Id == id).First();
            OnEdit(id);
            StateHasChanged();
        }

        /// <summary>
        /// Allows the child class to perform some operation when edit button is clicked
        /// </summary>
        public virtual void OnEdit(int id)
        {}

        protected void Clear()
		{
            SaveResetup();
            OnClear();
            StateHasChanged();
		}

        /// <summary>
        /// Allows child classes to change the clear functionality
        /// </summary>
        protected virtual void OnClear() { ShouldAdd = true; }

        public virtual List<T> DeleteRecord(int id)
        {
            _dbSet.Remove(_dbSet.Where(f => f.Id == id).First());
            _dbContext.SaveChanges();
            Enteries = _dbSet.ToList();
            StateHasChanged();
            return Enteries;
        }

        #endregion

    }
}
