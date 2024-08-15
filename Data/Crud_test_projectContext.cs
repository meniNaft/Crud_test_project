using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Crud_test_project.Models;

namespace Crud_test_project.Data
{
    public class Crud_test_projectContext : DbContext
    {
        public Crud_test_projectContext (DbContextOptions<Crud_test_projectContext> options)
            : base(options)
        {
        }

        public DbSet<Crud_test_project.Models.ToDoModel> ToDoModel { get; set; } = default!;
    }
}
