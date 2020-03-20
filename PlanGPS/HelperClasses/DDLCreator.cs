using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModelLayer;
using DataAccessLayer;
using PlanGPS.HelperClasses;

namespace PlanGPS.HelperClasses
{
    public class DDLCreator
    {
        DAL dal = new DAL();

        public SelectList NeuroSelectListWithNA(List<Neurosurgeon> nList)
        {
            List<ReducedPropertyPerson> rppList = new List<ReducedPropertyPerson>();
            
            for (int i = 0; i < nList.Count()+1; i++)
            {
                if (i == 0)
                {
                    ReducedPropertyPerson rpp0 = new ReducedPropertyPerson()
                    {
                        ID = -1,
                        Name = "No Neurosurgeon Used"
                    };

                    rppList.Add(rpp0);
                    continue;
                }

                ReducedPropertyPerson rpp = new ReducedPropertyPerson()
                {
                    ID = nList[i-1].ID,
                    Name = nList[i - 1].LastFirstName
                };

                rppList.Add(rpp);
            }

            SelectList nsl = new SelectList(rppList, "ID", "Name");

            return new SelectList(rppList, "ID", "Name");
        }

    }
}