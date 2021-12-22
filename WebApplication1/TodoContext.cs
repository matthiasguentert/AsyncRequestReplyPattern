using WebApplication1.Model;

namespace WebApplication1
{
    public class TodoContext
    {
        public Dictionary<int, TodoItem> TodoItems { get; set; } = new Dictionary<int, TodoItem>();
    }
}
