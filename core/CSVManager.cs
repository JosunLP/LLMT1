using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LLMT1.core
{
    internal class CSVManager
    {
        private readonly string filePath;

        public CSVManager(string filePath)
        {
            this.filePath = filePath;
        }

        public void WriteToFile(Dictionary data)
        {
            try
            {
                using StreamWriter sw = new(filePath);
                foreach (string[] rowData in data)
                {
                    string row = string.Join(",", rowData);
                    sw.WriteLine(row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
            }
        }

        public List<string[]> ReadFromFile()
        {
            List<string[]> data = new();

            try
            {
                using (StreamReader sr = new(filePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        string row = sr.ReadLine();
                        string[] rowData = row.Split(',');
                        data.Add(rowData);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while reading from the file: " + e.Message);
            }

            return data;
        }
    }
}