using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Crud_test_project.Data;
using Crud_test_project.Services;
using Newtonsoft.Json;
using System;
using Crud_test_project.Models;
namespace Crud_test_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Crud_test_projectContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Crud_test_projectContext") ?? throw new InvalidOperationException("Connection string 'Crud_test_projectContext' not found.")));
            HttpClient client = new HttpClient();
            builder.Services.AddSingleton(new ApiToDoService(client));
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=ToDoModels}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
