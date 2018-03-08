using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLogic
{
    public static class Clock
    {
       public static string GetCurrentDateAndTime()
        {
            return DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }
    }
}
