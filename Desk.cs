using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BaggageSys
{
    class Desk
    {
        Buffer<Baggage> baggageBuffer;
        Buffer<Passenger> passengerQueue;
        int id;

        public Desk(Buffer<Baggage> baggageBuffer, int id)
        {
            this.id = id;
            this.baggageBuffer = baggageBuffer;
            passengerQueue = new Buffer<Passenger>(10);
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
                Console.WriteLine("passenger " + passenger.passengerNumber + " checked in desk " + id);
                foreach(Baggage b in passenger.baggages)
                {
                    baggageBuffer.TryPutItem(b);
                }
            }
        }
    }
}
