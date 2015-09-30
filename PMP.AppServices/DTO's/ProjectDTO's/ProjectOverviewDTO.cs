using PMP.AppServices.DTO_s.UserDTO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.DTO_s.ProjectDTO_s
{
    public class ProjectOverviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<TaskDTO_s.TaskDTO> Tasks { get; set; }
        public List<UserAssignedDTO> Users { get; set; }
        public bool isAdmin { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DueDate { get; set; }
    }
}
