using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.DTO_s.TaskDTO_s
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
    }
}
