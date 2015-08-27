using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillonRowSort
{
    internal class SortedRowBuffer
    {
        private const int CHUNK_ROW_SIZE = 1000000;
        private List<string> files = new List<string>();
        private List<Row> rows = new List<Row>();
        private TextReader reader;

        internal SortedRowBuffer(TextReader reader)
        {
            this.reader = reader;
        }

        internal IEnumerable<string> Chunk()
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                rows.Add(Row.FromLine(line));
                
                if (rows.Count == CHUNK_ROW_SIZE)
                {
                    yield return Flush();
                }
            }

            if (rows.Count > 0)
            {
                yield return Flush();
            }
        }

        private string Flush()
        {
            rows.AsParallel()
                .OrderBy(row => row.ColumnOne)
                .ThenBy(row => row.ColumnTwo)
                .ThenBy(row => row.ColumnThree);
            string file = Path.GetTempFileName();
            using (TextWriter tw = File.CreateText(file))
            {
                rows.ForEach(row => tw.WriteLine(row));
            }
            rows.Clear();
            return file;
        }
    }
}
