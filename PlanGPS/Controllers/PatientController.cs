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
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Home()
        {
            PatientViewModel pvm = new PatientViewModel();
            return View(pvm);
        }

        //GET
        public ActionResult AddPatient()
        {
            AddPatientViewModel apvm = new AddPatientViewModel();
            return View(apvm);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient([Bind(Include = "LastName, FirstName, MRN")] Patient patient)
        {
            //Create a New Patient
            DAL dal = new DAL();
            bool didSave = dal.AddNewPatient(patient);

            ViewBag.SaveMessage = "Patient Saved";
            return View(new AddPatientViewModel());
            //Patient pa = new Patient()
            //{
            //    FirstName = patient
            //}
            //LeadApron apron = new LeadApron()
            //{
            //    ApronIdentifier = lavm.ApronIdentifier,
            //    Description = lavm.Description,
            //    IsRetired = lavm.IsRetired,
            //    Location = lavm.Location,
            //    InstitutionID = lavm.InstitutionID,
            //    FacilityID = lavm.FacilityID,
            //    DepartmentID = lavm.DepartmentID,
            //    LeadApronTypeID = lavm.ApronTypeID
            //};

            //bool didSave = dal.AddNewLeadApron(apron);

            //ViewBag.SaveMessage = didSave ? "Apron successfully saved to database." : "Apron was not saved to database. Please check entries. If problem persists contact system administrator.";
            //return View(lavm);
        }

        [HttpPost]
        public bool DeletePatient(int patientID)
        {
            DAL dal = new DAL();

            bool wasDeleted = dal.DeletePatient(patientID);

            return wasDeleted;
        }

        public ActionResult _PatientTable(bool allPatients)
        {
            PatientViewModel pvm = new PatientViewModel();
            return PartialView(pvm);
        }
    }
}