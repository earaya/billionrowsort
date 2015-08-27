using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillonRowSort
{
    internal struct Row : IComparable<Row>
    {
        internal string ColumnOne { get; set; }
        internal int ColumnTwo { get; set; }
        internal DateTime ColumnThree { get; set; }

        internal static Row FromLine(string line)
        {
            string[] values = line.Split(',');
            Row row = new Row();
            row.ColumnOne = values[0];
            row.ColumnTwo = int.Parse(values[1]);
            row.ColumnThree = DateTime.Parse(values[2]);
            return row;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", ColumnOne, ColumnTwo, ColumnThree);
        }

        public int CompareTo(Row other)
        {
            int result = this.ColumnOne.CompareTo(other.ColumnOne);
            
            if (result == 0)
            {
                result = this.ColumnTwo.CompareTo(other.ColumnTwo);
            }

            if (result == 0)
            {
                result = this.ColumnThree.CompareTo(other.ColumnThree);
            }

            return result;
        }
    }
}

