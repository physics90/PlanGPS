using DataAccessLayer;
using DataModelLayer;
using Newtonsoft.Json;
using PlanGPS.PlotModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PlanGPS.Controllers
{
    public class HomeDataController : Controller
    {
        public string GetPlansPerDoc()
        {
            DAL dal = new DAL();

            List<TypePlotModel> planList = new List<TypePlotModel>();
            List<RadOnc> radOncs = dal.GetAllRadOncs();

            foreach (RadOnc ro in radOncs)
            {
                TypePlotModel tpm = new TypePlotModel()
                {
                    DPName = ro.LastName,
                    DPValue = ro.Plans.Count(),
                    DPColor = ro.DesiredColor
                };

                planList.Add(tpm);
            }

            string data = JsonConvert.SerializeObject(planList);

            return data;
        }

        public string GetPlansPerPhysicist()
        {
            DAL dal = new DAL();

            List<TypePlotModel> planList = new List<TypePlotModel>();
            List<Physicist> physics = dal.GetAllPhysicists();

            foreach (Physicist ph in physics)
            {
                TypePlotModel tpm = new TypePlotModel()
                {
                    DPName = ph.LastName,
                    DPValue = ph.Plans.Count(),
                    DPColor = ph.DesiredColor
                };

                planList.Add(tpm);
            }

            string data = JsonConvert.SerializeObject(planList);

            return data;
        }

        public string GetPlansPerTypes()
        {
            DAL dal = new DAL();

            List<TypePlotModel> planList = new List<TypePlotModel>();
            List<PlanType> planTypes = dal.GetAllPlanTypes();
            List<Plan> plans = dal.GetAllPlans();

            foreach (PlanType type in planTypes)
            {
                int typeCount = (from p in plans
                                 where p.PlanType.ID == type.ID
                                 select p).Count();

                TypePlotModel tpm = new TypePlotModel()
                {
                    DPName = type.Type,
                    DPValue = typeCount,
                    DPColor = type.DesiredColor
                };

                planList.Add(tpm);
            }

            string data = JsonConvert.SerializeObject(planList);

            return data;
        }

    }
}