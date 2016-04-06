using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TheaterSeating
{
    [DataContract]
    public class Seat
    {
        public Seat(string row, int num)
        {
            Row = row;
            Number = num;
        }

        public Seat(string rowId)
        {
            Row = rowId.Substring(0, 1);
            Number = int.Parse(rowId.Substring(1));
        }

        public static implicit operator string(Seat seatStr)
        {
            return seatStr.ToString();
        }

        [DataMember]
        public string Row { get; set; }

        [DataMember]
        public int Number { get; set; }

        public override string ToString()
        {
            return Row + Number;
        }
    }


}