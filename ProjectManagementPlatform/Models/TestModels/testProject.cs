using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementPlatform.Models.TestModels
{
    public class testProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<testTask> Tasks { get; set; }
    }
}