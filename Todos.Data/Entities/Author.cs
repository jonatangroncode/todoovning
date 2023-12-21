namespace Todos.Data.Entities;

public class Author
{
    public int Id { get; set; } //PK
    public string Name { get; set; } = string.Empty;


    public List<Book>? Books { get; set; }


}
