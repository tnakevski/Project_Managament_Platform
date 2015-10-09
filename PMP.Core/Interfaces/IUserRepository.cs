using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Core.Interfaces
{
    public interface IUserRepository
    {
        User FindByUsernameAndPass(string username, string password);
        bool CheckIfExists(string username, string mail);
        List<User> GetNotAssignedUsers(int id);
        List<User> GetNotAssignedTaskUsers(int taskId, int projectId);
    }
}
