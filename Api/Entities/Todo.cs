namespace Api.Entities;

public class Todo
{
    public string Title { get; init; }
    public string Description { get; init; }
    public bool IsChecked { get; init; }

    public Todo(string? title, string? description, bool? isChecked)
    {
        Title = title ?? string.Empty;
        Description = description ?? string.Empty;
        IsChecked = isChecked ?? false;
    }
}
