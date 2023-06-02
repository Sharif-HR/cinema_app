using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

public class HallLogic
{
    public SeatModel[][] GenerateHall()
    {
        Hall hall = new();
        SeatModel[][] seatArrangement = hall.seatArrangement;

        string[][] rawSeatArrangement = new string[14][]
        {
            new string[] { "0", "0", "a", "a", "a", "a", "a", "a", "a", "a", "0", "0" },
            new string[] { "0", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "0" },
            new string[] { "0", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "0" },
            new string[] { "a", "a", "a", "a", "a", "p", "p", "a", "a", "a", "a", "a" },
            new string[] { "a", "a", "a", "a", "p", "p", "p", "p", "a", "a", "a", "a" },
            new string[] { "a", "a", "a", "p", "p", "v", "v", "p", "p", "a", "a", "a" },
            new string[] { "a", "a", "a", "p", "p", "v", "v", "p", "p", "a", "a", "a" },
            new string[] { "a", "a", "a", "p", "p", "v", "v", "p", "p", "a", "a", "a" },
            new string[] { "a", "a", "a", "p", "p", "v", "v", "p", "p", "a", "a", "a" },
            new string[] { "a", "a", "a", "a", "p", "p", "p", "p", "a", "a", "a", "a" },
            new string[] { "a", "a", "a", "a", "a", "p", "p", "a", "a", "a", "a", "a" },
            new string[] { "0", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "0" },
            new string[] { "0", "0", "a", "a", "a", "a", "a", "a", "a", "a", "0", "0" },
            new string[] { "0", "0", "a", "a", "a", "a", "a", "a", "a", "a", "0", "0" }
        };


        for (int row = 0; row < rawSeatArrangement.Length; row++)
        {
            List<SeatModel> seatRow = new List<SeatModel>();
            for (int seat = 0; seat < rawSeatArrangement[row].Length; seat++)
            {
                var type = rawSeatArrangement[row][seat];
                SeatModel newSeat = new(type: type, column: seat, row: row);
                seatRow.Add(newSeat);
            }
            seatArrangement[row] = seatRow.ToArray();
        }

        return seatArrangement;
    }
}