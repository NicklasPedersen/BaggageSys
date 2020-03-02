using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BaggageSys
{
    class Terminal
    {
        Buffer<Baggage> buf;
        int id;
        public Terminal(Buffer<Baggage> baggages, int id)
        {
            buf = baggages;
            this.id = id;
        }
        public void Run()
        {
            while (true)
            {
                Baggage b = buf.TryGetItem();
                Console.WriteLine("terminal " + id + " got baggage");
                Thread.Sleep(1000);
            }
        }
    }
}
