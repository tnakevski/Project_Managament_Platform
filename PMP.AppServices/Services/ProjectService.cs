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
            //get selected project by id
            Project project = _uWork.ProjectRepo.GetForOverview(id);
            //convert model project to DTO project
            ProjectOverviewDTO projectDTO = ConvertToDTO.ConvertToProjectOverview(project);
            return projectDTO;
        }


        public bool ChangeTitle(int id, string title)
        {
            try
            {
                Project project = _uWork.ProjectRepo.GetById(id);
                project.Title = title;
                _uWork.ProjectRepo.Update(project);
                _uWork.ProjectRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
