using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class ProjectRepository : BaseRepository<Project>
    {
        public ProjectRepository(PMPDBEntities context)
            :base(context)
        {

        }
    }
}
