using Company_Site.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Data
{
    public class EventTimelineModel : BaseModel<int>
    {
        [Required(ErrorMessage = "Event Date is required")]
        public DateTime EventDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Event is required")]
        public string Event { get; set; }

        public DateTime ReminderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Details are required")]
        public string Details { get; set; }
    }
}
