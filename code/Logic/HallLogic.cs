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



    // private static SeatModel[][] SelectSeat(SeatModel[][] hall)
    // {
    //     while (true)
    //     {

    //         Helpers.WarningMessage("Select a seat by entering the row and column number exmaple(2,4):");
    //         var index = Console.ReadLine();
    //         var splitIndex = index.Split(",");

    //         var row = int.Parse(splitIndex[0]);
    //         var column = int.Parse(splitIndex[1]);

    //         try
    //         {
    //             var selectedSeat = hall[row][column];

    //             if (selectedSeat.isSeat && !selectedSeat.Reserved)
    //             {
    //                 selectedSeat.Reserved = true;
    //                 selectedSeat.Type = "x";
    //                 return hall;
    //             }
    //             else
    //             {
    //                 Helpers.WarningMessage("Please select a valid seat.");
    //                 continue;
    //             }
    //         }
    //         catch (Exception)
    //         {
    //             Helpers.WarningMessage("Something went wrong");

    //         }
    //     }

    // }

    // public void Flow()
    // {
    //     var oldHall = GenerateHall();
    //     ShowHall(oldHall);

    //     // Console.WriteLine(seat.GetAttributes());
    //     var newHall = SelectSeat(oldHall);
    //     ShowHall(newHall);
    // }


}