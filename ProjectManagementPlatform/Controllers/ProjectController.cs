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
    public class ProjectController : Controller
    {
        ProjectService _projectService;
        public ProjectController()
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

        [HttpPost]
        public JsonResult ChangeProjectDate(int id, string dueDate)
        {
            bool result = _projectService.ChangeDate(id, dueDate);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeProjectDescription(int id, string description)
        {
            bool result = _projectService.ChangeDescription(id, description);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsersToAssign(int id)
        {
            var users = _projectService.GetUsersToAssing(id);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AssignUserProject(List<int> userIds, int projectId)
        {
            var users = _projectService.AssignUser(userIds, projectId);
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProject(int id)
        {
            _projectService.DeleteProject(id);
            return RedirectToAction("Welcome", "Home");
        }
	}
}