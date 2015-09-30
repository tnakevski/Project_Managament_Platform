using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.DTO_s.UserDTO_s
{
    public class UserAssignedDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isAdmin { get; set; }
        public string Avatar { get; set; }

        public string GetFullName()
        {
            return String.Format("{0} {1}", FirstName, LastName);
        }
    }
}
