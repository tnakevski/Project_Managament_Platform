﻿using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class ProjectLogRepository : BaseRepository<ProjectLog>
    {
        public ProjectLogRepository(PMPDBEntities context)
            :base (context)
        {

        }
    }
}
