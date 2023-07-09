using TodoApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
namespace TodoApi.Services;

public class TodoService{
    private readonly IMongoCollection<TodoItem> _todoItems;

    public TodoService(IOptions<TodoDatabaseSettings> settings)
    {

        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        _todoItems = database.GetCollection<TodoItem>(settings.Value.TodosCollectionName);
    }

    public async Task<List<TodoItem>> GetAsync() =>
        await _todoItems.Find(todoItem => true).ToListAsync();

    public async Task<TodoItem?> GetAsync(string id) =>
        await _todoItems.Find<TodoItem>(item => item.Id == id).FirstOrDefaultAsync();

    public async Task<TodoItem> CreateAsync(TodoItem todoItem)

    {
        await _todoItems.InsertOneAsync(todoItem);
        return todoItem;
    }

    public async Task UpdateAsync(string id, TodoItem todoItemIn) =>
        await _todoItems.ReplaceOneAsync(item => item.Id == id, todoItemIn);

    public async Task RemoveAsync(TodoItem todoItemIn) =>
        await _todoItems.DeleteOneAsync(item => item.Id == todoItemIn.Id);

}