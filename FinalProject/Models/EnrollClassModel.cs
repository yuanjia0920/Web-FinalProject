using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Models
{
    public class EnrollClassModel
    {
        public SelectList ClassList { get; set; }
        public int SelectedClassId { get; set; }
        public int SelectedUserId { get; set; }
    }
}