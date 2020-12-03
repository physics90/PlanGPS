using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer
{
    public partial class PlanningStats
    {
        public int ID { get; set; }
        public int ImagingDays { get; set; }
        public int ImportingDays { get; set; }
        public int ContouringDays { get; set; }
        public int PlanningDays { get; set; }
        public int PlanReviewDays { get; set; }
        public int PlanFinalizationDays { get; set; }
        public int PlanSecondCheckDays { get; set; }
        public int SchedulingDays { get; set; }
        public int InsuranceApprovalDays { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
