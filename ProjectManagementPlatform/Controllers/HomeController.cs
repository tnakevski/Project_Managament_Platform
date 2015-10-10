using ProjectManagementPlatform.Models.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMP.Repositories.DataSQL.Repositories;
using PMP.Core.Entities;
using ProjectManagementPlatform.Filters;
using PMP.AppServices.Services;

namespace ProjectManagementPlatform.Controllers
{
    [AuthenticationFilter]
    public class HomeController : Controller
    {

        HomeService _homeService;
        public HomeController()
        {
            PMPDBEntities context = new PMPDBEntities();
            _homeService = new HomeService(context);
        }

        public ActionResult Welcome()
        {
            return View();
        }

        //GET
        public PartialViewResult ProjectList()
        {
            //return list of projects for logged user
            List<Project> projects = _homeService.GetProjectsForUser();
            return PartialView("../Partials/LayoutPartials/_projectList", projects);
        }

        public PartialViewResult Notifications()
        {
            List<testNotifications> notification = new List<testNotifications>{
                new testNotifications{Title="A new order has been placed", Date="5 hours ago", Icon="order", IconColor="green"},
                new testNotifications{Title="Completed task", Date="2 days ago", Icon="user", IconColor="green"},
                new testNotifications{Title="Settings updated", Date="2 day ago", Icon="settings", IconColor="red"},
                new testNotifications{Title="Task 1 Completed", Date="5 hours ago", Icon="settings", IconColor="red"},
                 //new testNotifications{Title="Task 2 Completed", Date="1 hour ago", Icon="user", IconColor="green"}
            };

            return PartialView("../Partials/LayoutPartials/_notifications", notification);
        }

        public PartialViewResult Messages()
        {
            List<testMessages> messages = new List<testMessages>{
                new testMessages{Avatar="/assets/portraits/2.jpg", User="Mary Adams",Date="30 minutes ago",Message="Anyways, I would like just do it"},
                new testMessages{Avatar="/assets/portraits/3.jpg", User="Caleb Richards",Date="20 minutes ago",Message="I ckecked, it is fine"},
                new testMessages{Avatar="/assets/portraits/4.jpg", User="June Lane",Date="10 minutes ago",Message="I would like you to check Task 1"},
            };

            return PartialView("../Partials/LayoutPartials/_messages", messages);
        }
    }
}