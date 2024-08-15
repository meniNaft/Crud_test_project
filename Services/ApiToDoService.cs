using Crud_test_project.Models;

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
            var response = await _httpClient.PutAsJsonAsync($"{baseUrl}/{id}", updatedToDo);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteToDoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
