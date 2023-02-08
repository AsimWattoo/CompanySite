using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.Components
{
    public partial class BaseCollapsibleContainer<T> : ComponentBase
        where T : class, new()
    {

        #region Protected Members

        /// <summary>
        /// The instance of the item
        /// </summary>
        protected T Instance = new T();

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the value to the instance
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(T value)
        {
            Instance = value;
        }

        /// <summary>
        /// Returns the instance value
        /// </summary>
        /// <returns></returns>
        public T GetValue() => Instance;

        #endregion

    }
}
