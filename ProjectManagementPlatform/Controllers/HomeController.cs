using ProjectManagementPlatform.Models.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Enumerations;
using PMP.Repositories.DataSQL.Repositories;
using PMP.Core.Entities;

namespace ProjectManagementPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ProjectPanel(string title)
        {
            testProject project = new testProject();
            List<testTask> tasks = new List<testTask>{
                new testTask{Id = 1, Status = "Done", Title = "Task 1", StatusColor="success"},
                new testTask{Id = 2, Status = "Done", Title = "Task 2", StatusColor="success"},
                new testTask{Id = 3, Status = "Pending", Title = "Task 3", StatusColor="danger"},
                new testTask{Id = 4, Status = "In Progress", Title = "Task 4", StatusColor="warning"},
                new testTask{Id = 5, Status = "Done", Title = "Task 5", StatusColor="success"},
                new testTask{Id = 6, Status = "Pending", Title = "Task 6" ,StatusColor="danger"},
                new testTask{Id = 7, Status = "Pending", Title = "Task 7",  StatusColor="danger"},
                new testTask{Id = 8, Status = "In Progress", Title = "Task 8", StatusColor="warning"},
                new testTask{Id = 9, Status = "In Progress", Title = "Task 9", StatusColor="warning"}
            };
            project.Title = title;
            project.Tasks = tasks;
            return View(project);
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public PartialViewResult ProjectList()
        {
            List<testProject> projects = new List<testProject>{
                new testProject{Id=1, Title = "Project 1"},
                new testProject{Id=2, Title = "Project 2"},
                new testProject{Id=3, Title = "Project 3"},
                new testProject{Id=4, Title = "Project 4"},
                new testProject{Id=5, Title = "Project 5"}
            };

            return PartialView("../Partials/LayoutPartials/_projectList", projects);
        }

        public PartialViewResult Notifications()
        {
            List<testNotifications> notification = new List<testNotifications>{
                new testNotifications{Title="A new order has been placed", Date="5 hours ago", Icon="order", IconColor="green"},
                new testNotifications{Title="Completed task", Date="2 days ago", Icon="user", IconColor="green"},
                new testNotifications{Title="Settings updated", Date="2 day ago", Icon="settings", IconColor="red"},
                new testNotifications{Title="Task 1 Completed", Date="5 hours ago", Icon="settings", IconColor="red"},
                 new testNotifications{Title="Task 2 Completed", Date="1 hour ago", Icon="user", IconColor="green"}
            };

            return PartialView("../Partials/LayoutPartials/_notifications", notification);
        }

        public PartialViewResult Messages()
        {
            List<testMessages> messages = new List<testMessages>{
                new testMessages{Avatar="../assets/portraits/2.jpg", User="Mary Adams",Date="30 minutes ago",Message="Anyways, I would like just do it"},
                new testMessages{Avatar="../assets/portraits/3.jpg", User="Caleb Richards",Date="20 minutes ago",Message="I ckecked, it is fine"},
                new testMessages{Avatar="../assets/portraits/4.jpg", User="June Lane",Date="10 minutes ago",Message="I would like you to check Task 1"},
            };

            return PartialView("../Partials/LayoutPartials/_messages", messages);
        }

        public JsonResult TaskDescription()
        {
            testSubtaskOverview result = new testSubtaskOverview();
            result.Description = "Testing Subtask Description with ajax call";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}