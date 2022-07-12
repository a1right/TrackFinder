using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobCoders
{
    internal class Point
    {
        public int Column { get; set; }
        public int Row { get; set; }
        public bool IsChecked { get; set; }
        public int Value { get; set; }

        public Point(int column, int row, bool @checked, int value )        {
            Column = column;
            Row = row;
            IsChecked = @checked;
            Value = value;
        }

        public static bool IsNeighborCheckPoint(Point point, Point neighbour)
        {
            int columnDifference = Math.Abs(point.Column - neighbour.Column);
            int rowDifference = Math.Abs(point.Row - neighbour.Row);
            return columnDifference <= 1 && rowDifference <= 1;
        }
    }
}
