﻿using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class TaskSettingRepository : BaseRepository<TaskSetting>
    {
        public TaskSettingRepository(PMPDBEntities context)
            :base(context)
        {

        }
    }
}
