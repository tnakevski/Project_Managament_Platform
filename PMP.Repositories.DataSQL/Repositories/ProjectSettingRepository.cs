using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Repositories.DataSQL.Repositories
{
    public class ProjectSettingRepository: BaseRepository<ProjectSetting>
    {
        public ProjectSettingRepository(PMPDBEntities context)
            : base(context)
        {

        }
    }
}
