using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using HCCInfrastructure.Models;

namespace HCCInfrastructure.Helpers
{
    public static class FileHelpers
    {
        public static IEnumerable<List<string>> ReadFileAsLineSets(string fileName, int setLen = 10)
        {
            string headerLine = "";
            using (var reader = new StreamReader(fileName))
            {
                if (!reader.EndOfStream)
                {
                    headerLine = reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    var set = new List<string>();
                    set.Add(headerLine);
                    for (var i = 0; i < setLen && !reader.EndOfStream; i++)
                    {
                        set.Add(reader.ReadLine());
                    }
                    yield return set;
                }
            }
        }

        public static List<BatchFileLineModel> ReadBatchFileAsLineModels(string fileName)
        {
            var records = new List<BatchFileLineModel>();

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<BatchFileLineModel>().ToList();
            }

            return records;
        }
    }
}
