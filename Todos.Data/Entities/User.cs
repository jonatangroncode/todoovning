namespace Todos.Data.Entities;
  public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; } = new List<Todo>();
    }