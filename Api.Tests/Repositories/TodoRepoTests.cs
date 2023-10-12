using Api.Db;
using Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Api.Tests.Repositories;

public class TodoRepoTests
{
    private readonly DbContextOptions<TodoContext> dbContextOptions;

    public TodoRepoTests()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        dbContextOptions = new DbContextOptionsBuilder<TodoContext>()
               .UseSqlite($"Data Source={Path.Join(path, "todo.db")}")
               .Options;

        var todos = new TodoDbModel[]
        {
            new TodoDbModel
            {
                Title = "Do something",
                Description = "Some text about what to do",
                IsChecked = false,
            },
            new TodoDbModel
            {
                Title = "Do something else",
                Description = "Some text about what to do else",
                IsChecked = true,
            },
            new TodoDbModel
            {
                Title = "Something done",
                Description = "Some text",
                IsChecked = true,
            }
        };

        using var context = new TodoContext(dbContextOptions);
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Todos.AddRange(todos);
        context.SaveChanges();
    }

    [Fact]
    public async Task Should_ReturnAll_When_Requested()
    {
        // Arrange
        using var context = new TodoContext(dbContextOptions);
        var repo = new TodoRepo(context);

        // Act
        var result = await repo.GetAll();

        // Assert 
        result.Count.ShouldBe(3);
        
    }

    [Theory]
    [InlineData(1, "Do something", "Some text about what to do", false)]
    [InlineData(2, "Do something else", "Some text about what to do else", true)]
    public async Task Should_ReturnOne_When_Requested(int id, string expectedTitle, string expectedDescription, bool expectedIsCheched)
    {
        // Arrange
        using var context = new TodoContext(dbContextOptions);
        var repo = new TodoRepo(context);

        // Act
        var result = await repo.FindOne(id);

        // Assert 
        result.ShouldNotBeNull();
        result.Title.ShouldBe(expectedTitle);
        result.Description.ShouldBe(expectedDescription);
        result.IsChecked.ShouldBe(expectedIsCheched);
    }

    [Fact]
    public async Task Should_ReturnNull_When_EntityDoesNotExistsInDatabase()
    {
        // Arrange
        var id = 10;
        using var context = new TodoContext(dbContextOptions);
        var repo = new TodoRepo(context);

        // Act
        var result = await repo.FindOne(id);

        // Assert 
        result.ShouldBeNull();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task Should_Delete_When_UsingDeleteWithId(int id)
    {
        // Arrange
        using var context = new TodoContext(dbContextOptions);
        var repo = new TodoRepo(context);

        // Act
        var result = await repo.Delete(id);

        // Assert 
        result.ShouldBe(1);
        (await repo.FindOne(id)).ShouldBeNull();
        (await repo.GetAll()).Count.ShouldBe(2);
    }
}

