using PMP.AppServices.DTO_s.ProjectDTO_s;
using PMP.AppServices.DTO_s.TaskDTO_s;
using PMP.AppServices.DTO_s.UserDTO_s;
using PMP.AppServices.Enums;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.Helpers
{
    public class ConvertToDTO
    {
        public static TaskDTO ConvertToTaskDTO(PMP.Core.Entities.Task task)
        {
            //convert model task to DTO task
            TaskStatusEnum status = (TaskStatusEnum)task.Status;
            TaskStatusColorEnum statusColor = (TaskStatusColorEnum)task.Status;
            TaskDTO taskDto = new TaskDTO();
            taskDto.Title = task.Title;
            taskDto.Status = (status).ToString();
            taskDto.StatusColor = (statusColor).ToString();

            return taskDto;
        }
        public static ProjectOverviewDTO ConvertToProjectOverview(Project project)
        {
            //converts model project to DTO project
            List<TaskDTO> taskDtos = new List<TaskDTO>();
            ProjectOverviewDTO projectDto = new ProjectOverviewDTO();
            projectDto.Id = project.Id;
            projectDto.Title = project.Title;
            projectDto.Description = project.Description;
            projectDto.DueDate = project.DueDate;
            foreach (var task in project.Tasks)
            {
                //convert model task to DTO task
                var converted = ConvertToTaskDTO(task);
                taskDtos.Add(converted);
            }
            List<UserAssignedDTO> assignedUsers = new List<UserAssignedDTO>();
            List<User> users = project.ProjectUsers.Where(x => x.ProjectId == project.Id).ToList().Select(x => x.User).ToList();
            foreach (var user in users)
            {
                //convert model user to assigned user dto
                var converted = ConvertToAssignedUserDTO(user);
                assignedUsers.Add(converted);
            }
            //add converted users and tasks to project dto 
            projectDto.Users = assignedUsers;
            projectDto.Tasks = taskDtos;
            return projectDto;
        }

        //Converts User DB model to assigned user dto model 
        public static UserAssignedDTO ConvertToAssignedUserDTO(User user)
        {
            UserAssignedDTO userDto = new UserAssignedDTO();
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.isAdmin = user.ProjectUsers.Where(x => x.UserId == user.Id).FirstOrDefault().isAdmin;
            userDto.Id = user.Id;
            userDto.Avatar = user.Avatar;
            return userDto;
        }
    }
}
