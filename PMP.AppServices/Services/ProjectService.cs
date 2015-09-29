using PMP.AppServices.DTO_s.ProjectDTO_s;
using PMP.AppServices.Helpers;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMP.AppServices.Services
{
    public class ProjectService : BaseService
    {
        public ProjectService(PMPDBEntities context)
            :base(context)
        {

        }

        public void CreateProject(CreateProjectDTO dto)
        {
            var project = ConvertToModel.ConvertToProject(dto);
            var user = (User)HttpContext.Current.Session["User"];
            ProjectUser relation = new ProjectUser();
            relation.UserId = user.Id;
            relation.isAdmin = true;
            relation.Project = project;

            _uWork.ProjectUserRepo.Create(relation);
            _uWork.ProjectUserRepo.Save();
        }

        public ProjectOverviewDTO GetProjectById(int id)
        {
            Project project = _uWork.ProjectRepo.GetById(id);
            var projectDTO = ConvertToDTO.ConvertToProjectOverview(project);
            return projectDTO;
        }
    }
}
