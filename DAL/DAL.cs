using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;

namespace DataAccessLayer
{
    public class DAL
    {
        private PlanGPSContext context { get; set; }

        public DAL()
        {
            context = new PlanGPSContext();

            if (context.PlanType.Count() == 0)
            {
                LoadEventTypes();
                LoadPhysicists();
                LoadNeuros();
                LoadRadOncs();
                LoadPlanTypes();
            }
            
        }

        public List<Physicist> GetAllPhysicists()
        {
            return context.Physicist.ToList();
        }

        public Patient GetPatientByID(int patientID)
        {
            Patient p = context.Patient.Find(patientID);

            return p;
        }

        public bool AddNewPlan(int patientID, bool isInPlanning, bool hasPreApproval, int physicistID, int radOncID, int neuroID, string planName, int planTypeID)
        {
            Plan plan = new Plan();
            bool success;
            try
            {
                plan.PlanName = planName;
                plan.IsInPlanning = isInPlanning;
                plan.HasPreApproval = hasPreApproval;

                plan.Patient = context.Patient.Find(patientID);
                plan.Physicist = context.Physicist.Find(physicistID);
                plan.RadOnc = context.RadOnc.Find(radOncID);
                plan.Neuro = context.Neurosurgeon.Find(neuroID);
                plan.PlanType = context.PlanType.Find(planTypeID);

                context.Plan.Add(plan);
                context.SaveChanges();

                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public void CreateDbDefaultData(List<EventType> etList)
        {
            context.EventType.AddRange(etList);
            context.SaveChanges();
        }

        public List<RadOnc> GetAllRadOncs()
        {
            return context.RadOnc.ToList();
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patList = context.Patient.ToList();

            return patList;
        }

        public List<Neurosurgeon> GetAllNeuros()
        {
            return context.Neurosurgeon.ToList();
        }

        public IEnumerable GetPlanTypes()
        {
            return context.PlanType.ToList();
        }

        public bool AddNewPatient(Patient patient)
        {
            bool success;
            if (patient != null)
            {
                context.Patient.Add(patient);
                context.SaveChanges();
                success = true;
            }
            else
            {
                success = false;
            }

            return success;
        }

        public bool DeletePatient(int patientID)
        {
            Patient pat = context.Patient.Find(patientID);
            bool wasDeleted = true;

            if (pat != null)
            {
                try
                {
                    context.Patient.Remove(pat);
                    context.SaveChanges();
                    wasDeleted = true;
                }
                catch (Exception)
                {
                    wasDeleted = false;
                }
            }

            return wasDeleted;
        }




        //Creating Database Data
        #region Create Database Data
        public void LoadEventTypes()
        {
            List<EventType> etList = new List<EventType>();
            EventType et = new EventType()
            {
                Name = "Image Import Complete",
                Description = "Importing of all images for planning has been completed"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Imaging Complete",
                Description = "All imaging has been completed"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Contouring Complete",
                Description = "All physician contouring has been completed"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Ready for Review",
                Description = "Plan is ready for physician review"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Reviewed",
                Description = "Physician reviewed plan"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Planning Complete",
                Description = "Planning has been completed"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Physics Second Check Done",
                Description = "Plan has been second checked and ready for scheduling"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Scheduled",
                Description = "Patient has been scheduled for treatment"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Treatment Started",
                Description = "Treatment has  started"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Planning CT",
                Description = "Planning CT Date"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "Outside CT",
                Description = "Additional CT"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "MRI Imaging",
                Description = "MRI Date"
            };

            etList.Add(et);

            et = new EventType()
            {
                Name = "PET Imaging",
                Description = "PET Date"
            };

            etList.Add(et);

            context.EventType.AddRange(etList);
            context.SaveChanges();

            //foreach (EventType eventType in etList)
            //{
            //    context.EventType.AddRange
            //}


        }

        private void LoadPhysicists()
        {
            Physicist ph = new Physicist()
            {
                FirstName = "Jeff",
                LastName = "Garrett",
                IsPhysician = false
            };

            context.Physicist.Add(ph);
            context.SaveChanges();

            Physicist ph2 = new Physicist()
            {
                FirstName = "Chris",
                LastName = "Westbrook",
                IsPhysician = false
            };

            context.Physicist.Add(ph2);
            context.SaveChanges();
        }

        private void LoadRadOncs()
        {
            List<RadOnc> roList = new List<RadOnc>();
            RadOnc ro = new RadOnc()
            {
                FirstName = "Jeanann",
                LastName = "Suggs",
                IsPhysician = true
            };

            roList.Add(ro);

            ro = new RadOnc()
            {
                FirstName = "Richard",
                LastName = "Friedman",
                IsPhysician = true
            };

            roList.Add(ro);

            ro = new RadOnc()
            {
                FirstName = "Margaret",
                LastName = "Wadsworth",
                IsPhysician = true
            };

            roList.Add(ro);

            ro = new RadOnc()
            {
                FirstName = "Eric",
                LastName = "Balfour",
                IsPhysician = true
            };

            roList.Add(ro);

            context.RadOnc.AddRange(roList);
            context.SaveChanges();

        }

        private void LoadNeuros()
        {
            Neurosurgeon ns = new Neurosurgeon()
            {
                FirstName = "",
                LastName = "Vanlandingham",
                IsPhysician = true
            };

            context.Neurosurgeon.Add(ns);
            context.SaveChanges();
        }

        private void LoadPlanTypes()
        {
            List<PlanType> ptList = new List<PlanType>();

            PlanType pt = new PlanType()
            {
                Type = "Intracranial",
                Description = "Brain (Intracranial) with 6DOF Skull tracking",
                IsLOT = false
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Body, Spine Tracking",
                Description = "Body with tracking on spine",
                IsLOT = false
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Body, Fiducial (Non-Synchrony)",
                Description = "Body using fiducials for motion tracking without Synchrony",
                IsLOT = false
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Body, Synchrony",
                Description = "Body using fiducials with Synchrony",
                IsLOT = false
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Lung LOT",
                Description = "Lung using LOT Planning",
                IsLOT = true
            };

            ptList.Add(pt);
            context.PlanType.AddRange(ptList);
            context.SaveChanges();

        }
        #endregion
    }
}
