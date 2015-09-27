using PMP.AppServices.DTO_s.AccountDTO_s;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMP.AppServices.Services
{
    public class AccountService : BaseService
    {
        public AccountService(PMPDBEntities context)
            :base(context)
        {

        }

        public bool Login(LoginDTO dto)
        {
            var user = _uWork.UserRepo.FindByUsernameAndPass(dto.Username, dto.Password);
            if (user == null)
                return false;

            HttpContext.Current.Session["User"] = user;
            return true;
        }

        public object Register(RegisterDTO dto)
        {
            //TODO: implement function 
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
