using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataAccessLayer;
using DataModelLayer;
using Newtonsoft.Json;
using PlanGPS.PlotModels;
//using Highsoft.Web.Mvc.Charts;

namespace PlanGPS.Models
{
    public class _CKDocDistributionViewModel
    {
        DAL dal = new DAL();
        public List<int> PlotData { get; set; }

        public _CKDocDistributionViewModel()
        {
            CalcData();
        }

        public void CalcData()
        {
            PlotData = new List<int>();
            List<RadOnc> radOncList = dal.GetAllRadOncs();
            var i = 3;
            foreach (RadOnc ro in radOncList)
            {
                
                CKPlanTypeDistributionPlotModel dp = new CKPlanTypeDistributionPlotModel()
                {
                    //Name = ro.LastName,
                    DPValue = ro.Plans.Count()
                };

                PlotData.Add(ro.Plans.Count()+i);
                i *= 2;
            }
        }

        public string GetJasonData()
        {
            string datapoints = JsonConvert.SerializeObject(PlotData);
            return datapoints;
        }
    }
}