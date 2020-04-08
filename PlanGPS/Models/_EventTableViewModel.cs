using System;
using System.Collections.Generic;
using System.Linq;
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
                    if (Plan.Events.Any(e => e.EventType.StageID == "0"))
                    {
                        if (SortedEventsByDate[0].EventType.StageID != "0")
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

        public SelectList EventTypeList()
        {
            List<EventType> etList = dal.GetAllEventTypes();
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