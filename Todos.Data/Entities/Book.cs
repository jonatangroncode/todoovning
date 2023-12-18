namespace Todos.Data.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }  = string.Empty;

    public int PublisherId { get; set; } //FK

    public Publisher? Publisher { get; set; }

    public List<Author>? Authors { get; set; }
}
