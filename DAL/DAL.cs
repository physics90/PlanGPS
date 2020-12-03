using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                LoadInitialData lid = new LoadInitialData(context);
                lid.LoadEventDurationCategories();
                lid.LoadEventTypes();
                lid.LoadPhysicists();
                lid.LoadNeuros();
                lid.LoadRadOncs();
                lid.LoadPlanTypes();
            }
            
        }

        public List<Plan> GetAllPlans()
        {
            return context.Plan.ToList();
        }

        public bool DoStatsExistForPlanWithPlanID(int planID)
        {
            bool exist = context.PlanningStats.Any(ps => ps.Plan.ID == planID);

            return exist;
        }

        public PlanningStats GetPlanningStatsWithPlanID(int planID)
        {
            PlanningStats ps = (from p in context.PlanningStats
                                where p.Plan.ID == planID
                                select p).First();

            return ps;
        }

        public int CountNumberOfPlansForRadOnc(int roID)
        {
            int numPlans = (from p in context.Plan
                            where p.RadOnc.ID == roID
                            select p).Count();

            return numPlans;
        }

        public List<EventDurationCategory> GetEventDurationCategories(string category)
        {
            List<EventDurationCategory> categories = (from ev in context.EventDurationCategory
                                                      where ev.Name == category
                                                      select ev).ToList();

            return categories;
        }

        public List<Plan> GetAllCompletedPlans()
        {
            List<Plan> plans = (from p in context.Plan
                          where p.IsInPlanning == true
                          select p).ToList();

            return plans;
        }

        public List<PlanType> GetAllPlanTypes()
        {
            List<PlanType> pt = (from p in context.PlanType
                                 select p).ToList();

            return pt;
        }

        public int CountNumberOfPlansForType(int planTypeID)
        {
            int numPlans = (from p in context.Plan
                            where p.PlanType.ID == planTypeID
                            select p).Count();

            return numPlans;
        }

        public List<Plan> GetAllCurrentPlans()
        {
            context = new PlanGPSContext();

            List<Plan> planList = (from p in context.Plan
                                   where p.IsInPlanning == true
                                   select p).ToList();

            return planList;
        }

        public bool SetPlanToComplete(int planID)
        {
            Plan plan = context.Plan.Find(planID);

            bool success;
            plan.IsInPlanning = false;
            try
            {
                context.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public List<EventType> GetAllEventTypes()
        {
            return context.EventType.ToList();
        }

        public List<EventType> GetAllEventTypeStages(bool isLot)
        {
            List<EventType> ets = new List<EventType>();

            ets = (from ev in context.EventType
                   where ev.IsAStage == true
                   select ev).ToList();

            EventType lotEvent = ets.Where(ev => ev.StageID == 2).First();
            //if (!isLot)
            //{
            //    ets.Remove(lotEvent);
            //}

            return ets;
        }

        public List<Event> GetAllStagesForPlan(int planID)
        {
            List<Event> eventList = (from ev in context.Event
                          where ev.PlanID == planID
                          where ev.EventType.IsAStage == true
                          orderby ev.EventDate
                          select ev).ToList();

            return eventList;
        }

        public Plan GetPlanWithPlanID(int planID)
        {
            return context.Plan.Find(planID);
        }

        public bool UpdateStats(PlanningStats stats)
        {
            bool success;

            try
            {
                context.PlanningStats.AddOrUpdate(stats);
                context.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public bool SaveStats(PlanningStats stats)
        {
            bool success;

            try
            {
                context.PlanningStats.AddOrUpdate(stats);
                context.SaveChanges();
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public List<Plan> GetAllPlansWithPatientID(int patientID)
        {
            List<Plan> pList = (from p in context.Plan
                                where p.Patient.ID == patientID
                                select p).ToList();
            return pList;
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
            bool success = true;
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

                //Add an initial event to plan
                EventType initialEv = (from evt in context.EventType
                                       where evt.StageID == 0
                                       select evt).First();

                bool didAddInsApproval;
                if (plan.HasPreApproval)
                {
                    EventType insuranceEvent = (from evt in context.EventType
                                                where evt.StageID == -1
                                                select evt).First();

                    try
                    {
                        didAddInsApproval = AddNewEvent(plan.ID, insuranceEvent.ID, DateTime.Now);
                    }
                    catch (Exception)
                    {
                        didAddInsApproval = false;
                        success = false;
                    }
                }

                bool didAddEvent;
                try
                {
                    didAddEvent = AddNewEvent(plan.ID, initialEv.ID, DateTime.Now);
                }
                catch (Exception)
                {
                    didAddEvent = false;
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }         
           
            return success;
        }

        public bool DeleteEvent(int eventID)
        {
            bool success;

            try
            {
                Event ev = context.Event.Find(eventID);
                context.Event.Remove(ev);
                context.SaveChanges();

                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public bool AddNewEvent(int planID, int eventTypeID, DateTime dt)
        {
            bool success;
            try
            {
                EventType et = context.EventType.Find(eventTypeID);

                Event evnt = new Event()
                {
                    EventDate = dt,
                    EventType = et,
                    PlanID = planID
                };

                context.Event.Add(evnt);
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
    }
}
