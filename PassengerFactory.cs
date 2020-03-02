using System;
namespace BaggageSys
{
    static class PassengerFactory
    {
        public static Random rnd = new Random();
        public static int currentPassenger = 0;
        public static Passenger CreatePassenger()
        {
            int baggageAmount = rnd.Next(4);
            Baggage[] baggages = new Baggage[baggageAmount];
            for (int i = 0; i < baggages.Length; i++)
            {
                baggages[i] = new Baggage(currentPassenger);
            }
            int passengerNumber = currentPassenger++;
            Destination destination = (Destination)rnd.Next((int)Destination.MAX_DESTINATIONS);
            return new Passenger(baggages, passengerNumber, destination);
        }
    }
}
