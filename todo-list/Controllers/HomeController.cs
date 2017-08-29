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

        [HttpPost("/categories")]
        public ActionResult AddCategory()
        {
            Category newCategory = new Category(Request.Form["category-name"]);
            List<Category> allCategories = Category.GetAll();
            return View("Categories", allCategories);
        }

        [HttpPost("task/add_category")]
        public ActionResult TaskAddCategory()
        {
            Category category = Category.Find(Int32.Parse(Request.Form["category-id"]));
            Task task = Task.Find(Int32.Parse(Request.Form["task-id"]));
            task.AddCategory(category);
            return View("Success");
        }

        [HttpPost("category/add_task")]
        public ActionResult CategoryAddTask()
        {
            Category category = Category.Find(Int32.Parse(Request.Form["category-id"]));
            Task task = Task.Find(Int32.Parse(Request.Form["task-id"]));
            category.AddTask(task);
            return View("Success");
        }

        [HttpGet("/categories/new")]
        public ActionResult CategoryForm()
        {
            return View();
        }

        [HttpPost("/categories/new")]
        public ActionResult CategoryCreate()
        {
            Category newCategory = new Category(Request.Form["category-name"]);
            newCategory.Save();
            return View("Success");
        }

        [HttpGet("/categories/{id}")]
        public ActionResult CategoryDetail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category SelectedCategory = Category.Find(id);
            List<Task> CategoryTasks = SelectedCategory.GetTasks();
            List<Task> AllTasks = Task.GetAll();
            model.Add("category", SelectedCategory);
            model.Add("categoryTasks", CategoryTasks);
            model.Add("allTasks", AllTasks);
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

        [HttpPost("/tasks/new")]
        public ActionResult TaskCreate()
        {
            Task newTask = new Task(Request.Form["task-description"]);
            newTask.Save();
            return View("Success");
        }

        [HttpGet("/tasks/{id}")]
        public ActionResult TaskDetail(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Task selectedTask = Task.Find(id);
            List<Category> TaskCategories = selectedTask.GetCategories();
            List<Category> AllCategories = Category.GetAll();
            model.Add("task", selectedTask);
            model.Add("taskCategories", TaskCategories);
            model.Add("allCategories", AllCategories);
            return View( model);

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
