using System;
using System.Collections.Generic;
using System.Text;

namespace BaggageSys
{
    class Passenger
    {
        public Baggage[] baggages { get; }
        public int passengerNumber { get; }
        public Destination destination { get; }
        public Passenger(Baggage[] baggages, int passengerNumber, Destination destination)
        {
            this.baggages = baggages;
            this.passengerNumber = passengerNumber;
            this.destination = destination;
        }
    }
}
