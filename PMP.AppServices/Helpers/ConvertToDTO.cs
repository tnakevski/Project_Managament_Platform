using PMP.AppServices.DTO_s.ProjectDTO_s;
using PMP.AppServices.DTO_s.TaskDTO_s;
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
            List<TaskDTO> taskDtos = new List<TaskDTO>();
            ProjectOverviewDTO projectDto = new ProjectOverviewDTO();
            projectDto.Id = project.Id;
            projectDto.Title = project.Title;
            foreach (var task in project.Tasks)
            {
                var converted = ConvertToTaskDTO(task);
                taskDtos.Add(converted);
            }
            projectDto.Tasks = taskDtos;
            return projectDto;
        }
    }
}
