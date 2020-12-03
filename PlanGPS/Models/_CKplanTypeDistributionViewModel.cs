using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataModelLayer;
using Highsoft.Web.Mvc.Charts;

namespace PlanGPS.Models
{
    public class _CKPlanTypeDistributionViewModel
    {
        DAL dal = new DAL();
        public List<PieSeriesData> PlotData { get; set; }

        public _CKPlanTypeDistributionViewModel()
        {
            CalcData();
        }

        public void CalcData()
        {
            PlotData = new List<PieSeriesData>();
            List<PlanType> planTypeList = dal.GetAllPlanTypes();

            foreach (PlanType pt in planTypeList)
            {
                var dataPoint = dal.CountNumberOfPlansForType(pt.ID);

                PieSeriesData pieSeriesDataPoint = new PieSeriesData() 
                { 
                    Name = pt.Type, 
                    Y = dataPoint 
                };

                PlotData.Add(pieSeriesDataPoint);
            }
        }

        
        
        
    }
}