using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly ILogger<StatusController> _logger;

        public StatusController(TodoContext context, ILogger<StatusController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult GetStatus(int id)
        {
            if(!_context.TodoItems.TryGetValue(id, out var item))
            {
                _logger.LogInformation($"Id: {id} is not ready yet");
                return Accepted($"https://localhost:7177/api/status/{id}");
            }
            else
            {
                _logger.LogInformation($"Redirecting to id {id}");
                return Redirect($"https://localhost:7177/api/todoitems/{id}");
            }    
        }
    }
}
