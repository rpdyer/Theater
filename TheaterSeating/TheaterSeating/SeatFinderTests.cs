using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace TheaterSeating
{
    [TestFixture]
    public class SeatFinderTests
    {
        [TestCase("", 1, Result = "A5")]
        [TestCase("", 2, Result = "A5,A6")]
        [TestCase("A5,A6", 2, Result = "A3,A4")]
        [TestCase("A5,A6", 3, Result = "B5,B6,B7")]
        [TestCase("A1,A2,A3,A4,A5,A6,A7,A8,A9,A10," +
                  "B1,B2,B3,B4,B5,B6,B7,B8,B9,B10," +
                  "C1,C2,C3,C4,C5,C6,C7,C8,C9,C10," +
                  "D3,D4,D5,D6,D7,D8," +
                  "E4,E5,E6,E7",
                   3, Result = "E1,E2,E3")]
        [TestCase("A1,A2,A3,A4,A5,A6,A7,A8,A9,A10," +
                  "B1,B2,B3,B4,B5,B6,B7,B8,B9,B10," +
                  "C1,C2,C3,C4,C5,C6,C7,C8,C9,C10," +
                  "D3,D4,D5,D6,D7,D8," +
                  "E3,E4,E5,E6,E7",
                   3, Result = "E8,E9,E10")]
        [TestCase("A1,A2,A3,A4,A5,A6,A7,A8,A9,A10," +
                  "B1,B2,B3,B4,B5,B6,B7,B8,B9,B10," +
                  "C1,C2,C3,C4,C5,C6,C7,C8,C9,C10," +
                  "D2,D3,D4,D5,D6,D7,D8,", +
                   2, Result = "D9,D10")]
        [TestCase("A1,A2,A3,A4,A5,A6,A7,A8,A9,A10," +
                  "B2,B3,B4,B5,B6,B7,B8,B9,B10," +
                  "C1,C2,C3,C4,C5,C6,C7,C8,C9,C10," +
                  "D2,D3,D4,D5,D6,D7,D8,", +
                   1, Result = "B1")]
        public string SuggestTest1(string bookedSeatsStr, int partySize)
        {
            SeatFinder seatFinder = new SeatFinder(bookedSeatsStr);
            List<Seat> seats = seatFinder.Suggest(partySize);
            StringBuilder sb = new StringBuilder();
            foreach (var seat in seats)
            {
                if (sb.Length > 0)
                    sb.Append(",");
                sb.Append(seat.ToString());
            }
            return sb.ToString();

        }
            
    }
}
