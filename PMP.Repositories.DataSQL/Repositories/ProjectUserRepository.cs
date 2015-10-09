using PMP.Core.Entities;
using PMP.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class ProjectUserRepository : BaseRepository<ProjectUser> , IProjectUserRepository
    {
        public ProjectUserRepository(PMPDBEntities context)
            :base(context)
        {

        }

        public ProjectUser GetSpecificProjectUser(int projectId, int userId)
        {
            ProjectUser projectUser = _context.ProjectUsers.Where(x => x.ProjectId == projectId && x.UserId == userId).FirstOrDefault();
            return projectUser;
        }
    }
}
