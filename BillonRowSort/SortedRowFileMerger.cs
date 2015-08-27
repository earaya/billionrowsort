using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillonRowSort
{
    internal class SortedRowFileMerger : IDisposable
    {
        private TextWriter writer;
        private SortedDictionary<Row, TextReader> priorityQueue = new SortedDictionary<Row, TextReader>();
        private IEnumerable<TextReader> readers;

        internal SortedRowFileMerger(TextWriter writer)
        {
            this.writer = writer;
        }

        internal void Merge(IEnumerable<string> files)
        {
            readers = files.Select(file => File.OpenText(file));
            foreach (TextReader reader in readers)
            {
                string line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    Row row = Row.FromLine(line);
                    priorityQueue.Add(row, reader);
                }
            }

            while (priorityQueue.Count > 0)
            {
                var smallestPair = priorityQueue.First();
                var smallestRow = smallestPair.Key;
                var smallestStream = smallestPair.Value;
                writer.WriteLine(smallestRow);
                priorityQueue.Remove(smallestRow);
                string line = smallestStream.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    Row row = Row.FromLine(line);
                    priorityQueue.Add(row, smallestStream);
                }
            }
        }

        public void Dispose()
        {
            foreach (var reader in readers)
            {
                reader.Dispose();
            }
        }
    }
}
