#nullable enable
using System;

// Demonstration driver: builds the browsing-history BST from the same
// 10 sample entries as Question 1 and exercises every operation.

/// <summary>Provides the demonstration entry point for the browser-history binary search tree.</summary>
public static class q2_Program
{
    /// <summary>Runs the browser-history binary search tree demonstration.</summary>
    public static void Main()
    {
        q2_BST tree = new q2_BST();

        Console.WriteLine("Browser History BST");
        Console.WriteLine("New tree | Count = " + tree.Count + " | Height() = " + tree.Height());
        Console.WriteLine("InOrder() on the empty tree:");
        tree.InOrder();
        Console.WriteLine();

        // The 10 sample entries from Question 1, in the same order.
        q1_HistoryEntry[] entries =
        {
            new q1_HistoryEntry("https://www.google.com",     "Google Search",  "08:00"),
            new q1_HistoryEntry("https://www.github.com",     "GitHub",         "08:05"),
            new q1_HistoryEntry("https://stackoverflow.com",  "Stack Overflow", "08:12"),
            new q1_HistoryEntry("https://docs.microsoft.com", "Microsoft Docs", "08:20"),
            new q1_HistoryEntry("https://www.w3schools.com",  "W3Schools",      "08:31"),
            new q1_HistoryEntry("https://leetcode.com",       "LeetCode",       "08:45"),
            new q1_HistoryEntry("https://www.reddit.com",     "Reddit",         "09:00"),
            new q1_HistoryEntry("https://www.youtube.com",    "YouTube",        "09:15"),
            new q1_HistoryEntry("https://www.linkedin.com",   "LinkedIn",       "09:30"),
            new q1_HistoryEntry("https://www.codewars.com",   "Codewars",       "09:45")
        };

        // Insert: each entry is placed by comparing its URL alphabetically.
        Console.WriteLine("Insert: the 10 sample entries");
        for (int i = 0; i < entries.Length; i++)
        {
            tree.Insert(entries[i]);
            Console.WriteLine("Insert #" + (i + 1) + " -> " + entries[i].URL + " | Count = " + tree.Count);
        }

        // Edge case: a URL that is already in the tree is rejected.
        bool added = tree.Insert(entries[0]);
        Console.WriteLine("Insert (same URL again) -> " + (added ? "added" : "rejected, URL already in the tree") + " | Count = " + tree.Count);
        Console.WriteLine();

        // InOrder: alphabetical by URL.
        Console.WriteLine("InOrder(): alphabetical by URL");
        tree.InOrder();
        Console.WriteLine();

        // Height.
        Console.WriteLine("Height()");
        int height = tree.Height();
        Console.WriteLine("Height() = " + height + " (edges on the longest root-to-leaf path, i.e. " + (height + 1) + " levels)");
        Console.WriteLine();

        // Search: found near the top, found deep in the tree, and not found.
        Console.WriteLine("Search(url)");
        tree.Search("https://www.github.com");
        tree.Search("https://leetcode.com");
        tree.Search("https://www.example.com");
        Console.WriteLine();

        // Delete: one example of each case, then a URL that is not in the tree.
        Console.WriteLine("Delete(url): the three cases");
        tree.Delete("https://www.codewars.com");        // case 1: leaf
        Report(tree);
        tree.Delete("https://docs.microsoft.com");      // case 2: one child (right child only)
        Report(tree);
        tree.Delete("https://www.reddit.com");          // case 2: one child (left child only)
        Report(tree);
        tree.Delete("https://www.google.com");          // case 3: two children (this node is the root)
        Report(tree);
        tree.Delete("https://www.example.com");         // not in the tree
        Report(tree);
        Console.WriteLine();

        // InOrder again: still alphabetical after every deletion.
        Console.WriteLine("InOrder() after the deletions");
        tree.InOrder();
    }

    /// <summary>Prints the current number of entries and the height of the tree.</summary>
    /// <param name="tree">The tree whose state should be reported.</param>
    private static void Report(q2_BST tree)
    {
        Console.WriteLine("   Count = " + tree.Count + " | Height() = " + tree.Height());
    }
}