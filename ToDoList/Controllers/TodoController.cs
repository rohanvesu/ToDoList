using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoService _service;

        public TodoController(TodoService service) => _service = service;

        public IActionResult Index() => View(_service.GetAll());

        [HttpPost]
        public IActionResult Add([FromForm] string todos)
        {
            Todo todo = string.IsNullOrWhiteSpace(todos) ? new Todo() : JsonConvert.DeserializeObject<Todo>(todos);

            if (!string.IsNullOrWhiteSpace(todo.Title))
                _service.Add(todo);

            return RedirectToAction("Index");
        }

        [HttpPut]
        public IActionResult Complete([FromQuery]string id)
        {
            _service.MarkCompleted(id);
            return RedirectToAction("Index");
        }
    }
}
