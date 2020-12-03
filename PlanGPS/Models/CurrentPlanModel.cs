using DataAccessLayer;
using DataModelLayer;
using PlanGPS.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanGPS.Models
{
    public class CurrentPlanModel
    {
        DAL dal = new DAL();

        public Plan Plan { get; set; }
        public EventType CurrentStageType { get; set; }
        public string CurrentStage { get; set; }
        public int DaysInStage { get; set; }
        public int TotalDays { get; set; }
        public DateTime CTDate { get; set; }

        //private List<EventType> TypeList { get; set; }

        public CurrentPlanModel(Plan plan)
        {
            Plan = plan;
            List<Event> evList = dal.GetAllStagesForPlan(Plan.ID).OrderBy(p => p.EventDate).ToList();
            Event lastEvent = dal.GetAllStagesForPlan(plan.ID).OrderBy(p => p.EventDate).Last();
            List<EventType>  TypeList = dal.GetAllEventTypeStages(plan.PlanType.IsLOT).OrderBy(p => p.StageID).ToList();

            if (evList.Count() == 0 || evList == null)
            {
                CurrentStage = "Waiting on Planning CT";
                DaysInStage = 0;
                TotalDays = 0;
                return;
            }

            //int dayShift = Plan.PlanType.IsLOT && evList.Last().EventType.StageID == 2 ? 2 : 1;

            int dayShift = Plan.PlanType.IsLOT && evList.Last().EventType.StageID == 2 ? 0 : 1;

            CurrentStageType = TypeList.Find(p => p.StageID == evList.Last().EventType.StageID + dayShift);
            CurrentStage = CurrentStageType.CurrentStageName;
            DaysInStage = DateTime.Today.Subtract(evList.Last().EventDate).Days;
            TotalDays = DateTime.Today.Subtract(evList.First().EventDate).Days;

        }

    }
}