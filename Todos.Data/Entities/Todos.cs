namespace Todos.Data.Entities;
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<TodoTasks>? Tasks { get; set; } = new List<TodoTasks>();
}
