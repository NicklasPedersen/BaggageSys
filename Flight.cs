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
        public Buffer<Baggage> baggages = new Buffer<Baggage>(999);
        public Buffer<Passenger> passengers = new Buffer<Passenger>(999);
        public Flight(int id, Destination destination, DateTime departure)
        {
            Id = id;
            Destination = destination;
            Departure = departure;
        }
    }
}
