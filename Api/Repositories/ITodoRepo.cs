using Api.Entities;

namespace Api.Repositories;

public interface ITodoRepo
{
    Task<IReadOnlyList<Todo>> GetAll();
    Task<int> Delete(int id);
}
