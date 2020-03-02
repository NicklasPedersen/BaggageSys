using System;
using System.Collections.Generic;
using System.Text;

namespace BaggageSys
{
    class Flight
    {
        public int Id { get; }
        public Destination Destination { get; }
        public DateTime Departure { get; }
        public Flight(int id, Destination destination, DateTime departure)
        {
            Id = id;
            Destination = destination;
            Departure = departure;
        }
    }
}
