using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModelLayer
{
    public partial class Neurosurgeon : TeamMember
    {
        public Neurosurgeon()
        {
            IsPhysician = true;
        }
    }
}
