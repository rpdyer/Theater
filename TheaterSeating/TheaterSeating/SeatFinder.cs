using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TheaterSeating
{
    public class SeatFinder
    {
        public Theater Theater { get; set; }
        public HashSet<string> BookedSeats { get; set; } 

        public SeatFinder()
        {
            Theater = new Theater();
            BookedSeats = new HashSet<string>();
        }

        public SeatFinder(List<Seat> bookedSeats)
        {
            Theater = new Theater();
            BookedSeats = new HashSet<string>();
            foreach (var seat in bookedSeats)
            {
                BookedSeats.Add(seat);
            }
        }

        public SeatFinder(string bookedSeatsStr)
        {
            Theater = new Theater();
            BookedSeats = new HashSet<string>();
            string[] seats = bookedSeatsStr.Split(new string[] {","}, StringSplitOptions.None);
            foreach (var s in seats)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var seat = new Seat(s);
                    BookedSeats.Add(seat);
                }
            }
        }

        public List<Seat> Suggest(int partySize)
        {
            List<Seat> ret = null;
            return ret;
        }

    }
}
