using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMP.AppServices.Services
{
    public class HomeService : BaseService
    {
        public HomeService(PMPDBEntities context)
            :base (context)
        {

        }

        public List<Project> GetProjectsForUser()
        {
            //get logged in user
            User user = (User)HttpContext.Current.Session["User"];
            //get list of projects
            List<Project> projects = _uWork.ProjectRepo.GetProjectsForUser(user.Id);
            return projects;
        }
    }
}
