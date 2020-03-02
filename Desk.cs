using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BaggageSys
{
    // The desk is where passengers check in their luggage
    class Desk
    {
        private Buffer<Baggage> baggageBuffer { get; }
        private Buffer<Passenger> passengerQueue { get; } = new Buffer<Passenger>(10);
        public int Id { get; }

        public Desk(Buffer<Baggage> baggageBuffer, int id)
        {
            Id = id;
            this.baggageBuffer = baggageBuffer;
        }
        public void CheckIn(Passenger p)
        {
            passengerQueue.TryPutItem(p);
        }
        public void Run()
        {
            while (true)
            {
                Passenger passenger = passengerQueue.TryGetItem();
                Console.WriteLine("passenger " + passenger.passengerNumber + " checked in desk " + Id);
                foreach(Baggage b in passenger.baggages)
                {
                    baggageBuffer.TryPutItem(b);
                }
            }
        }
    }
}
