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

        protected override async void OnInitialized()
        {
			Setup();
            Enteries = _dbSet.ToList();
        }

		#endregion

		#region Protected Methods

		protected virtual void Setup(){}

		protected virtual void Save()
		{
            if (ShouldAdd)
                _dbSet.Add(NewEntry);
            else
                _dbSet.Update(NewEntry);
            _dbContext.SaveChanges();
            NewEntry = new T();
            ShouldAdd = true;
		}

        public virtual void EditRecord(int id)
        {
            ShouldAdd = false;
            NewEntry = _dbSet.Where(t => t.Id == id).First();
        }

        protected void Clear()
		{
			NewEntry = new T();
            ShouldAdd = false;
		}

        public virtual List<T> DeleteRecord(int id)
        {
            _dbSet.Remove(_dbSet.Where(f => f.Id == id).First());
            _dbContext.SaveChanges();
            Enteries = _dbSet.ToList();
            return Enteries;
        }

        #endregion

    }
}
