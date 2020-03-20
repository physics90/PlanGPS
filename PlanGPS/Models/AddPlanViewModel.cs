using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataModelLayer;
using PlanGPS.HelperClasses;

namespace PlanGPS.Models
{
    public class AddPlanViewModel : Plan
    {
        DAL dal = new DAL();

        //Properties
        public Patient pat { get; set; }
        public int PatientID { get; set; }

        [Display(Name = "Physicist")]
        public int PhysicistID { get; set; }
        [Display(Name = "Rad Onc")]
        public int RadOncID { get; set; }
        [Display(Name = "Neuro")]
        public int NeuroID { get; set; }
        [Display(Name = "Plan Type")]
        public int PlanTypeID { get; set; }

        //Constructors
        public AddPlanViewModel()
        {

        }

        public AddPlanViewModel(int patientID)
        {
            pat = dal.GetPatientByID(patientID);
            PatientID = patientID;
        }

        //Methods
        public SelectList CreatePhysicistDDL()
        {
            List<Physicist> pL = dal.GetAllPhysicists();

            return new SelectList(dal.GetAllPhysicists(), "ID", "LastFirstName");
        }

        public SelectList CreateRadOncDDL()
        {
            return new SelectList(dal.GetAllRadOncs(), "ID", "LastFirstName");
        }

        public SelectList CreateNeuroDDL()
        {
            DDLCreator ddlC = new DDLCreator();

            SelectList nL = ddlC.NeuroSelectListWithNA(dal.GetAllNeuros());

            return nL;
        }

        public SelectList CreatePlanTypeDDL()
        {
            return new SelectList(dal.GetPlanTypes(), "ID", "Type");
        }
    }

}