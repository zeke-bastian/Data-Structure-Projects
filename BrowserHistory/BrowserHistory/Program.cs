using System;

/// <summary>
/// Demonstrates the browser history stack.
/// </summary>
public static class q1_Program
{
    /// <summary>
    /// Runs the browser history stack demonstration.
    /// </summary>
    public static void Main()
    {
        q1_Stack history = new q1_Stack();

        Console.WriteLine("Browser History Stack");
        Console.WriteLine("Initial capacity : " + history.Capacity);
        Console.WriteLine("IsEmpty()        : " + history.IsEmpty());
        Console.WriteLine("Size()           : " + history.Size());
        Console.WriteLine();

        // Exactly 12 hardcoded entries: the 10 supplied samples plus 2 extra.
        q1_HistoryEntry[] entries =
        {
            new q1_HistoryEntry("https://www.google.com",        "Google Search",  "08:00"),
            new q1_HistoryEntry("https://www.github.com",        "GitHub",         "08:05"),
            new q1_HistoryEntry("https://stackoverflow.com",     "Stack Overflow", "08:12"),
            new q1_HistoryEntry("https://docs.microsoft.com",    "Microsoft Docs", "08:20"),
            new q1_HistoryEntry("https://www.w3schools.com",     "W3Schools",      "08:31"),
            new q1_HistoryEntry("https://leetcode.com",          "LeetCode",       "08:45"),
            new q1_HistoryEntry("https://www.reddit.com",        "Reddit",         "09:00"),
            new q1_HistoryEntry("https://www.youtube.com",       "YouTube",        "09:15"),
            new q1_HistoryEntry("https://www.linkedin.com",      "LinkedIn",       "09:30"),
            new q1_HistoryEntry("https://www.codewars.com",      "Codewars",       "09:45"),
            new q1_HistoryEntry("https://www.wikipedia.org",     "Wikipedia",      "10:00"),
            new q1_HistoryEntry("https://www.geeksforgeeks.org", "GeeksforGeeks",  "10:15")
        };

        // Push all 12 entries. The 12th push finds the 11-slot array full and triggers the resize.
        Console.WriteLine("Push: adding " + entries.Length + " entries");
        for (int i = 0; i < entries.Length; i++)
        {
            history.Push(entries[i]);
            Console.WriteLine("Push #" + (i + 1) + " -> " + entries[i].PageTitle + " | Size = " + history.Size() + " | Capacity = " + history.Capacity);
        }
        Console.WriteLine();

        // Demonstrate every operation on the populated stack.
        Console.WriteLine("Operations on the populated stack");
        Console.WriteLine("Size()    : " + history.Size());
        Console.WriteLine("IsEmpty() : " + history.IsEmpty());

        q1_HistoryEntry top = history.Peek();
        Console.WriteLine("Peek() -> " + top);
        Console.WriteLine("Size() after Peek : " + history.Size() + " (entry not removed)");

        q1_HistoryEntry popped = history.Pop();
        Console.WriteLine("Size() after Pop  : " + history.Size());

        history.Push(popped);
        Console.WriteLine("Push(popped entry) -> " + popped.PageTitle + " | Size = " + history.Size());
        Console.WriteLine();

        // Pop everything to show LIFO order and that the copied entries survived the resize intact.
        Console.WriteLine("Pop until empty (LIFO order)");
        while (!history.IsEmpty())
        {
            history.Pop();
        }
        Console.WriteLine("IsEmpty() : " + history.IsEmpty());
        Console.WriteLine("Size()    : " + history.Size());
        Console.WriteLine();

        // Empty-stack handling: Pop and Peek throw, and the error is displayed here.
        Console.WriteLine("Empty stack handling");
        try
        {
            history.Pop();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        try
        {
            history.Peek();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}