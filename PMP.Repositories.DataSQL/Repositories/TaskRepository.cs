using PMP.Core.Entities;
using PMP.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class TaskRepository : BaseRepository<PMP.Core.Entities.Task>
    {
        public TaskRepository(PMPDBEntities context)
            :base(context)
        {

        }
    }
}
