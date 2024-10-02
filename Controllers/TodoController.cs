using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]

public class TodoController : ControllerBase
{
	private readonly TodoContext _context;
	public TodoController(TodoContext context)
	{
		_context = context;
	}

	[HttpGet]//просмотр списка дел
	public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
	{
        
        return await _context.TodoItems.ToListAsync();
	}

	[HttpPost]//добавление дела
	public async Task<IActionResult> AddTask([FromBody] string task)
	{
		if (task == null)
		{
			return BadRequest();
		}

        var newTask = new TodoItem
        {
            Description = task,
            Status = false 
        };
        _context.TodoItems.Add(newTask);
		await _context.SaveChangesAsync();
		return Ok();
	}

	[HttpDelete("{id}")]//удаление дела
	public async Task<IActionResult> DeleteTask(int id)
	{
		var task = await _context.TodoItems.FindAsync(id);
		if (task == null)
		{
			return NotFound("Такого дела не существует");
		}

		_context.TodoItems.Remove(task);
		await _context.SaveChangesAsync();
		return Ok();
	}

	[HttpPut("{id}")]//редактирование дела
	public async Task<IActionResult> EditTask(int id, [FromBody] string newDescription)
	{
		if (newDescription == null)
		{
			return BadRequest();
		}
		var task = await _context.TodoItems.FindAsync(id);
		if (task == null)
		{
			return NotFound("Такого дела не существует");
		}
		task.Description = newDescription;
		await _context.SaveChangesAsync();
		return Ok();
	}

	[HttpPatch("{id}/complete")]//задача выполнена
	public async Task<IActionResult> CompleteTask(int id)
	{
		var task = await _context.TodoItems.FindAsync(id);
		if (task == null)
		{
			return NotFound("Такое дело не существует");
		}

		task.Status = true;
		await _context.SaveChangesAsync();
		return Ok();
	}

	[HttpPatch("{id}/incomplete")]//задача не выполнена
	public async Task<IActionResult> IncompleteTask(int id)
	{
		var task = await _context.TodoItems.FindAsync(id);
		if (task == null)
		{
			return NotFound("Такое дело не существует");
		}

		task.Status = false;
		await _context.SaveChangesAsync();
		return Ok();
	}

    /*[HttpPost("addNewList")]//загрузка нового списка
    public async Task<IActionResult> AddNewList([FromBody] IEnumerable<TodoItem> tasks)
	{
		if (tasks == null || !tasks.Any())
		{
			return BadRequest();
		}

		foreach (var task in _context.TodoItems)
		{
			_context.TodoItems.Remove(task);
		}

		foreach (var task in tasks)
		{
			_context.TodoItems.Add(task);
		}

		//_context.TodoItems.AddRange(tasks);
		await _context.SaveChangesAsync();
		return Ok(tasks);
	}*/
}
