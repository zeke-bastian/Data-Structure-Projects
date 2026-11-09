/// <summary>Represents one entry in the browser navigation history.</summary>
public class q1_HistoryEntry
{
    /// <summary>Gets or sets the URL visited by the user.</summary>
    public string URL { get; set; }

    /// <summary>Gets or sets the title of the visited page.</summary>
    public string PageTitle { get; set; }

    /// <summary>Gets or sets the time when the page was visited.</summary>
    public string VisitTime { get; set; }

    /// <summary>Creates a history entry with the specified URL, page title, and visit time.</summary>
    /// <param name="url">The URL visited by the user.</param>
    /// <param name="pageTitle">The title of the visited page.</param>
    /// <param name="visitTime">The time when the page was visited.</param>
    public q1_HistoryEntry(string url, string pageTitle, string visitTime)
    {
        URL = url;
        PageTitle = pageTitle;
        VisitTime = visitTime;
    }

    /// <summary>Returns a formatted representation of this history entry.</summary>
    /// <returns>The URL, page title, and visit time.</returns>
    public override string ToString()
    {
        return "URL: " + URL + " | Title: " + PageTitle + " | Time: " + VisitTime;
    }
}