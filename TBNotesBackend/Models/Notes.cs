namespace TBNotesBackend.Models;
public class Note
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public string? Content { get; set; }

    public DateTime created_at { get; set; }

    public DateTime? updated_at { get; set; }
}
