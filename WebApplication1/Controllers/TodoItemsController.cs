using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoItemsController> _logger; 

        public TodoItemsController(TodoContext context, ILogger<TodoItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        

        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return _context.TodoItems.Values;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(int id)
        {
            if (!_context.TodoItems.TryGetValue(id, out TodoItem item))
            {
                return NotFound();
            };

            return item;
        }

        // POST: api/TodoItems
        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem todoItem)
        {
            // start work 
            AddItemAsync(todoItem);

            // response with 202 + location + and interval 
            return Accepted($"https://localhost:7177/api/status/{todoItem.Id}");
            
            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        private async Task AddItemAsync(TodoItem item)
        {
            _logger.LogInformation("Started work");

            await Task.Delay(30000);
            _ = _context.TodoItems.TryAdd(item.Id, item);

            _logger.LogInformation("Finished work");
        }
    }
}
