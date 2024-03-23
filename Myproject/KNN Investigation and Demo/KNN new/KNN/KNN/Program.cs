using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        List<DataEntry> data = ReadJsonData("C:\\Users\\Lenovo\\Documents\\GitHub\\Global_Variables\\Myproject\\KNN Investigation and Demo\\KNN new\\KNN\\KNN\\Dataset_KNN.json");

        // Access and print the data
        foreach (var entry in data)
        {
            Console.WriteLine("Sequence:");
            foreach (var sequence in entry.SequenceData)
            {
                Console.WriteLine($"{sequence.Key}: {string.Join(", ", sequence.Value)}");
            }
            //Console.WriteLine("SDR: " + string.Join(", ", entry.SDR));
            Console.WriteLine();
        }
    }

    static List<DataEntry> ReadJsonData(string filePath)
    {
        try
        {
            // Read JSON file
            string jsonContent = File.ReadAllText(filePath);

            // Deserialize JSON content
            List<DataEntry> data = JsonConvert.DeserializeObject<List<DataEntry>>(jsonContent);

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the JSON file: {ex.Message}");
            return null;
        }
    }
}

class DataEntry
{
    public Dictionary<string, List<double>> SequenceData{ get; set; }
    public List<int> SDR { get; set; }
}
