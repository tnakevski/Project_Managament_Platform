using PMP.Core.Entities;
using PMP.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(PMPDBEntities context)
            :base(context)
        {

        }

        public List<Project> GetProjectsForUser(int id)
        {
            //get list of projects for the loged user
            List<Project> projects = _context.ProjectUsers.Where(x => x.UserId == id).ToList().Select(x => x.Project).ToList();
            return projects;
        }

    }
}
