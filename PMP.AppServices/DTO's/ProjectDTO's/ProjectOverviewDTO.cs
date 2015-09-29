using System;
using System.Collections.Generic;
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
    }
}
