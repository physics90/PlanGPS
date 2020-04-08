using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Models
{
    public class ManagePatientPlansViewModel
    {
        DAL dal = new DAL();
        public Patient Patient { get; set; }

        public ManagePatientPlansViewModel(int patientID)
        {
            Patient = dal.GetPatientByID(patientID);


        }
    }
}