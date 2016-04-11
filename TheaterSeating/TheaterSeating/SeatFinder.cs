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
            foreach (var row in Theater.Rows)
            {
                ret = FindAdjacentSeats(row, Theater.Seats[row], partySize);
                if (ret != null)
                    break;
            }
            return ret;
        }

        public List<Seat> FindAdjacentSeats(string row, List<int> seats, int partySize)
        {
            List<Seat> ret = null;
            var adjacentSeats = FindAdjacentSeatsInMiddle(row, seats, partySize);
            if (adjacentSeats == null)
            {
                adjacentSeats = FindAdjacentSeatsOnLeft(row, seats, partySize);
                if (adjacentSeats == null)
                    adjacentSeats = FindAdjacentSeatsOnRight(row, seats, partySize);
            }
            if (adjacentSeats != null)
            {
                ret = new List<Seat>();
                for (int i = adjacentSeats.Item1; i <= adjacentSeats.Item2; i++)
                {
                    ret.Add(new Seat(row, i));
                }
            }
            return ret;

        }

        public Tuple<int,int> FindAdjacentSeatsInMiddle(string row, List<int> seats, int partySize)
        {
            // first try to find seats in the middle:
            int dec = partySize / 2;
            if (dec > 0)
                dec--;
            int firstSeat = (seats.Count / 2) - dec;
            int lastSeat = firstSeat + partySize - 1;
            // confirm if all are available:
            bool ok = true;
            for (int i = firstSeat; i <= lastSeat && ok; i++)
            {
                ok = !BookedSeats.Contains(row + i);
            }
            return ok ? new Tuple<int, int>(firstSeat, lastSeat) : null;
        }

        /// <summary>
        /// Count how many seats are available at the left.
        /// Precondition: At least one seat in the middle is booked.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="seats"></param>
        /// <param name="partySize"></param>
        /// <returns></returns>
        public Tuple<int, int> FindAdjacentSeatsOnLeft(string row, List<int> seats, int partySize)
        {
            int firstSeat = -1;
            int lastSeat = -1;
            bool ok = false;
            int count = 0;
            for (int i = 0; i < seats.Count / 2; i++)
            {
                if (!BookedSeats.Contains(row + seats[i]))
                    count++;
                else
                {
                    if (count >= partySize)
                    {
                        int index = i - 1;
                        lastSeat = seats[index];
                        index = index - partySize + 1;
                        firstSeat = seats[index];
                        // avoid leaving an orphaned empty seat on the far left:
                        ok = index != 1;
                    }
                    break;
                }
            }
            return ok ? new Tuple<int, int>(firstSeat, lastSeat) : null;
        }

        /// <summary>
        /// Count how many seats are available at the right:
        /// Precondition: At least one seat in the middle is booked.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="seats"></param>
        /// <param name="partySize"></param>
        /// <returns></returns>
        public Tuple<int, int> FindAdjacentSeatsOnRight(string row, List<int> seats, int partySize)
        {
            int firstSeat = -1;
            int lastSeat = -1;
            bool ok = false;
            int count = 0;
            for (int i = seats.Count - 1; i >= seats.Count / 2; i--)
            {
                if (!BookedSeats.Contains(row + seats[i]))
                    count++;
                else
                {
                    if (count >= partySize)
                    {
                        int index = i + 1;
                        firstSeat = seats[index];
                        index = index + partySize - 1;
                        lastSeat = seats[index];
                        // avoid leaving an orphaned empty seat on the far right:
                        ok = index != seats.Count - 2;
                    }
                    break;
                }
            }
            return ok ? new Tuple<int, int>(firstSeat, lastSeat) : null;
        }

    }
}
