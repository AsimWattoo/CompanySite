namespace Company_Site.ViewModels
{
    public class TableInput<T>
        where T: new()
    {
        #region Public Properties

        /// <summary>
        /// The name of the property which is set to be editable
        /// </summary>
        public string? PropertyName { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TableInput(Func<T, string> propName)
        {
            PropertyName = propName(new T());
        }

        #endregion
    }
}
