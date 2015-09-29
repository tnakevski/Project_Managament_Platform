using PMP.AppServices.DTO_s.AccountDTO_s;
using PMP.AppServices.Helpers;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool Register(RegisterDTO dto)
        {
            //TODO: implement function 
            bool exists = _uWork.UserRepo.CheckIfExists(dto.Username, dto.Email);
            if (exists)
                return false;

            User user = ConvertToModel.ConvertToUser(dto);
            _uWork.UserRepo.Create(user);
            var fileName = Path.GetFileName(dto.File.FileName);
            var formatedName = String.Format("{0}_{1}", user.Username, fileName);
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Avatars"), formatedName);
            dto.File.SaveAs(path);
            _uWork.UserRepo.Save();
            return true;
        }

        public void SignOut()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}
