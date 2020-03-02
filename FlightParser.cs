using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BaggageSys
{
    // Extremely simple json parser, only works with one specific instance
    static class FlightParser
    {
        // Writes the json to a file
        // It looks like this:
        // {
        //     "Flights":
        //         [
        //             {
        //                 "id":"$id0",
        //                 "destination":"$destination0",
        //                 "departure":"$departure0"
        //             },
        //             {
        //                 "id":"$id1",
        //                 "destination":"$destination1",
        //                 "departure":"$departure1"
        //             }
        //         ]
        // }
        // You can also see an example in flights.json
        public static void WriteFlights(Flight[] flights, string file)
        {
            StringBuilder b = new StringBuilder();
            b.Append("{\n");
            b.Append("    \"flights\":\n");
            b.Append("        [\n");
            for (int i = 0; i < flights.Length; i++)
            {
                b.Append("            {\n");
                b.Append("                \"id\":" + "\"" + flights[i].Id + "\",\n");
                b.Append("                \"destination\":" + "\"" + flights[i].Destination.ToString() + "\",\n");
                b.Append("                \"departure\":" + "\"" + flights[i].Departure.ToString() + "\"\n");
                b.Append("            }");
                if (i != flights.Length - 1)
                {
                    b.Append(',');
                }
                b.Append('\n');
            }
            b.Append("        ]\n");
            b.Append("}");
            File.WriteAllText(file, b.ToString());
        }
        static int NextCharacter(string s, int n)
        {
            n++;
            while (s.Length > n && char.IsWhiteSpace(s[n])) n++;
            return n;
        }
        static int FirstCharacterLocation(string s)
        {
            int n = 0;
            while (s.Length > n && char.IsWhiteSpace(s[n])) n++;
            return n;
        }
        static int EndOfName(string s, int n)
        {
            n++;
            while (s.Length > n && s[n] != '"') n++;
            return n;
        }

        public static Flight[] GetFlights(string file)
        {
            string s = File.ReadAllText(file);
            int currentChar = FirstCharacterLocation(s); // '{'
            currentChar = NextCharacter(s, currentChar); // '"'
            currentChar = EndOfName(s, currentChar);     // "Flights"
            currentChar = NextCharacter(s, currentChar); // ':'
            currentChar = NextCharacter(s, currentChar); // '['
            List<Flight> flights = new List<Flight>();
            do
            {
                currentChar = NextCharacter(s, currentChar); // '{'
                currentChar = NextCharacter(s, currentChar); // '"'
                currentChar = EndOfName(s, currentChar); // '"'
                currentChar = NextCharacter(s, currentChar); // ':'
                currentChar = NextCharacter(s, currentChar); // '"'
                int start = currentChar;
                currentChar = EndOfName(s, currentChar); // '"'
                string id = s.Substring(start + 1, currentChar - start - 1);
                currentChar = NextCharacter(s, currentChar); // '}'
                currentChar = NextCharacter(s, currentChar); // ','

                currentChar = NextCharacter(s, currentChar); // '{'
                currentChar = NextCharacter(s, currentChar); // '"'
                currentChar = EndOfName(s, currentChar); // '"'
                currentChar = NextCharacter(s, currentChar); // ':'
                currentChar = NextCharacter(s, currentChar); // '"'
                start = currentChar;
                currentChar = EndOfName(s, currentChar); // '"'
                string destination = s.Substring(start + 1, currentChar - start - 1);
                currentChar = NextCharacter(s, currentChar); // '}'
                currentChar = NextCharacter(s, currentChar); // ','

                currentChar = NextCharacter(s, currentChar); // '{'
                currentChar = NextCharacter(s, currentChar); // '"'
                currentChar = EndOfName(s, currentChar); // '"'
                currentChar = NextCharacter(s, currentChar); // ':'
                currentChar = NextCharacter(s, currentChar); // '"'
                start = currentChar;
                currentChar = EndOfName(s, currentChar); // '"'
                string departure = s.Substring(start + 1, currentChar - start - 1);
                currentChar = NextCharacter(s, currentChar); // '}'

                flights.Add(new Flight(int.Parse(id), (Destination)Enum.Parse(typeof(Destination), destination, true), DateTime.Parse(departure)));
                currentChar = NextCharacter(s, currentChar); // ',' or ']'
            } while (s.Length > currentChar && s[currentChar] == ',');
            return flights.ToArray();
        }
    }
}
 