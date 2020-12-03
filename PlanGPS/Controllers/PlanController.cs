using PlanGPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataModelLayer;
using PlanGPS.HelperClasses;

namespace PlanGPS.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        //public ActionResult ManagePlanEvents(int patientID)
        //{
        //    ManagePlanEventsViewModel mvm = new ManagePlanEventsViewModel(patientID);

        //    return View(mvm);
        //}

        public ActionResult ManagePlanEvents(int patientID, int planID)
        {
            ManagePlanEventsViewModel mvm = new ManagePlanEventsViewModel(patientID, planID);

            return View(mvm);
        }

        public ActionResult _PlanTable(int patientID)
        {
            ManagePlanEventsViewModel mvm = new ManagePlanEventsViewModel(patientID);

            return PartialView(mvm);
        }

        public ActionResult AddPlan(int patientID)
        {
            AddPlanViewModel apvm = new AddPlanViewModel(patientID) { };

            return View(apvm);
        }

        [HttpPost]
        public ActionResult AddPlan([Bind(Include = "PatientID, PhysicistID, RadOncID, NeuroID, PlanTypeID, PlanName, IsInPlanning, HasPreApproval")] AddPlanViewModel apvm)
        {
            //send to database
            DAL dal = new DAL();
             
            bool success = dal.AddNewPlan(apvm.PatientID, apvm.IsInPlanning, apvm.HasPreApproval, apvm.PhysicistID, apvm.RadOncID, apvm.NeuroID, apvm.PlanName, apvm.PlanTypeID);

            ViewBag.Success = success ? "Plan has been saved to the database." : "There was an error saving to the database.";
            return View( new AddPlanViewModel(apvm.PatientID));
        }

        public ActionResult _EventsTable(int planID)
        {
            //Gets all events for plan
            _EventTableViewModel evm = new _EventTableViewModel(planID);

            ViewBag.EventWarning = "";
            if (planID != -1)
            {
                ViewBag.EventWarning = evm.WarningMessage;
            }
            
            return PartialView(evm);
        }

        [HttpPost]
        public bool SaveNewEvent(int planID, int eventTypeID, string eventDateString)
        {
            DateTime dt;    
            bool converted = DateTime.TryParse(eventDateString, out dt);

            if (!converted) return false; //If date can't parse then return false

            DAL dal = new DAL();
            bool success = dal.AddNewEvent(planID, eventTypeID, dt);

            //Set isinplanning to false
            //Get list of all eventtypes sorted by stageid
            Plan plan = dal.GetPlanWithPlanID(planID);
            List<EventType> etList = dal.GetAllEventTypeStages(plan.PlanType.IsLOT).OrderBy(et => et.StageID).ToList();
            if (eventTypeID == etList.Last().ID)
            {
                success = dal.SetPlanToComplete(planID);
            }

            //If plan is complete then compute stats
            //Works also if updating dates for a completed plan
            if (!plan.IsInPlanning)
            {
                ComputeStats cs = new ComputeStats(planID);
                success = cs.Compute();
            }

            return success;
        }

        [HttpPost]
        public bool DeleteEvent(int eventID)
        {
            DAL dal = new DAL();
            bool success = dal.DeleteEvent(eventID);

            return success;

        }
    }
}