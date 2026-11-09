#nullable enable
using System;
using System.Collections.Generic;
using System.Text.Json;

/// <summary>
/// PageTurner.co.za sorting engine: reads the book JSON file, sorts the books with Insertion Sort
/// on the chosen metric and displays the titles in sorted order.
/// </summary>
public static class q4_Program
{
    // THE SORTING METRIC. Change this one variable to "title", "author", "price" or "year_published".
    private static string metric = "title";

    // Name of the book JSON file. Keep it in the project folder next to this file.
    private const string FileName = "books.json";

    /// <summary>Runs the book loading, sorting, and display workflow.</summary>
    public static void Main()
    {
        // Tolerate capital letters or stray spaces in the metric, for example "Price ".
        string chosenMetric = metric.Trim().ToLowerInvariant();

        if (!q4_BookSorter.IsValidMetric(chosenMetric))
        {
            Console.WriteLine("Unknown metric \"" + metric + "\". Use title, author, price or year_published.");
            return;
        }

        string? path = q4_BookLoader.FindFile(FileName);
        if (path == null)
        {
            Console.WriteLine("Could not find " + FileName + ". Put it in the project folder next to q4_Program.cs.");
            return;
        }

        // Step 1: deserialise the JSON into q4_Book objects.
        List<q4_Book> books;
        try
        {
            books = q4_BookLoader.LoadBooks(path);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("The JSON file could not be read: " + ex.Message);
            return;
        }

        // Step 2: sort in place, ascending, on the chosen metric.
        q4_BookSorter.InsertionSort(books, chosenMetric);

        // Step 3: display the titles in their sorted order.
        Console.WriteLine("Books sorted by " + chosenMetric + " (ascending):");
        foreach (q4_Book book in books)
        {
            Console.WriteLine(book.Title);
        }
    }
}