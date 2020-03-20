using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer
{
    public class TeamMember
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsPhysician { get; set; }
        public string LastFirstName { get { return this.LastName + ", " + this.FirstName; } }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}
