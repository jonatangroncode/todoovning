namespace Todos.Data.Contexts;

public class DataContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<AuthorBook> AuthorBooks { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);
        builder.Entity<AuthorBook>().HasKey(tt => new { tt.AuthorId, tt.BookId });
    }
}