using PMP.AppServices.Services;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementPlatform.Controllers
{
    public class SubtaskController : Controller
    {
        SubtaskService _subtaskService;
        public SubtaskController()
        {
            PMPDBEntities context = new PMPDBEntities();
            _subtaskService = new SubtaskService(context);
        }

        public JsonResult CreateSubtask(string title, string description, int taskId)
        {
            int id = _subtaskService.CreateSubtask(title, description, taskId);
            return Json(id, JsonRequestBehavior.AllowGet);
        }
	}
}