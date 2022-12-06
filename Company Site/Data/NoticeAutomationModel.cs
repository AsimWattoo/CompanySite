using Company_Site.Base;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class NoticeAutomationModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Notice Type is required")]
        public string NoticeType { get; set; }

        [Required(ErrorMessage = "Notice Title is required")]
        public string NoticeTitle { get; set; }

        [Required(ErrorMessage = "Forum is required")]
        public string Forum { get; set; }

        private string? Account { get; set; }

        public string? Notice { get; set; }

        public string? SearchSavedDraft { get; set; }
    }
}
