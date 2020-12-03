using DataAccessLayer;
using DataModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanGPS.HelperClasses
{
    public class ComputeStats
    {
        //called by PlanController.SaveEvent
        Plan Plan;
        PlanningStats stats;
        DAL dal = new DAL();

        public ComputeStats(int planID)
        {
            Plan = dal.GetPlanWithPlanID(planID);
        }

        public bool Compute()
        {
            bool statsExist = dal.DoStatsExistForPlanWithPlanID(Plan.ID);
            bool success;

            stats = statsExist ? dal.GetPlanningStatsWithPlanID(Plan.ID) : new PlanningStats();

            List<Event> eventList = Plan.Events.ToList();

            //The start date for timing is the planning ct date
            try
            {
                DateTime planningCTDate = eventList.Where(el => el.EventType.StageID == 1).Select(ev => ev.EventDate).First();
                DateTime imagingCompletedDate = eventList.Where(el => el.EventType.StageID == 2).Select(ev => ev.EventDate).FirstOrDefault();
                DateTime imagingImportedDate = eventList.Where(el => el.EventType.StageID == 4).Select(ev => ev.EventDate).First();
                DateTime contouringCompletedDate = eventList.Where(el => el.EventType.StageID == 5).Select(ev => ev.EventDate).First();
                List<DateTime> planningDates = eventList.Where(el => el.EventType.StageID == 6).Select(ev => ev.EventDate).ToList(); //Plan is ready for review
                List<DateTime> reviewDates = eventList.Where(el => el.EventType.StageID == 7).Select(ev => ev.EventDate).ToList(); //Plan reviewed but needs revising
                DateTime planApprovalDate = eventList.Where(el => el.EventType.StageID == 8).Select(ev => ev.EventDate).First();
                DateTime planFinalizedDate = eventList.Where(el => el.EventType.StageID == 8).Select(ev => ev.EventDate).First();
                DateTime planSecondCheckedDate = eventList.Where(el => el.EventType.StageID == 9).Select(ev => ev.EventDate).First();
                DateTime planScheduledDate = eventList.Where(el => el.EventType.StageID == 10).Select(ev => ev.EventDate).First();
                DateTime txStartedDate = eventList.Where(el => el.EventType.StageID == 11).Select(ev => ev.EventDate).First();
                DateTime insuranceApprovalDate = eventList.Where(el => el.EventType.StageID == -1).Select(ev => ev.EventDate).First();

                //Perform check of dates

                if (Plan.PlanType.IsLOT)
                {
                    DateTime lotSIMDate = eventList.Where(el => el.EventType.StageID == 3).Select(ev => ev.EventDate).First();
                    stats.ImportingDays = imagingImportedDate.Subtract(lotSIMDate).Days;
                    stats.SchedulingDays = CalcSchedulingDaysWithLOT(planningCTDate, imagingCompletedDate, lotSIMDate, imagingImportedDate, planScheduledDate, txStartedDate);
                }
                else
                {
                    stats.ImportingDays = imagingImportedDate.Subtract(imagingCompletedDate).Days;
                    stats.SchedulingDays = CalcSchedulingDaysNoLOT(planningCTDate, imagingCompletedDate, imagingImportedDate, planScheduledDate, txStartedDate);
                }

                stats.ImagingDays = imagingCompletedDate.Subtract(planningCTDate).Days;                
                stats.ContouringDays = contouringCompletedDate.Subtract(imagingImportedDate).Days;
                stats.PlanningDays = CalcPlanningDays(contouringCompletedDate, planningDates, reviewDates);
                stats.PlanReviewDays = CalcReviewDays(planningDates, reviewDates, planApprovalDate);
                stats.PlanFinalizationDays = planFinalizedDate.Subtract(reviewDates.Last()).Days;
                stats.PlanSecondCheckDays = planSecondCheckedDate.Subtract(planFinalizedDate).Days;
                stats.SchedulingDays = planScheduledDate.Subtract(planSecondCheckedDate).Days;

                //bool dateError = CheckDates(stats);
                
                stats.InsuranceApprovalDays = insuranceApprovalDate > planFinalizedDate ? insuranceApprovalDate.Subtract(planFinalizedDate).Days : 0;
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            if (success)
            { 
                success = dal.SaveStats(stats);                
            }

            return success;
        }

        private int CalcSchedulingDaysWithLOT(DateTime planningCTDate, DateTime imagingCompletedDate, DateTime lotSIMDate, DateTime imagingImportedDate, DateTime planScheduledDate, DateTime txStartedDate)
        {
            int totalDays = 0;

            totalDays += imagingCompletedDate.Subtract(planningCTDate).Days + 
                txStartedDate.Subtract(planScheduledDate).Days + 
                lotSIMDate.Subtract(imagingCompletedDate).Days + 
                imagingImportedDate.Subtract(lotSIMDate).Days;

            return totalDays;
        }

        private int CalcSchedulingDaysNoLOT(DateTime planningCTDate, DateTime imagingCompletedDate, DateTime imagingImportedDate, DateTime planScheduledDate, DateTime txStartedDate)
        {
            int totalDays = 0;

            totalDays += imagingCompletedDate.Subtract(planningCTDate).Days + 
                txStartedDate.Subtract(planScheduledDate).Days + 
                imagingImportedDate.Subtract(imagingCompletedDate).Days;

            return totalDays;
        }

        private int CalcReviewDays(List<DateTime> planningDates, List<DateTime> reviewDates, DateTime approvalDate)
        {
            int totalDays = 0;
            if (planningDates.Count() == 1)
            {
                totalDays += approvalDate.Subtract(planningDates[0]).Days;
            }
            else
            {
                for (int i = 0; i < reviewDates.Count(); i++)
                {
                    totalDays += reviewDates[i].Subtract(planningDates[i]).Days;
                }

                totalDays += approvalDate.Subtract(planningDates.Last()).Days;
            }

            return totalDays;
        }

        private int CalcPlanningDays(DateTime contouringCompletedDate, List<DateTime> planningDates, List<DateTime> reviewDates)
        {
            int totalDays = 0;

            totalDays += planningDates[0].Subtract(contouringCompletedDate).Days;

            if (planningDates.Count() > 1)
            {
                for (int i = 1; i < reviewDates.Count(); i++)
                {
                    totalDays += planningDates[i].Subtract(reviewDates[i - 1]).Days;
                }
            }

            return totalDays;
        }
    }
}