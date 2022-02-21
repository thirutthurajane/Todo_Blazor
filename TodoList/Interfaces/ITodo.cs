using TodoList.Models;

namespace TodoList.Interfaces;

public interface ITodo
{
    public Task<List<Todo>> GetAllTodoAsync();
    public Task<Todo> GetTodo(int id);

    public Task<List<Todo>> GetTodoByTitle(string? title);
    public Task EditTodo(Todo todo);
    public Task<int> AddTodo(Todo todo);
    public Task DeleteTodo(int id);
}