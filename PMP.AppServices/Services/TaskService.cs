using PMP.AppServices.DTO_s.TaskDTO_s;
using PMP.AppServices.Enums;
using PMP.AppServices.Helpers;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
