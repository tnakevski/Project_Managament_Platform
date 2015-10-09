using PMP.Core.Entities;
using PMP.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(PMPDBEntities context)
            :base(context)
        {

        }

        public User FindByUsernameAndPass(string username, string password)
        {
            User user = _context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            return user;
        }


        public bool CheckIfExists(string username, string mail)
        {
            return _context.Users.Any(x => x.Username == username || x.Email == mail);
        }



        public List<User> GetNotAssignedUsers(int id)
        {
            List<User> projectUsers = _context.ProjectUsers.Where(x => x.ProjectId == id).ToList().Select(x => x.User).ToList();
            List<User> allUsers = _context.Users.ToList();
            List<User> users = allUsers.Except(projectUsers).ToList();
            return users;
        }


        public List<User> GetNotAssignedTaskUsers(int taskId, int projectId)
        {
            List<User> assigned = _context.Tasks.Where(x => x.Id == taskId).FirstOrDefault().ProjectUsers.ToList().Select(x => x.User).ToList();
            List<User> projectUsers = _context.ProjectUsers.Where(x => x.ProjectId == projectId).ToList().Select(x => x.User).ToList();
            List<User> result = projectUsers.Except(assigned).ToList();
            return result;
        }
    }
}
