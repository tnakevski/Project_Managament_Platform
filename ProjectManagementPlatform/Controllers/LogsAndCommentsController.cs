using PMP.AppServices.Services;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementPlatform.Controllers
{
    public class LogsAndCommentsController : Controller
    {
        LogsAndCommentsService _logsAndCommentsService;
        public LogsAndCommentsController()
        {
            PMPDBEntities context = new PMPDBEntities();
            _logsAndCommentsService = new LogsAndCommentsService(context);
        }


        public JsonResult AddLog(int taskId, string description)
        {
            var result = _logsAndCommentsService.AddLog(taskId, description);
            return Json(JsonRequestBehavior.AllowGet);
        }
	}
}