using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillonRowSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            IEnumerable<string> files;

            SortedRowBuffer chunker = new SortedRowBuffer(Console.In);
            files = chunker.Chunk();

            using (SortedRowFileMerger merger = new SortedRowFileMerger(Console.Out))
            {
                merger.Merge(files);
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }
}


