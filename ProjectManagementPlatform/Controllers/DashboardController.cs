using PMP.AppServices.DTO_s.ProjectDTO_s;
using PMP.AppServices.Services;
using PMP.Core.Entities;
using ProjectManagementPlatform.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagementPlatform.Controllers
{
    [AuthenticationFilter]
    public class DashboardController : Controller
    {
        ProjectService _projectService;
        public DashboardController()
        {
            PMPDBEntities _context = new PMPDBEntities();
            _projectService = new ProjectService(_context);
        }
        
        [HttpPost]
        public ActionResult CreateProject(CreateProjectDTO dto)
        {
            _projectService.CreateProject(dto);
            return RedirectToAction("Welcome", "Home");
        }

        public ActionResult ProjectPanel(int id)
        {
            //get project for main project panel
            var project = _projectService.GetProjectById(id);
            return View(project);
        }

        [HttpPost]
        public JsonResult ChangeProjectTitle(int id, string title)
        {
            bool result = _projectService.ChangeTitle(id, title);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}