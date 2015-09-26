using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class TaskLogRepository : BaseRepository<TaskLog>
    {
        public TaskLogRepository(PMPDBEntities context)
            :base (context)
        {

        }
    }
}
