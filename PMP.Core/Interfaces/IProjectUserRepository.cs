﻿using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.Core.Interfaces
{
    public interface IProjectUserRepository
    {
        ProjectUser GetSpecificProjectUser(int projectId, int userId);
    }
}
