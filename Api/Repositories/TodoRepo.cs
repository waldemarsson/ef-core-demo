using Api.Db;
using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class TodoRepo : ITodoRepo
{
    private readonly TodoContext todoContext;
    
    public TodoRepo(TodoContext todoContext) => this.todoContext = todoContext;

    public async Task<IReadOnlyList<Todo>> GetAll() => await todoContext.Todos.Select(x => new Todo(x.Title, x.Description, x.IsChecked)).ToListAsync();

    public async Task<Todo?> FindOne(int id)
    {
        var todo = await todoContext.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (todo is not null)
        {
            return new Todo(todo.Title, todo.Description, todo.IsChecked);
        }

        return null;
    }

    public async Task<int> Delete(int id) => await todoContext.Todos.Where(x => x.Id == id).ExecuteDeleteAsync();
}
