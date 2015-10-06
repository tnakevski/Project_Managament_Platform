using PMP.AppServices.DTO_s.TaskDTO_s;
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
    }
}
