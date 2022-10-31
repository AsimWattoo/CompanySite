using Company_Site.Data;
using Microsoft.AspNetCore.Components;

namespace Company_Site.Interfaces
{
    public interface ITable<T, TId>
    {

        #region Properties

        List<T> Enteries { get; set; }

        Dictionary<string, Func<T, dynamic>> Headers { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        TId GetId(T t);

        /// <summary>
        /// Deletes the expense entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<T> DeleteRecord(TId id);

        /// <summary>
        /// Edits the record
        /// </summary>
        /// <param name="id"></param>
        void EditRecord(TId id);

        /// <summary>
        /// Searches the records
        /// </summary>
        /// <param name="users"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        List<T> Search(List<T> enteries, string text);

        /// <summary>
        /// Specifies each table row
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        List<string> GetTableRows(T record);

        #endregion

    }
}
