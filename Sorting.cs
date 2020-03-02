using System;
using System.Collections.Generic;
using System.Text;

namespace BaggageSys
{
    class Sorting
    {
        private Buffer<Baggage> input, output;
        public Sorting(Buffer<Baggage> input, Buffer<Baggage> output)
        {
            this.input = input;
            this.output = output;
        }

        public void Sort()
        {
            while (true)
            {
                Baggage baggageToSort = input.TryGetItem();
                output.TryPutItem(baggageToSort);
            }
        }
    }
}
