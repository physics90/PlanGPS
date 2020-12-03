using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Models
{
    public class _EventTableViewModel
    {
        DAL dal = new DAL();
        public Plan Plan { get; set; }
        public List<Event> SortedEventsByDate { get; set; }
        public int NewEventType { get; set; }
        public string NewEventDateString { get; set; }
        public string WarningMessage { get; set; }

        public _EventTableViewModel()
        {
            Plan = new Plan();
        }

        public _EventTableViewModel(int planID)
        {
            if (planID != -1)
            {
                Plan = dal.GetPlanWithPlanID(planID);

                SortedEventsByDate = (from e in Plan.Events
                                      orderby e.EventDate
                                      select e).ToList();

                WarningMessage = "";

                if (Plan.Events.Count() > 0)
                {
                    if (Plan.Events.Any(e => e.EventType.StageID == 0))
                    {
                        if (SortedEventsByDate[0].EventType.StageID != 0)
                        {
                            WarningMessage = "Please check dates of events. Planning CT should be first.";
                        }
                    }
                    else
                    {
                        WarningMessage = "Make sure to enter Planning CT event.";
                    }
                }
            }
        }

        public List<EventType> AllStagesList()
        {
            bool isLOT = Plan.PlanType.IsLOT;
            List<EventType> stageList = dal.GetAllEventTypeStages(isLOT);
            return stageList;
        }

        public SelectList EventTypeList()
        {
            List<EventType> etList = dal.GetAllEventTypes();

            //Remove any event type from eventlist that has already ocurred
            //Except for planning and plan review as those can happen multiple times
            //First get a list of all event types that can only be used once.
            //new List<int>() { 1, 2, 3, 4, 5, 8, 9, 10, 11 };

            List<EventType> singularInstanceStageList = (from ev in etList
                                                         where ev.AllowMultiple == false
                                                         select ev).ToList();
                
            //If the sortedeventlist for the current plan has an event that can be used only once remove from list
            if (SortedEventsByDate.Count() >= 0)
            {
                foreach (EventType et in singularInstanceStageList)
                {
                    if (SortedEventsByDate.Any(ev => ev.EventType.StageID == et.StageID))
                    {
                        etList.RemoveAll(ev => ev.StageID == et.StageID);
                        //etList.RemoveAt(etList.FindIndex(ev => ev.StageID == et.StageID));
                    }

                    if (!Plan.PlanType.IsLOT)
                    {
                        etList.Remove(etList.Find(etl => etl.Name == "LOT Sim Completed"));
                    }
                }
            }

            SelectList sl = new SelectList(etList, "ID", "Name");

            return sl;
        }

        private List<Event> GetSortEventsByDateForPlanID()
        {
            SortedEventsByDate = (from e in Plan.Events
                                 orderby e.EventDate
                                 select e).ToList();

            return SortedEventsByDate;

        }
    }
}