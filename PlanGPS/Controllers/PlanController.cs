using PlanGPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult ManagePatientPlans()
        {

            return View();
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

            return View( new AddPlanViewModel(apvm.PatientID));
        }
    }
}