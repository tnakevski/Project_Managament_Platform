using PMP.AppServices.DTO_s.TaskDTO_s;
using PMP.AppServices.DTO_s.UserDTO_s;
using PMP.AppServices.Enums;
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
    public class TaskService : BaseService
    {
        public TaskService(PMPDBEntities context)
            : base(context)
        {

        }

        public int CreateTask(CreateTaskDTO dto)
        {
            ProjectUser relation = new ProjectUser();
            PMP.Core.Entities.Task task = ConvertToModel.ConvertToTask(dto);
            task.ProjectId = dto.ProjectId;
            _uWork.TaskRepo.Create(task);
            _uWork.TaskRepo.Save();
            return task.Id;
        }

        public TaskOverViewAdminDTO GetTaskOverView(int Id)
        {
            var task = _uWork.TaskRepo.GetById(Id);
            TaskOverViewAdminDTO taskDto = ConvertToDTO.ConvertToTaskOverviewAdmin(task);
            return taskDto;
        }

        public bool ChangeTaskTitle(int Id, string title)
        {
            PMP.Core.Entities.Task task = _uWork.TaskRepo.GetById(Id);
            task.Title = title;
            _uWork.TaskRepo.Update(task);
            _uWork.TaskRepo.Save();
            return true;
        }

        public bool ChangeTaskStatus(int Id, string status)
        {
            PMP.Core.Entities.Task task = _uWork.TaskRepo.GetById(Id);
            task.Status = (byte)((TaskStatusEnum) Enum.Parse(typeof(TaskStatusEnum), status));
            _uWork.TaskRepo.Update(task);
            _uWork.TaskRepo.Save();
            return true;
        }

        public bool ChangeTaskDate(int Id, string date)
        {
            PMP.Core.Entities.Task task = _uWork.TaskRepo.GetById(Id);
            task.DueDate = Convert.ToDateTime(date);
            _uWork.TaskRepo.Update(task);
            _uWork.TaskRepo.Save();
            return true;
        }

        public void DeleteTask(int Id)
        {
            PMP.Core.Entities.Task task = _uWork.TaskRepo.GetById(Id);
            _uWork.TaskRepo.Delete(task);
            _uWork.TaskRepo.Save();
        }

        public object GetUsersToAssign(int taskId, int projectId)
        {
            List<User> users = _uWork.UserRepo.GetNotAssignedTaskUsers(taskId, projectId);
            List<UserToAssignDTO> usersDto = ConvertToDTO.ConvertToUserToAssingDTO(users);
            return usersDto;
        }

        public object AssignUsers(List<int> userIds, int projectId, int taskId)
        {
            List<User> users = new List<User>();
            PMP.Core.Entities.Task task = _uWork.TaskRepo.GetById(taskId);
            foreach (var id in userIds)
            {
                ProjectUser projectUser = _uWork.ProjectUserRepo.GetSpecificProjectUser(projectId, id);
                task.ProjectUsers.Add(projectUser);
                User user = _uWork.UserRepo.GetById(id);
                users.Add(user);
            }
            _uWork.TaskRepo.Update(task);
            _uWork.TaskRepo.Save();
            List<UserToAssignDTO> usersDto = ConvertToDTO.ConvertToUserToAssingDTO(users);
            return usersDto;
        }
    }
}
