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

        public bool HasPlanInPlanning(Patient pat)
        {
            List<Plan> plans = dal.GetAllPlansWithPatientID(pat.ID);

            bool inPlanning = plans.Count == 0 ? false : plans.Any(c => c.IsInPlanning);

            return inPlanning;

            //if (plans.Count == 0)
            //{
            //    return false;
            //}

            //bool ans = plans.Any(c => c.IsInPlanning)
        }
    }
}