using Microsoft.EntityFrameworkCore;

namespace Api.Db;

public class TodoContext : DbContext
{
    public DbSet<TodoDbModel> Todos { get; set; }

    public TodoContext(DbContextOptions<TodoContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoDbModel>().ToTable("TodoTable");
    }
}

public class TodoDbModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsChecked { get; set; }
}
