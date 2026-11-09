#nullable enable
using System;
using System.Collections.Generic;

/// <summary>
/// In-place sorting engine for book listings. The sort is an Insertion Sort written from first
/// principles: no built-in sorting method and no LINQ ordering is used.
/// </summary>

public static class q4_BookSorter
{
    /// <summary>Checks that a metric name is one of the four supported sort fields.</summary>

    /// <param name="metric">Lower-case metric name.</param>
    /// <returns>True for "title", "author", "price" or "year_published"; otherwise false.</returns>
    public static bool IsValidMetric(string metric)
    {
        switch (metric)
        {
            case "title":
            case "author":
            case "price":
            case "year_published":
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// Compares two books on the chosen metric, always in ascending order.
    /// Strings (title, author) are compared ignoring case; price and year are compared numerically.
    /// OrdinalIgnoreCase is used so the result is the same on every computer and is exactly
    /// "case-insensitive", with no culture-specific rules.
    /// </summary>

    /// <param name="first">The first book.</param>
    /// <param name="second">The second book.</param>
    /// <param name="metric">"title", "author", "price" or "year_published".</param>
    /// <returns>Negative if first comes before second, zero if they tie, positive if first comes after second.</returns>
    /// <exception cref="ArgumentException">The metric is not one of the four supported fields.</exception>
    public static int CompareBooks(q4_Book first, q4_Book second, string metric)
    {
        switch (metric)
        {
            case "title":
                return string.Compare(first.Title, second.Title, StringComparison.OrdinalIgnoreCase);
            case "author":
                return string.Compare(first.Author, second.Author, StringComparison.OrdinalIgnoreCase);
            case "price":
                return first.Price.CompareTo(second.Price);
            case "year_published":
                return first.YearPublished.CompareTo(second.YearPublished);
            default:
                throw new ArgumentException("Unsupported metric: " + metric, nameof(metric));
        }
    }

    /// <summary>
    /// Sorts the list in place into ascending order using Insertion Sort.
    /// The list is treated as a sorted part on the left (index 0 to i - 1) and an unsorted part on
    /// the right. Each pass takes the first unsorted book (the key), shifts every larger book in the
    /// sorted part one place to the right, and drops the key into the gap.
    /// Best case O(n) (already sorted), average and worst case O(n squared). Books that tie on the
    /// metric keep their original relative order (the sort is stable).
    /// </summary>

    /// <param name="books">The list to sort. It is rearranged in place.</param>
    /// <param name="metric">"title", "author", "price" or "year_published".</param>
    public static void InsertionSort(List<q4_Book> books, string metric)
    {
        // books[0] on its own is already "sorted", so start with the second book.
        for (int i = 1; i < books.Count; i++)
        {
            q4_Book key = books[i];   // the book being inserted into the sorted part
            int j = i - 1;            // last position of the sorted part

            // Shift books that come after the key one place right. The strict "> 0" means a tie
            // stops the shifting, which is what keeps the sort stable.
            while (j >= 0 && CompareBooks(books[j], key, metric) > 0)
            {
                books[j + 1] = books[j];
                j--;
            }

            // j + 1 is now the gap where the key belongs.
            books[j + 1] = key;
        }
    }
}