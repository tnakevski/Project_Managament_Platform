using PMP.AppServices.DTO_s.TaskDTO_s;
using PMP.AppServices.Services;
using PMP.Core.Entities;
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
        public TaskController()
        {
            PMPDBEntities context = new PMPDBEntities();
            _taskService = new TaskService(context);
        }
        public JsonResult CreateTask(CreateTaskDTO dto)
        {
           int id =  _taskService.CreateTask(dto);
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult TaskOverview(int Id)
        {
            TaskOverViewAdminDTO dto = _taskService.GetTaskOverView(Id);
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
	}
}