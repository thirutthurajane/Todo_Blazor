using Microsoft.EntityFrameworkCore;
using TodoList.Interfaces;
using TodoList.Models;
using AppContext = TodoList.Data.AppContext;

namespace TodoList.Services;

public class TodoServices : ITodo
{
    private IDbContextFactory<AppContext> _appContext;

    public TodoServices(IDbContextFactory<AppContext> appContext)
    {
         _appContext = appContext;
    }

    public async Task<List<Todo>> GetAllTodoAsync()
    {
        await using var ctx = await _appContext.CreateDbContextAsync();
        return await ctx.Todo.ToListAsync();
    }

    public async Task<Todo> GetTodo(int id)
    {
        await using var ctx = await _appContext.CreateDbContextAsync();
        return (await ctx.Todo.FindAsync(id))!;
    }

    public async Task<List<Todo>> GetTodoByTitle(string? title)
    {
        Console.WriteLine(title);
        await using var ctx = await _appContext.CreateDbContextAsync();
        var todos = from t in ctx.Todo
            where t.Title.Contains(title) || t.Detail.Contains(title)
            select t;
        return await todos.ToListAsync();
    }

    public async Task EditTodo(Todo todo)
    {
        await using var ctx = await _appContext.CreateDbContextAsync();
        var _todo = await ctx.Todo.FindAsync(todo.Id);
        _todo.Title = todo.Title;
        _todo.Detail = todo.Detail;
        await ctx.SaveChangesAsync();
    }

    public async Task<int> AddTodo(Todo todo)
    {
        try
        {
            await using var ctx = await _appContext.CreateDbContextAsync();
            todo.Created = DateTime.Now;
            todo.Updated = DateTime.Now;
            await ctx.Todo.AddAsync(todo);
            return await ctx.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteTodo(int id)
    {
        await using var ctx = await _appContext.CreateDbContextAsync();
        var todo = ctx.Todo.Find(id);
        ctx.Todo.Remove(todo);
        await ctx.SaveChangesAsync();
    }
}