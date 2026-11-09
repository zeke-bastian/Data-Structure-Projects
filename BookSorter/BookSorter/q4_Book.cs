#nullable enable
using System.Text.Json.Serialization;

/// <summary>
/// One book listing from the PageTurner.co.za JSON file.
/// The <see cref="JsonPropertyNameAttribute" /> attributes tell System.Text.Json which JSON field fills each
/// property, so "year_published" in the file maps onto <see cref="YearPublished" /> in C#.
/// </summary>
public class q4_Book
{
    /// <summary>Unique identifier of the book ("id").</summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>Title of the book ("title").</summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    /// <summary>Author name ("author").</summary>
    [JsonPropertyName("author")]
    public string Author { get; set; } = "";


    /// <summary>
    /// Price in rand ("price"). decimal is used because it stores amounts such as 299.99
    /// exactly, whereas double can only approximate them.
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>Year the book was published ("year_published").</summary>
    [JsonPropertyName("year_published")]
    public int YearPublished { get; set; }
}