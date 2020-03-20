using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer
{
    public partial class Patient
    {
        public Patient()
        {
            
        }

        public Patient(int patientID)
        {
            this.HasPlanInPlanning = this.Plans != null ? this.Plans.Any(p => p.IsInPlanning == true) : false;
        }

        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string MRN { get; set; }

        [Display(Name = "Is In Planning")]
        public bool HasPlanInPlanning { get; set; } = false;

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
