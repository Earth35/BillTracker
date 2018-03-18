using System;

namespace TrackerLogic
{
    public static class Clock
    {
        static private DateTime _currentDateTime = DateTime.Now;

        public static string GetCurrentDateAndTime()
        {
            return _currentDateTime.ToString("dd-MM-yyyy HH:mm");
        }

        public static string GetCurrentDate()
        {
            return _currentDateTime.ToShortDateString();
        }
    }
}
