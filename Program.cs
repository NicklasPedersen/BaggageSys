using System;
using System.Threading;
using System.Collections.Generic;

namespace BaggageSys
{
    class Program
    {
        static void Main(string[] args)
        {
            Flight[] flights = {
                new Flight(0, Destination.COPENHAGEN, DateTime.Now),
                new Flight(1, Destination.LONDON, DateTime.Now),
                new Flight(2, Destination.PARIS, DateTime.Now),
            };
            string file = "../../../flights.json";
            FlightParser.WriteFlights(flights, file);
            FlightParser.GetFlights(file);
            Buffer<Baggage> deskBuffer = new Buffer<Baggage>(10), terminalBuffer = new Buffer<Baggage>(10);
            Desk[] desks = new Desk[5];
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < desks.Length; i++)
            {
                desks[i] = new Desk(deskBuffer, i);
            }
            Sorting s = new Sorting(deskBuffer, terminalBuffer);
            Terminal[] terminals = new Terminal[5];
            for (int i = 0; i < terminals.Length; i++)
            {
                terminals[i] = new Terminal(terminalBuffer, i);
            }
            foreach (Desk desk in desks)
            {
                Thread t = new Thread(desk.Run);
                threads.Add(t);
                t.Start();
            }
            foreach(Terminal terminal in terminals)
            {
                Thread t = new Thread(terminal.Run);
                threads.Add(t);
                t.Start();
            }
            Thread thread = new Thread(s.Sort);
            thread.Start();
            threads.Add(thread);
            Random r = new Random();
            while (true)
            {
                Passenger p = PassengerFactory.CreatePassenger();
                desks[r.Next(desks.Length)].CheckIn(p);
            }
        }
    }
}
