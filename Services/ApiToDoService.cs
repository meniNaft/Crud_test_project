using Crud_test_project.Models;
using System.Text.Json;

namespace Crud_test_project.Services
{
    public class ApiToDoService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "https://dummyjson.com/todos";
        public ApiToDoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ToDoModel>> GetToDosAsync()
        {
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var toDoList = await response.Content.ReadFromJsonAsync<ToDoList>();
            return toDoList.ToDos;
        }

        public async Task<ToDoModel> GetToDoByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var ToDo = await response.Content.ReadFromJsonAsync<ToDoModel>();
            return ToDo;
        }

        public async Task<ToDoModel> CreateToDoAsync(ToDoModel newToDo)
        {
            var response = await _httpClient.PostAsJsonAsync(baseUrl + "/add", newToDo);
            response.EnsureSuccessStatusCode();
            var createdToDo = await response.Content.ReadFromJsonAsync<ToDoModel>();
            return createdToDo;
        }

        public async Task UpdateToDoAsync(int id, ToDoModel updatedToDo)
        {
            var original = await GetToDoByIdAsync(id);
            if (original != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (original.Completed != updatedToDo.Completed) dict.Add("completed", updatedToDo.Completed); 
                if (original.ToDo != updatedToDo.ToDo) dict.Add("todo", updatedToDo.ToDo); 
                if (original.UserId != updatedToDo.UserId) dict.Add("userId", updatedToDo.UserId);

                var content = new StringContent(JsonSerializer.Serialize(dict), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/{id}", content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteToDoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
