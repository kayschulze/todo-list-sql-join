using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/categories")]
        public ActionResult Categories()
        {
            List<Category> allCategories = Category.GetAll();
            return View(allCategories);
        }

        [HttpGet("/categories/new")]
        public ActionResult CategoryForm()
        {
            return View();
        }

        [HttpPost("/categories")]
        public ActionResult AddCategory()
        {
            Category newCategory = new Category(Request.Form["category-name"]);
            List<Category> allCategories = Category.GetAll();
            return View("Categories", allCategories);
        }

        [HttpGet("/categories/{id}")]
        public ActionResult CategoryDetail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Task> categoryTasks = selectedCategory.GetTasks();
            model.Add("category", selectedCategory);
            model.Add("tasks", categoryTasks);
            return View(model);
        }

        [HttpGet("/categories/{id}/tasks/new")]
        public ActionResult CategoryTaskForm(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Task> allTasks = selectedCategory.GetTasks();
            model.Add("category", selectedCategory);
            model.Add("tasks", allTasks);
            return View(model);
        }

        [HttpGet("/tasks")]
        public ActionResult Tasks()
        {
            List<Task> allTasks = Task.GetAll();
            return View(allTasks);
        }

        [HttpGet("/tasks/new")]
        public ActionResult TaskForm()
        {
            return View();
        }

        [HttpGet("/tasks/{id}/edit")]
        public ActionResult EditTask(int id)
        {
            Task thisTask = Task.Find(id);
            return View(thisTask);
        }

        [HttpPost("/tasks/{id}/edit")]
        public ActionResult EditTaskConfirm(int id)
        {
            Task thisTask = Task.Find(id);
            thisTask.UpdateDescription(Request.Form["newname"]);
            return RedirectToAction("Index");
        }
    }
}
