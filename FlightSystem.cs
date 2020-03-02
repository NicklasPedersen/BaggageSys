using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BaggageSys
{
    // A class to hold events that can be taken by gui
    class FlightSystem
    {
        public List<string> events = new List<string>();
        public EventWaitHandle eventHappened = new EventWaitHandle(false, EventResetMode.AutoReset);
        public void LandPlane(Flight f, Terminal t)
        {
            lock (events)
            {
                t.Land(f);
                events.Add("Flight number " + f.Id + " has arrived, leaving to " + f.Destination + " at " + f.Departure);
                eventHappened.Set();
            }
        }
        public void DepartPlane(Flight f, Terminal t)
        {
            lock (events)
            {
                events.Add("Flight number " + f.Id + " departing from terminal number " + t.Id);
                eventHappened.Set();
            }
        }
    }
}
