using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BaggageSys
{
    // The terminal takes the baggages and flies away, the passengers have yet to be implemented
    class Terminal
    {
        private Buffer<Baggage> buf;
        private Buffer<Baggage> internalBuffer { get; } = new Buffer<Baggage>(9999);
        public int Id { get; }
        private Flight f;
        public Terminal(Buffer<Baggage> baggages, int id)
        {
            buf = baggages;
            Id = id;
        }
        public void Run(FlightSystem s)
        {
            while (true)
            {
                if (!(f is null))
                {
                    // Time to depart?
                    if (f.Departure < DateTime.Now)
                    {
                        lock(buf) // Acquire the buffer so no one else can lock it
                        {
                            while (!buf.IsEmpty())
                            {
                                f.baggages.TryPutItem(buf.TryGetItem());
                            }
                            s.DepartPlane(f, this);
                            f = null;
                        }
                    }
                }
                internalBuffer.TryPutItem(buf.TryGetItem());
                Console.WriteLine("terminal " + Id + " got baggage");
                Thread.Sleep(1000);
            }
        }
        public void Land(Flight f)
        {
            this.f = f;
        }
    }
}
