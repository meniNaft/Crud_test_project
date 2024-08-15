namespace Crud_test_project.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        public string ToDo { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
    }

    public class ToDoList
    {
        public List<ToDoModel> ToDos { get; set; }
    }
}
