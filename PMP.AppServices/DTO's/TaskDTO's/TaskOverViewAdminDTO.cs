using PMP.AppServices.DTO_s.UserDTO_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.DTO_s.TaskDTO_s
{
    public class TaskOverViewAdminDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public List<UserAssignedDTO> Users { get; set; }
    }
}
