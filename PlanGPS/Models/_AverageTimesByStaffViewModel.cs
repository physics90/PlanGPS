using DataModelLayer;
using DataAccessLayer;
using PlanGPS.PlotModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanGPS.Models
{
    public class _AverageTimesByStaffViewModel
    {
        public List<DataStatsPlottingModel>  Stats { get; set; }
        public List<EventType> XAxisCategories { get; set; }

        public _AverageTimesByStaffViewModel(string staffType)
        {
            if (staffType == "physics")
            {
                GetPhysicsData();
            }
            else if (staffType == "radonc")
            {
                GetRadOncData();
            }
        
        }

        private void GetPhysicsData()
        {
            DAL dal = new DAL();

            List<Plan> allPlans = dal.GetAllCompletedPlans();

            //Setup list of physics event types
            List<EventDurationCategory> evCategoryList = dal.GetEventDurationCategories("Physics");

            foreach (EventDurationCategory eventDurationCategory in evCategoryList)
            {
                DataStatsPlottingModel data = new DataStatsPlottingModel(); //avg, max, min


                foreach (Plan plan in allPlans)
                {
                    foreach (Event planEvent in plan.Events)
                    {

                    }
                    List<Event> physicsEvents = plan.Events.Where(ev => ev.EventType.EventDurationCategory.Name == "Physics").ToList();


                }
            }

            
            
        }

        private void GetRadOncData()
        {

        }
    }
}