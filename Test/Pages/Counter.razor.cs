using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Pages
{
    public partial class Counter
    {
        private int currentCount = 1;
        private string fontFamily = "Verdana";

        private void IncrementCount()
        {
            currentCount = currentCount * 2;
        }

}
}
