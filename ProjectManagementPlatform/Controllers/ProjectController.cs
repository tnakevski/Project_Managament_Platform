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

	}
}