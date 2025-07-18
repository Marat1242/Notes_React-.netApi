namespace NotesPetProject.Models;

public class Note
{
    public Note(Guid id, string title, string description)
    {
        Id = id;
        Title = title;
        Desctiprion = description;
        CreatedAt = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Desctiprion { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
