using PMP.Core.Entities;
using PMP.Repositories.DataSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.Services
{
    public class BaseService
    {
        protected UnityOfWork _uWork;
        public BaseService(PMPDBEntities context)
        {
            _uWork = new UnityOfWork(context);
        }
    }
}
