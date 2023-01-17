using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class DocumentsListsModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Type is required!")]
        public string? Type { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        public int DocumentAmount { get; set; }

        public DateTime Document_Date { get; set; } = DateTime.Now;

        public string? Stamp_Duty { get; set; }

        public string? Original_Held_With { get; set; }

        public string? Execution_Place { get; set; }

        public string? Document { get; set; }

    }
}