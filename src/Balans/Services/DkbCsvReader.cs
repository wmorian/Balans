using Balans.Models;
using Balans.Models.Banks.DKB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Balans.Services
{
    /// <summary>
    /// Reader for the DKB Bank
    /// </summary>
    public static class DkbCsvReader
    {
        public static IEnumerable<DkbEntity> GetEntities(string path)
        {
            var data = CsvReader.GetData(path);

            int offset = 6;
            var entities = new List<DkbEntity>();
            for (int i = offset; i < data.Count(); i++)
            {
                var values = data[i].ToList();
                DkbEntity entity = ExtractEntity(values);
                entities.Add(entity);
            }

            return entities;
        }

        private static DkbEntity ExtractEntity(List<string> values) => new DkbEntity
        {
            DateOfBooking = DateTime.Parse(values[0].Trim('\"')),
            ValueDate = DateTime.Parse(values[1].Trim('\"')),
            BookingType = values[2].Trim('\"'),
            Initiator = values[3].Trim('\"'),
            Purpose = values[4].Trim('\"'),
            AccountNumber = values[5].Trim('\"'),
            BLZ = values[6].Trim('\"'),
            Amount = float.Parse(values[7].Trim('\"')),
            CreditorId = values[8].Trim('\"'),
            MandateReference = values[9].Trim('\"'),
            ClientReference = values[10].Trim('\"')
        };
    }
}
