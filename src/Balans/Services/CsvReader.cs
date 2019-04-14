using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Services
{
    public class CsvReader
    {
        public static IList<IEnumerable<string>> GetData(string path)
        {
            var lines = File.ReadAllLines(path);

            IList<IEnumerable<string>> csv = new List<IEnumerable<string>>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var columns = line.Split(';').Where(cell => !string.IsNullOrWhiteSpace(cell));
                csv.Add(columns);
            }

            return csv;
        }
    }
}
