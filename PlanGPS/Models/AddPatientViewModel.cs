using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;

namespace PlanGPS.Models
{
	public class AddPatientViewModel
	{
        DAL dal = new DAL();
        public Patient patient = new Patient();

	}
}