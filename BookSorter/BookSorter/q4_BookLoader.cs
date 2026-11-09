#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Locates the book JSON file and deserialises it into q4_Book objects.
/// Only System.Text.Json is used (no third-party libraries).
/// </summary>
public static class q4_BookLoader
{
    /// <summary>
    /// Looks for the file in the program's folder and then in each parent folder (up to six levels),
    /// then in the current directory. This means the file can sit in the project folder next to
    /// q4_Program.cs even though Visual Studio runs the program from bin\Debug\netX.
    /// </summary>

    /// <param name="fileName">Name of the JSON file, for example books.json.</param>
    /// <returns>The full path if the file was found; otherwise null.</returns>
    public static string? FindFile(string fileName)
    {
        DirectoryInfo? folder = new DirectoryInfo(AppContext.BaseDirectory);

        for (int level = 0; level < 6 && folder != null; level++)
        {
            string candidate = Path.Combine(folder.FullName, fileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }
            folder = folder.Parent;
        }

        string inCurrentDirectory = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        return File.Exists(inCurrentDirectory) ? inCurrentDirectory : null;
    }

    /// <summary>
    /// Reads the file and deserialises every book into a List of q4_Book objects.
    /// The normal layout is a JSON array of book objects. A wrapper object that holds the array
    /// (for example { "books": [ ... ] }) and a single book object are also accepted.
    /// </summary>

    /// <param name="path">Full path of the JSON file.</param>
    /// <returns>The deserialised books, in file order.</returns>
    /// <exception cref="JsonException">The file is not valid JSON or a field has the wrong type.</exception>
    public static List<q4_Book> LoadBooks(string path)
    {
        string json = File.ReadAllText(path);

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,                              // "Title" or "title" both match
            AllowTrailingCommas = true,                                      // tolerate a trailing comma
            NumberHandling = JsonNumberHandling.AllowReadingFromString       // tolerate "299.99" written as text
        };
        JsonDocumentOptions documentOptions = new JsonDocumentOptions { AllowTrailingCommas = true };

        using (JsonDocument document = JsonDocument.Parse(json, documentOptions))
        {
            JsonElement root = document.RootElement;

            // Normal case: a JSON array of book objects.
            if (root.ValueKind == JsonValueKind.Array)
            {
                return root.Deserialize<List<q4_Book>>(options) ?? new List<q4_Book>();
            }

            if (root.ValueKind == JsonValueKind.Object)
            {
                // Wrapper object: use the first property whose value is an array.
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    if (property.Value.ValueKind == JsonValueKind.Array)
                    {
                        return property.Value.Deserialize<List<q4_Book>>(options) ?? new List<q4_Book>();
                    }
                }

                // Otherwise the file holds one book object.
                q4_Book? single = root.Deserialize<q4_Book>(options);
                List<q4_Book> result = new List<q4_Book>();
                if (single != null)
                {
                    result.Add(single);
                }
                return result;
            }
        }

        return new List<q4_Book>();
    }
}