using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


public class SeatModel
{
    private string _type;
    private string _icon;
    private double _price;
    private bool _isSeat;

    private bool _reserved = false;

    public string OriginalType { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    private bool _selected = false;
    public bool Selected
    {
        get { return _selected; }
        set { _selected = value; }
    }

    public bool Reserved { get { return _reserved; } set { _reserved = value; } }

    public string Type
    {
        get { return _type; }
        set
        {
            _type = value;
        }
    }

    public string Icon
    {
        get { return _icon; }
        private set
        {
            if (_type is ("a" or "p" or "v"))
            {
                _icon = "■";


            }
            else
            {
                _icon = " ";
            }
        }
    }

    public double Price
    {
        get { return _price; }
        private set
        {
            if (_type == "a")
            {
                _price = 7.50;
            }

            if (_type == "p")
            {
                _price = 9.50;
            }

            if (_type == "v")
            {
                _price = 14.75;
            }
        }
    }

    public bool isSeat
    {
        get { return _isSeat; }
        set
        {
            if (_type is ("a" or "p" or "v"))
            {
                _isSeat = true;

            }
            else
            {
                _isSeat = false;

            }

        }
    }




    public SeatModel(string type, int row, int column)
    {
        OriginalType = type;
        Type = type;
        Row = row;
        Column = column;
        Icon = "init";
        isSeat = false;
        Price = 0;
        Selected = false;
    }


    public void PrintAttributes()
    {
        Type carType = typeof(SeatModel);
        PropertyInfo[] properties = carType.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            string attributeName = property.Name;
            object attributeValue = property.GetValue(this);

            Console.WriteLine($"{attributeName}: {attributeValue}");
        }
    }


}
