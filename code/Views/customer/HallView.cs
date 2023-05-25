using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Views;

public class HallView : ViewTemplate
{
    private HallLogic _logic = new();
    private SeatModel[][] _emptyHall;

    public HallView() : base("Auditorium - 150 seats") { }
    private void loadEmptyHall()
    {
        _emptyHall = _logic.GenerateHall();
    }

    public override void Render()
    {
        loadEmptyHall();
        index();

    }

    private void index()
    {
        //TODO  if statement to check if movie has an auditorium
        var hall = _emptyHall;
        while (true)
        {
            base.Render();
            // base.MenuList(new List<string>() {"back", "Select seats"}, SelectedMovie )
            SelectSeat(hall);
        }
    }

    private bool selectAnotherSeat()
    {
        return base.InputBool("Would you like to select another seat?");
    }

    private SeatModel[][] SelectSeat(SeatModel[][] hall)
    {
        while (true)
        {
            try
            {
                base.Render();
                ShowHall(hall);
                Console.WriteLine("Select a seat by entering the [ROW] , [SEAT NUMBER] number.");
                Helpers.WarningMessage("For example: 2,4");
                Console.Write("> ");
                var index = Console.ReadLine();
                var splitIndex = index.Split(",");

                bool validRow = int.TryParse(splitIndex[0], out int row);
                bool validColumn = int.TryParse(splitIndex[1], out int column);


                if (!validRow || !validColumn)
                {
                    Helpers.WarningMessage("Invalid row or column number");
                    Helpers.Continue();

                    continue;
                }


                SeatModel selectedSeat;
                selectedSeat = hall[row - 1][column - 1];


                if (selectedSeat.Reserved)
                {
                    Helpers.WarningMessage("Sorry this seat is reserved. Please choose another one.");
                    Helpers.Continue();

                    continue;
                }

                if (!selectedSeat.isSeat)
                {
                    Helpers.WarningMessage("Sorry there is not seat on this location. Please choose another one.");
                    Helpers.Continue();
                    continue;
                }


                switch (selectedSeat.Selected)
                {
                    case false:
                        selectedSeat.Selected = true;
                        selectedSeat.Type = "s";

                        break;

                    case true:
                        selectedSeat.Selected = false;
                        selectedSeat.Type = selectedSeat.OriginalType;
                        break;
                }

                base.Render();
                ShowHall(hall);
                Helpers.SuccessMessage($"You selected seat number {column} on row {row}! \n\n");

                if (selectAnotherSeat())
                {
                    continue;
                }


                //TODO  reserve selected seats
                // write to csv
                return hall;
            }
            catch (Exception e)
            {
                Helpers.WarningMessage("Something went wrong");
                Helpers.WarningMessage(e.Message + e.StackTrace + e.GetBaseException());
                Helpers.Continue();
            }
        }

    }


    public static SeatModel[][] ShowHall(SeatModel[][] hall)
    {

        XLine();
        Content(hall);
        Screen();

        return hall;
    }

    private static void XLine()
    {
        string marginLeft = new string(' ', 8);
        string div = new string(' ', 39);
        Console.Write($"{marginLeft}{div}SEAT NUMBER{div}");

        Console.WriteLine();
        Console.WriteLine();


        Console.Write(marginLeft);
        for (int i = 1; i < 13; i++)
        {

            if (i >= 10)
            {
                Console.Write($"|  {i} |");
            }
            else
            {
                Console.Write($"|  {i}  |");
            }

        }

        Console.WriteLine();

        string dashes = new string('-', 84);
        Console.WriteLine($"{marginLeft}{dashes}");
    }

    private static void Content(SeatModel[][] hall)
    {

        for (int row = 0; row < hall.Length; row++)
        {
            for (int column = 0; column < hall[row].Length; column++)
            {
                if (column == 0)
                {

                    if (row == 5)
                    {
                        Console.Write("R");
                        Console.Write($"  {row + 1} :  ");

                    }
                    if (row == 6)
                    {
                        Console.Write("O");
                        Console.Write($"  {row + 1} :  ");

                    }
                    if (row == 7)
                    {
                        Console.Write("W");
                        Console.Write($"  {row + 1} :  ");

                    }

                    if (row >= 9)
                    {
                        Console.Write($"   {row + 1}:  ");
                    }

                    if (row < 9 && row is not (5 or 6 or 7))
                    {
                        Console.Write($"   {row + 1} :  ");
                    }

                }
                var seat = hall[row][column];
                string visualSeat = $"|  {seat.Icon}  |";

                switch (seat.Type)
                {
                    case ("a"):
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;

                    case ("p"):
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;


                    case ("v"):
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;


                    case ("s"):
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;

                    case ("x"):
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;

                    default:
                        Console.Write(new string(' ', 7));
                        break;

                }

            }
            Console.WriteLine();
        }
    }

    private static void Screen()
    {
        string sp = new string(' ', 39);
        string margin = new string(' ', 8);
        Console.WriteLine();
        Console.WriteLine();
        Console.Write(margin);
        Console.WriteLine(new string('_', 84));

        Console.WriteLine($"{margin}{sp}SCREEN{sp}");
        Console.WriteLine();
        Console.WriteLine();
    }



}
