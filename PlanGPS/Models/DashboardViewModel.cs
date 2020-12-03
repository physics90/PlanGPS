using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Models
{
    public class DashboardViewModel
    {
        public bool PlansExist { get; set; }
        public DashboardViewModel()
        {
            DAL dal = new DAL();

            PlansExist = dal.GetAllPlans().Count() > 0 ? true : false;
        }
    }
}