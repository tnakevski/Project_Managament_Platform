using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class TaskRespository : BaseRepository<PMP.Core.Entities.Task>
    {
        public TaskRespository(PMPDBEntities context)
            :base(context)
        {

        }
    }
}
