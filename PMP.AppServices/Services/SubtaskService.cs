using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMP.AppServices.Services
{
    public class SubtaskService : BaseService
    {
        public SubtaskService(PMPDBEntities context)
            :base(context)
        {

        }

        public int CreateSubtask(string title, string description, int taskId)
        {
            var user = (User)HttpContext.Current.Session["User"];
            Subtask subtask = new Subtask();
            subtask.Title = title;
            subtask.Description = description;
            subtask.TaskId = taskId;
            subtask.Status = false;
            _uWork.SubtaskRepo.Create(subtask);
            _uWork.SubtaskRepo.Save();

            return subtask.Id;
        }

        public bool DeleteSubtask(int subtaskId)
        {
            Subtask subtask = _uWork.SubtaskRepo.GetById(subtaskId);
            _uWork.SubtaskRepo.Delete(subtask);
            _uWork.SubtaskRepo.Save();
            return true;
        }

        public object StatusFalse(int Id)
        {
            Subtask subtask = _uWork.SubtaskRepo.GetById(Id);
            subtask.Status = false;
            _uWork.SubtaskRepo.Update(subtask);
            _uWork.SubtaskRepo.Save();
            return true;
        }

        public object StatusTrue(int Id)
        {
            Subtask subtask = _uWork.SubtaskRepo.GetById(Id);
            subtask.Status = true;
            _uWork.SubtaskRepo.Update(subtask);
            _uWork.SubtaskRepo.Save();
            return true;
        }

        public string GetDescription(int Id)
        {
            Subtask subtask = _uWork.SubtaskRepo.GetById(Id);
            string description = subtask.Description;
            return description;
        }
    }
}
