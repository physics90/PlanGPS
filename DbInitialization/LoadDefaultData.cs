using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataModelLayer;

namespace DbInitialization
{
    public class LoadDefaultData
    {
        private DAL dal = new DAL();

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

            dal.CreateDbDefaultData(etList);
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

            Physicist ph2 = new Physicist()
            {
                FirstName = "Chris",
                LastName = "Westbrook",
                IsPhysician = false
            };


        }

        private void LoadRadOncs()
        {

        }

        private void LoadNeuros()
        {

        }

        private void LoadPlanTypes()
        {

        }


    }
}
