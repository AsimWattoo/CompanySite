using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;

namespace Company_Site.Pages.User.Legal_Management_Pages;

public partial class EventTimeline: BaseAddPage<EventTimelineModel, int>, ITable<EventTimelineModel, int>
{
    #region Public Properties

    public Dictionary<string, Func<EventTimelineModel, string>> Headers { get; set; } = new Dictionary<string, Func<EventTimelineModel, string>>()
    {
        ["Event Date"] = p => p.EventDate.ToString("dd-MMM-yyyy"),
        ["Event"] = p => p.Event,
        ["Description"] = p => p.Details,
        ["Next Date"] = p => p.ReminderDate.ToString("dd-MMM-yyyy"),
    };

    #endregion

    #region Overriden Methods

    protected override void Setup()
    {
        _dbSet = _dbContext.EventTimeLines;
    }

    #endregion

    #region Public Methods

    public int GetId(EventTimelineModel t) => t.Id;

    public bool SearchItem(EventTimelineModel e)
    {
        return e.EventDate.ToString().Contains(_text) || e.Event.Contains(_text) || e.ReminderDate.ToString().Contains(_text) || e.Details.Contains(_text);
    }

    private string _text;

    public List<EventTimelineModel> Search(List<EventTimelineModel> enteries, string text)
    {
        _text = text;
        return enteries.Where(SearchItem).ToList();
    }

    public void ApplyDateFilter(DateTime startDate, DateTime endDate)
    {
        Enteries = _dbContext.EventTimeLines.Where(f => f.EventDate >= startDate && f.EventDate <= endDate).ToList();
        StateHasChanged();
    }

    public void ClearDateFilter()
    {
        Enteries = _dbContext.EventTimeLines.ToList();
        StateHasChanged();
    }

    #endregion
}
