using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheaterSeating
{

    public class Theater
    {
        public List<string> Rows { get; }
        public Dictionary<string, List<int>> Seats { get; }

        public Theater()
        {
            Rows = new List<string>()
                { "A", "B", "C", "D", "E", "F", "G"};
            Seats = new Dictionary<string, List<int>>()
            {
                {"A", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
                {"B", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
                {"C", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
                {"D", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
                {"E", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
                {"F", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
                {"G", new List<int> {1,2,3,4,5,6,7,8,9,10 }},
            };
        }

        public Theater(List<string> rows, Dictionary<string, List<int>> seatsAvailable)
        {
            Rows = rows;
            Seats = seatsAvailable;
        }
    }
}

