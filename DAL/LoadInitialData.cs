using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModelLayer;

namespace DataAccessLayer
{
    public class LoadInitialData
    {
        private PlanGPSContext ctx;
        public LoadInitialData(PlanGPSContext context)
        {
            ctx = context;
        }

        public void LoadEventDurationCategories()
        {
            List<EventDurationCategory> edList = new List<EventDurationCategory>();
            EventDurationCategory edc = new EventDurationCategory()
            {
                Name = "Scheduling",
                DataID = 1
            };
            edList.Add(edc);

            edc = new EventDurationCategory()
            {
                Name = "Therapist",
                DataID = 2
            };
            edList.Add(edc);

            edc = new EventDurationCategory()
            {
                Name = "Physics",
                DataID = 3
            };
            edList.Add(edc);

            edc = new EventDurationCategory()
            {
                Name = "Rad Onc",
                DataID = 4
            };
            edList.Add(edc);

            edc = new EventDurationCategory()
            {
                Name = "Insurance",
                DataID = 5
            };
            edList.Add(edc);

            ctx.EventDurationCategory.AddRange(edList);
            ctx.SaveChanges();
        }

        public void LoadEventTypes()
        {
            //Set eventdurcat objects first to add to event types
            EventDurationCategory Scheduling = (from ev in ctx.EventDurationCategory
                                                where ev.Name == "Scheduling"
                                                select ev).First();
            EventDurationCategory Therapist = (from ev in ctx.EventDurationCategory
                                               where ev.Name == "Therapist"
                                               select ev).First();
            EventDurationCategory Physics = (from ev in ctx.EventDurationCategory
                                             where ev.Name == "Physics"
                                             select ev).First();
            EventDurationCategory RadOnc = (from ev in ctx.EventDurationCategory
                                            where ev.Name == "Rad Onc"
                                            select ev).First();
            EventDurationCategory Insurance = (from ev in ctx.EventDurationCategory
                                               where ev.Name == "Insurance"
                                               select ev).First();

            //***************************************************************************************
            //***************************************************************************************
            //If the StageID of any object below changes, update ComputeStats method in HelperClasses
            //***************************************************************************************
            //***************************************************************************************
            List<EventType> etList = new List<EventType>();
            EventType et;

            et = new EventType()
            {
                Name = "Insurance Approval",
                Description = "Date of Insurance Approval/Pre-Approval",
                IsAStage = true,
                StageID = -1,
            };
            etList.Add(et);
            
            et = new EventType()
            {

                Name = "Waiting on Planning CT",
                Description = "Waiting on planning CT",
                IsAStage = true,
                StageID = 0,
                CurrentStageName = "Waiting for Planning CT",
                EventDurationCategory = Scheduling,
                AllowMultiple = false
            };
            etList.Add(et);
            
            et = new EventType()
            {
                Name = "Planning CT Performed",
                Description = "Planning CT performed and process started",
                IsAStage = true,
                StageID = 1,
                CurrentStageName = "Waiting for Planning CT",
                EventDurationCategory = Scheduling,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Imaging Completed",
                Description = "All imaging has been completed",
                IsAStage = true,
                StageID = 2,
                CurrentStageName = "Imaging",
                EventDurationCategory = Scheduling,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "LOT Sim Completed",
                Description = "LOT Simulation completed",
                IsAStage = true,
                StageID = 3,
                CurrentStageName = "LOT Sim",
                EventDurationCategory = Scheduling,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Image Import and Fusion Completed",
                Description = "Importing of all images for planning has been completed",
                IsAStage = true,
                StageID = 4,
                CurrentStageName = "Image Import",
                EventDurationCategory = Physics,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Contouring Completed",
                Description = "Physician contouring started",
                IsAStage = true,
                StageID = 5,
                CurrentStageName = "Contouring",
                EventDurationCategory = RadOnc,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Ready for Review",
                Description = "Plan is ready for physician review",
                IsAStage = true,
                StageID = 6,
                CurrentStageName = "Planning",
                EventDurationCategory = Physics
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Reviewed, Revision Requested",
                Description = "Physician reviewed plan",
                IsAStage = true,
                StageID = 7,
                CurrentStageName = "Plan Review",
                EventDurationCategory = RadOnc
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Approved",
                Description = "Physician approved plan",
                IsAStage = true,
                StageID = 8,
                CurrentStageName = "Plan Review",
                EventDurationCategory = RadOnc,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Planning Finalized",
                Description = "Planning finialized for second check.",
                IsAStage = true,
                StageID = 9,
                CurrentStageName = "Plan Finalization",
                EventDurationCategory = Physics,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Physics Second Check Done",
                Description = "Plan has been second checked and ready for scheduling",
                IsAStage = true,
                StageID = 10,
                CurrentStageName = "Physics Second Check",
                EventDurationCategory = Physics,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Plan Scheduled",
                Description = "Patient has been scheduled for treatment",
                IsAStage = true,
                StageID = 11,
                CurrentStageName = "Patient Scheduling",
                EventDurationCategory = Therapist,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Treatment Started",
                Description = "Treatment has  started",
                IsAStage = true,
                StageID = 12,
                CurrentStageName = "Treatment Scheduled",
                EventDurationCategory = Scheduling,
                AllowMultiple = false
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Outside CT",
                Description = "Additional CT",
                IsAStage = false,
                StageID = 1,
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "MRI Imaging",
                Description = "MRI Date",
                IsAStage = false,
                StageID = 1,
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "PET Imaging",
                Description = "PET Date",
                IsAStage = false,
                StageID = 1,
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Fiducial Placement",
                Description = "Fiducial placement",
                IsAStage = false,
                StageID = 1,
            };
            etList.Add(et);

            et = new EventType()
            {
                Name = "Reimaging",
                Description = "New/Additional imaging required",
                IsAStage = true,
                StageID = 1,
            };
            etList.Add(et);

            ctx.EventType.AddRange(etList);
            ctx.SaveChanges();
        }

        public void LoadPhysicists()
        {
            Physicist ph = new Physicist()
            {
                FirstName = "Jeff",
                LastName = "Garrett",
                IsPhysician = false,
                DesiredColor = "rgba(0,140,255,0.35)"
            };

            ctx.Physicist.Add(ph);
            ctx.SaveChanges();

            Physicist ph2 = new Physicist()
            {
                FirstName = "Chris",
                LastName = "Westbrook",
                IsPhysician = false,
                DesiredColor = "rgba(255,0,0,0.52)"
            };

            ctx.Physicist.Add(ph2);
            ctx.SaveChanges();
        }

        public void LoadRadOncs()
        {
            List<RadOnc> roList = new List<RadOnc>();
            RadOnc ro = new RadOnc()
            {
                FirstName = "Jeanann",
                LastName = "Suggs",
                IsPhysician = true,
                DesiredColor = "rgba(112,0,255,0.36)"
            };

            roList.Add(ro);

            ro = new RadOnc()
            {
                FirstName = "Richard",
                LastName = "Friedman",
                IsPhysician = true,
                DesiredColor = "rgba(0,254,58,0.35)"
            };

            roList.Add(ro);

            ro = new RadOnc()
            {
                FirstName = "Margaret",
                LastName = "Wadsworth",
                IsPhysician = true,
                DesiredColor = "rgba(255,0,0,0.52)"
            };

            roList.Add(ro);

            ro = new RadOnc()
            {
                FirstName = "Eric",
                LastName = "Balfour",
                IsPhysician = true,
                DesiredColor = "rgba(255,255,0,0.46)"
            };

            roList.Add(ro);

            ctx.RadOnc.AddRange(roList);
            ctx.SaveChanges();

        }

        public void LoadNeuros()
        {
            Neurosurgeon ns = new Neurosurgeon()
            {
                FirstName = "Matthew",
                LastName = "Vanlandingham",
                IsPhysician = true
            };

            ctx.Neurosurgeon.Add(ns);
            ctx.SaveChanges();
        }

        public void LoadPlanTypes()
        {
            List<PlanType> ptList = new List<PlanType>();

            PlanType pt = new PlanType()
            {
                Type = "Intracranial",
                Description = "Brain (Intracranial) with 6DOF Skull tracking",
                IsLOT = false,
                DesiredColor = "rgba(255,255,0,0.46)"
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Body, Spine Tracking",
                Description = "Body with tracking on spine",
                IsLOT = false,
                DesiredColor = "rgba(255,0,0,0.52)"
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Body, Fiducial",
                Description = "Body using fiducials for motion tracking without Synchrony",
                IsLOT = false,
                DesiredColor = "rgba(255,142,0,0.36)"
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Body, Synchrony",
                Description = "Body using fiducials with Synchrony",
                IsLOT = false,
                DesiredColor = "rgba(112,0,255,0.36)"
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Lung LOT",
                Description = "Lung using LOT Planning",
                IsLOT = true,
                DesiredColor = "rgba(0,254,58,0.34)"
            };

            ptList.Add(pt);

            pt = new PlanType()
            {
                Type = "Prostate",
                Description = "Prostate SRT",
                IsLOT = true,
                DesiredColor = "rgba(0,140,255,0.34)"
            };

            ptList.Add(pt);
            ctx.PlanType.AddRange(ptList);
            ctx.SaveChanges();

        }
    }
}
