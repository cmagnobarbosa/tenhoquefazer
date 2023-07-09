using TodoApi.Models;
using TodoApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController: ControllerBase{

    private readonly TodoService _todoService;
    public TodoController(TodoService todoService) =>_todoService = todoService;


    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAsync() =>
        await _todoService.GetAsync();

    [HttpGet("{id:length(24)}", Name = "GetTodo")]
    public async Task<ActionResult<TodoItem?>> GetAsync(string id){
        var todoItem = await _todoService.GetAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
    }
    [HttpPost]
    public async Task<ActionResult<TodoItem>> CreateAsync(TodoItem todoItem){
        await _todoService.CreateAsync(todoItem);

        return CreatedAtRoute("GetTodo", new { id = todoItem.Id.ToString() }, todoItem);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateAsync(string id, TodoItem todoItemIn){
        var todoItem = await _todoService.GetAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        await _todoService.UpdateAsync(id, todoItemIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteAsync(string id){
        var todoItem = await _todoService.GetAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        await _todoService.RemoveAsync(todoItem);

        return NoContent();
    }
}