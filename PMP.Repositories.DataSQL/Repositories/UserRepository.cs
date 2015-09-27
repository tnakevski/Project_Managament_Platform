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
    }
}
