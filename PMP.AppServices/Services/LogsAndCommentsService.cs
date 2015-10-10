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

        public bool AddComment(int taskId, string comment)
        {
            User user = (User)HttpContext.Current.Session["User"];
            TaskComment taskComment = new TaskComment();
            taskComment.Comment = comment;
            taskComment.TaskId = taskId;
            taskComment.UserId = user.Id;
            _uWork.TaskCommentRepo.Create(taskComment);
            _uWork.TaskCommentRepo.Save();
            return true;
        }
    }
}
