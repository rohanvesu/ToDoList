using Newtonsoft.Json;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TodoService
    {
        private readonly string _folderPath = "Data";
        private readonly string _fileName = "todos.json";
        private readonly string _filePath;
        private List<Todo> _todos;

        public TodoService()
        {
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            _filePath = Path.Combine(_folderPath,_fileName);

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");

            _todos = JsonConvert.DeserializeObject<List<Todo>>(File.ReadAllText(_filePath)) ?? new List<Todo>();
        }

        public List<Todo> GetAll() => _todos;

        public void Add(Todo todo)
        {
            todo.Id = Convert.ToString(Guid.NewGuid());
            _todos.Add(todo);
            Save();
        }

        public void MarkCompleted(string id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
                Save();
            }
        }

        private void Save() =>
            File.WriteAllText(_filePath, JsonConvert.SerializeObject(_todos, Formatting.Indented));
    }
}
