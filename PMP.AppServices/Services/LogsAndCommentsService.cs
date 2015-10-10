using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMP.AppServices.Services
{
    public class LogsAndCommentsService : BaseService
    {
        public LogsAndCommentsService(PMPDBEntities context)
            :base(context)
        {

        }

        public object AddLog(int taskId, string description)
        {
            User user = (User)HttpContext.Current.Session["User"];
            TaskLog taskLog = new TaskLog();
            taskLog.LogDescription = description;
            taskLog.TaskId = taskId;
            taskLog.UserId = user.Id;
            taskLog.DateCreated = DateTime.Now;
            _uWork.TaskLogRepo.Create(taskLog);
            _uWork.TaskLogRepo.Save();
            return true;
        }
    }
}
