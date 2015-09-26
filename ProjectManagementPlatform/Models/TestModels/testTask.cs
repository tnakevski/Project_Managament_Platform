using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagementPlatform.Models.TestModels
{
    public class testTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
    }
}