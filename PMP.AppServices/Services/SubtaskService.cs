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
            _uWork.SubtaskRepo.Create(subtask);
            _uWork.SubtaskRepo.Save();

            return subtask.Id;
        }
    }
}
