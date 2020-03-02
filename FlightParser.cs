using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BaggageSys
{
    static class FlightParser
    {
        public static void WriteFlights(Flight[] flights, string file)
        {
            StringBuilder b = new StringBuilder();
            b.Append("{\n    \"flights\":\n        [\n");
            for (int i = 0; i < flights.Length; i++)
            {
                b.Append("            {\n");
                b.Append("                \"id\":" + "\"" + flights[i].Id + "\",\n");
                b.Append("                \"destination\":" + "\"" + flights[i].Destination.ToString() + "\",\n");
                b.Append("                \"departure\":" + "\"" + flights[i].Departure.ToString() + "\"\n");
                b.Append("            },\n");
            }
            b.Append("        ]\n}");
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
            // Debug.Assert(s[n] == '"');
            n++;
            while (s.Length > n && s[n] != '"') n++;
            return n;
        }

        public static Flight[] GetFlights(string file)
        {
            string s = File.ReadAllText(file);
            int currentChar = FirstCharacterLocation(s);
            currentChar = NextCharacter(s, currentChar);
            currentChar = EndOfName(s, currentChar);
            currentChar = NextCharacter(s, currentChar);
            currentChar = NextCharacter(s, currentChar);
            List<Flight> flights = new List<Flight>();
            currentChar = NextCharacter(s, currentChar); // '{'
            while (s.Length > currentChar && s[currentChar] != ']')
            {
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
                currentChar = NextCharacter(s, currentChar); // ','

                flights.Add(new Flight(int.Parse(id), (Destination)Enum.Parse(typeof(Destination), destination, true), DateTime.Parse(departure)));
                currentChar = NextCharacter(s, currentChar); // '{' or ']'
            }
            return flights.ToArray();
        }
    }
}
 