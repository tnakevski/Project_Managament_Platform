using PMP.AppServices.DTO_s.ProjectDTO_s;
using PMP.AppServices.DTO_s.UserDTO_s;
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
            : base(context)
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

        public bool ChangeDate(int id, string dueDate)
        {
            try
            {
                Project project = _uWork.ProjectRepo.GetById(id);
                project.DueDate = Convert.ToDateTime(dueDate);
                _uWork.ProjectRepo.Update(project);
                _uWork.ProjectRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeDescription(int id, string description)
        {
            try
            {
                Project project = _uWork.ProjectRepo.GetById(id);
                project.Description = description;
                _uWork.ProjectRepo.Update(project);
                _uWork.ProjectRepo.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public object GetUsersToAssing(int id)
        {
            List<User> users = _uWork.UserRepo.GetNotAssignedUsers(id);
            List<UserToAssignDTO> usersDto = ConvertToDTO.ConvertToUserToAssingDTO(users);
            return usersDto;
        }

        public List<UserToAssignDTO> AssignUser(List<int> userIds, int projectId)
        {
            List<User> users = new List<User>();
            foreach (int id in userIds)
            {
                ProjectUser relation = new ProjectUser();
                relation.UserId = id;
                relation.ProjectId = projectId;
                relation.isAdmin = false;
                User user = _uWork.UserRepo.GetById(id);
                users.Add(user);
                _uWork.ProjectUserRepo.Create(relation);
            }
            _uWork.ProjectUserRepo.Save();

            List<UserToAssignDTO> userDto = ConvertToDTO.ConvertToUserToAssingDTO(users);
            return userDto;
        }

        public void DeleteProject(int id)
        {
            Project project = _uWork.ProjectRepo.GetById(id);
            _uWork.ProjectRepo.Delete(project);
            _uWork.ProjectRepo.Save();
        }
    }
}
