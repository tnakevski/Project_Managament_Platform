using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.DTO_s.ProjectDTO_s
{
    public class CreateProjectDTO
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Description { get; set; }
    }
}
