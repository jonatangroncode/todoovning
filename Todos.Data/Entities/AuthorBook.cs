namespace Todos.Data.Entities;

public class AuthorBook
{
    public int Id { get; set; } //PK

    public int AuthorId { get; set; } //FK

    public int BookId { get; set; } //FK
  

   
}
