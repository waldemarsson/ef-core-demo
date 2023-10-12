namespace Api.Db;

public static class DbInitializer
{
    public static void Initialize(TodoContext context)
    {
        if (context.Todos.Any())
        {
            return;   // DB has been seeded
        }

        var students = new TodoDbModel[]
        {
            new TodoDbModel
            {
                Title = "Do something",
                Description = "Some text about what to do",
                IsChecked = false,
            },
            new TodoDbModel
            {
                Title = "Do something else ",
                Description = "Some text about what to do else",
                IsChecked = false,
            },
            new TodoDbModel
            {
                Title = "Something done",
                Description = "Some text",
                IsChecked = true,
            }
        };

        context.Todos.AddRange(students);
        context.SaveChanges();
    }
}
