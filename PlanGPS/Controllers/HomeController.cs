using PlanGPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlanGPS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DashboardViewModel dvm = new DashboardViewModel();
            
            return View(dvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _CurrentPlansTable()
        {
            _CurrentPlansTableViewModel cpVM = new _CurrentPlansTableViewModel();

            return PartialView(cpVM);
        }

        public ActionResult _CKPlanTypeDistributionPlot()
        {
            _CKPlanTypeDistributionViewModel ckPlot = new _CKPlanTypeDistributionViewModel();

            return PartialView(ckPlot);
        }

        public ActionResult _CKDocDistributionPlot()
        {
            _CKDocDistributionViewModel ckPlot = new _CKDocDistributionViewModel();

            return PartialView(ckPlot);
        }

        public ActionResult _CKPhysicsDistributionPlot()
        {
            //_CKPhysicsDistributionPlot ckPlot = new _CKPhysicsDistributionPlot();

            return PartialView();
        }

        //public ActionResult _EventsTable(int planID)
        //{
        //    //Gets all events for plan
        //    _EventTableViewModel evm = new _EventTableViewModel(planID);

        //    ViewBag.EventWarning = "";
        //    if (planID != -1)
        //    {
        //        ViewBag.EventWarning = evm.WarningMessage;
        //    }

        //    return PartialView(evm);
        //}
    }
}