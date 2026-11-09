/// <summary>
/// Represents one entry in the browser navigation history.
/// </summary>
public class q1_HistoryEntry
{
    /// <summary>
    /// Gets or sets the URL visited.
    /// </summary>
    public string URL { get; set; }

    /// <summary>
    /// Gets or sets the title of the visited page.
    /// </summary>
    public string PageTitle { get; set; }

    /// <summary>
    /// Gets or sets the time when the page was visited.
    /// </summary>
    public string VisitTime { get; set; }

    /// <summary>
    /// Initializes a new browser history entry.
    /// </summary>
    /// <param name="url">The URL visited.</param>
    /// <param name="pageTitle">The title of the visited page.</param>
    /// <param name="visitTime">The time when the page was visited.</param>
    public q1_HistoryEntry(string url, string pageTitle, string visitTime)
    {
        URL = url;
        PageTitle = pageTitle;
        VisitTime = visitTime;
    }

    /// <summary>
    /// Returns a string that represents this history entry.
    /// </summary>
    public override string ToString()
    {
        return "URL: " + URL + " | Title: " + PageTitle + " | Time: " + VisitTime;
    }
}