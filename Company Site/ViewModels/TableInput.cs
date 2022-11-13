using Org.BouncyCastle.Asn1.BC;

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

        public dynamic DefaultValue { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TableInput(Func<T, string> propName, dynamic defaultValue)
        {
            PropertyName = propName(new T());
            DefaultValue = defaultValue;
        }

        #endregion
    }
}
