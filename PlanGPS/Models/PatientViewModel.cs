using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModelLayer;
using DataAccessLayer;

namespace PlanGPS.Models
{
    public class PatientViewModel
    {
        DAL dal = new DAL();

        public List<Patient> PatientList { get; set; }

        public PatientViewModel()
        {
            PatientList = dal.GetAllPatients();
        }
    }
}