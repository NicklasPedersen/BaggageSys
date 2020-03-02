using System;
using System.Collections.Generic;
using System.Text;

namespace BaggageSys
{
    class CentralServer
    {
        List<Flight> flights = new List<Flight>();
        public bool ContainsDestination(Destination d)
        {
            return flights.Find(f => f.Destination == d) != null;
        }
    }
}
