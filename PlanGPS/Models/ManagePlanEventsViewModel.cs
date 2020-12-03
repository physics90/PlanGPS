using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Models
{
    public class ManagePlanEventsViewModel
    {
        DAL dal = new DAL();
        public Patient Patient { get; set; }
        public List<Plan> PlanList { get; set; }
        public int InitialPlanID { get; set; }

        public ManagePlanEventsViewModel(int patientID)
        {
            Patient = dal.GetPatientByID(patientID);
            PlanList = dal.GetAllPlansWithPatientID(patientID);
            InitialPlanID = -1;
        }

        public ManagePlanEventsViewModel(int patientID, int planID)
        {
            Patient = dal.GetPatientByID(patientID);
            PlanList = dal.GetAllPlansWithPatientID(patientID);
            InitialPlanID = planID;
        }

        public string GetCurrentStageForPlanID(int planID)
        {
            Plan plan = dal.GetPlanWithPlanID(planID);
            Event currentEvent = null;
            if (plan.Events.Count != 0)
            {
                currentEvent = (from e in plan.Events
                                     where e.EventType.IsAStage == true
                                     orderby e.EventDate
                                     select e).Last();
            }
            
            return currentEvent == null ? "No CurrentStage Exists." :  currentEvent.EventType.Name;
                    
        }
    }
}