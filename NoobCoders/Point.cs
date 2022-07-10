using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobCoders
{
    internal class Point
    {
        public Point(int column, int row, bool @checked, int value )        {
            Column = column;
            Row = row;
            isChecked = @checked;
            Value = value;
        }


        public int Column { get; set; }
        public int Row { get; set; }
        public bool isChecked { get; set; }
        public int Value { get; set; }

        public static bool CheckPointIsNeighbor(Point point, Point neighbour)
        {
            int columnDifference = Math.Abs(point.Column - neighbour.Column);
            int rowDifference = Math.Abs(point.Row - neighbour.Row);
            if (columnDifference > 1 || rowDifference > 1)
                return false;
            return true;
        }
    }
}
