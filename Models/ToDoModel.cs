using System.ComponentModel.DataAnnotations;

namespace Crud_test_project.Models
{
    public class ToDoModel
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
    }

    public class ToDoList
    {
        public List<ToDoModel> ToDos { get; set; }
    }
}
