using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Models
{
    public class _CurrentPlansTableViewModel
    {
        private DAL dal = new DAL();
        private List<Plan> AllPlans { get; set; }
        public List<CurrentPlanModel> CurrentPlanList { get; set; }

        public _CurrentPlansTableViewModel()
        {
            CurrentPlanList = new List<CurrentPlanModel>();

            //Get all of the plans that are in planning
            AllPlans = dal.GetAllCurrentPlans();

            //Iterate through AllPlans and create the currentplanlist. CurrentPlan computes data for table
            foreach (Plan plan in AllPlans)
            {
                CurrentPlanModel cpm = new CurrentPlanModel(plan);
                CurrentPlanList.Add(cpm);
            }

        }
    }
}