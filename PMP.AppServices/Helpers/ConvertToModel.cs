using PMP.AppServices.DTO_s.AccountDTO_s;
using PMP.AppServices.DTO_s.ProjectDTO_s;
using PMP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMP.AppServices.Helpers
{
    public class ConvertToModel
    {
        public static User ConvertToUser(RegisterDTO dto)
        {
            User user = new User();
            user.Email = dto.Email;
            user.LastName = dto.LastName;
            user.FirstName = dto.FirstName;
            user.Username = dto.Username;
            user.Password = dto.Password;
            user.Avatar = String.Format("{0}_{1}", user.Username, dto.File.FileName);

            return user;
        }

        public static Project ConvertToProject(CreateProjectDTO dto)
        {
            Project project = new Project();
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.DueDate = dto.DueDate;
            project.DateCreated = DateTime.Now;
            return project;
        }
    }
}
