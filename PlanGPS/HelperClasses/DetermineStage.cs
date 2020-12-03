using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.HelperClasses
{
    public class DetermineStage
    {
        DAL dal = new DAL();

        public string GetCurrentStage(int planID)
        {
            //Get a list of all event types that are stages
            bool isLOT = false;
            List<EventType> typeList = dal.GetAllEventTypeStages(isLOT);

            List<Event> evList = dal.GetAllStagesForPlan(planID).OrderBy(p => p.EventType.StageID).ToList();
            //evList.OrderBy(p => p.EventType.StageID);
            int recentStage = evList.Last().EventType.StageID;
            EventType currentStage = typeList.Find(p => p.StageID == recentStage + 1);

            return currentStage.Name;

        }
        
    }
}