using PMP.AppServices.DTO_s.UserDTO_s;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.DTO_s.TaskDTO_s
{
    public class TaskOverViewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public List<UserAssignedDTO> Users { get; set; }
        public string UserRole { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public string Description { get; set; }
        public List<TaskLog> Logs { get; set; }
        public List<TaskComment> Comments { get; set; }
    }
}
