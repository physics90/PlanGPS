using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataModelLayer;

namespace DbInitialization
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //Use this to initialize and populate the PlanGPS database
            LoadDefaultData ldd = new LoadDefaultData();
            ldd.LoadEventTypes();
        }

        


    }
}
