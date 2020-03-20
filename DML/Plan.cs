using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModelLayer
{
    public partial class Plan
    {
        public int ID { get; set; }

        [Display(Name = "Plan Name")]
        public string PlanName { get; set; }

        [Display(Name = "Is Being Planned?")]
        public bool IsInPlanning { get; set; }

        [Display(Name = "Has PreApproval?")]
        public bool HasPreApproval { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual RadOnc RadOnc { get; set; }
        public virtual Neurosurgeon Neuro { get; set; }
        public virtual Physicist Physicist { get; set; }
        public virtual PlanType PlanType { get; set; }
        public virtual ICollection<Event> Events { get; set; }

    }
}