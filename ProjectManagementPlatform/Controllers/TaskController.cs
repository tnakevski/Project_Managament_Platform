using Microsoft.AspNet.SignalR;
using PMP.AppServices.DTO_s.TaskDTO_s;
using PMP.AppServices.Services;
using PMP.Core.Entities;
using ProjectManagementPlatform.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementPlatform.Controllers
{
    public class TaskController : Controller
    {
        TaskService _taskService;
        IHubContext _hubContext;
        public TaskController()
        {
            PMPDBEntities context = new PMPDBEntities();
            _taskService = new TaskService(context);
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<BaseHub>();
        }
        public JsonResult CreateTask(CreateTaskDTO dto)
        {
           int id =  _taskService.CreateTask(dto);
           return Json(id, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult TaskOverview(int Id)
        {
            TaskOverViewDTO dto = _taskService.GetTaskOverView(Id);
            switch (dto.UserRole)
            {
                case "admin":
                    return PartialView("../Partials/PanelPartials/_taskOverviewPanel", dto);
                case "assigned":
                    return PartialView("../Partials/PanelPartials/_taskOverviewAssigned", dto);
                case "notAssigned":
                    return PartialView("../Partials/PanelPartials/_taskOverviewNotAssigned", dto);
            }
            return PartialView("../Partials/PanelPartials/_taskOverviewPanel", dto);
        }

        public JsonResult ChangeTaskTitle(int Id, string title)
        {
            var result = _taskService.ChangeTaskTitle(Id, title);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeTaskStatus(int Id, string status)
        {
            var result = _taskService.ChangeTaskStatus(Id, status);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeTaskDate(int Id, string date)
        {
            var result = _taskService.ChangeTaskDate(Id, date);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DeleteTask(int Id)
        {
            _taskService.DeleteTask(Id);
            return PartialView("../Partials/PanelPartials/_taskNotSelectedOverview");
        }

        public JsonResult GetUsersToAssign(int taskId, int projectId)
        {
            var users = _taskService.GetUsersToAssign(taskId,projectId);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AssignUserTask(List<int> userIds, int projectId, int taskId)
        {
            var users = _taskService.AssignUsers(userIds, projectId, taskId);
            return Json(users, JsonRequestBehavior.AllowGet);
        }
	}
}