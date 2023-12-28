

namespace Todos.Data.Contexts
{
    public class TodosDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TodoTasks> Tasks { get; set; }

        public TodosDbContext(DbContextOptions<TodosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Todos)
                .HasForeignKey(t => t.CategoryId);

            modelBuilder.Entity<TodoTasks>()
                .HasOne(t => t.Todo)
                .WithMany(t => t.TodoTasks)
                .HasForeignKey(t => t.TodoId);

            // Add more configurations if needed for your relationships
        }
    }

}