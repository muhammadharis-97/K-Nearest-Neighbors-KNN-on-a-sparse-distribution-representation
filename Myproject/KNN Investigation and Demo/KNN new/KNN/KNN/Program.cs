using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using KNN;

namespace KNNImplementation
{
    // Define classes to represent the JSON structure
    public class SequenceDataEntry
    {
        public Dictionary<string, List<double>> SequenceData { get; set; }
    }

    public class Dataset
    {
        public List<SequenceDataEntry> Data { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Read JSON data from file
            string jsonFilePath = "C:\\Users\\Lenovo\\Documents\\GitHub\\Global_Variables\\Myproject\\KNN Investigation and Demo\\KNN new\\KNN\\KNN\\Dataset_KNN.json"; // Update this with your file path
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserialize JSON data into a list of SequenceDataEntry objects
            List<SequenceDataEntry> sequenceDataList = JsonConvert.DeserializeObject<List<SequenceDataEntry>>(jsonData);

            // Separate the data into training and testing sets
            List<List<double>> trainingFeatures = new List<List<double>>();
            List<string> trainingLabels = new List<string>();
            List<List<double>> testingFeatures = new List<List<double>>();
            List<string> testingLabels = new List<string>();

            // Assuming you want to randomly assign data to training or testing set
            Random rand = new Random();
            foreach (var entry in sequenceDataList)
            {
                foreach (var sequenceData in entry.SequenceData)
                {
                    string label = sequenceData.Key;
                    List<double> features = sequenceData.Value;

                    // Randomly assign data to training or testing set (example logic)
                    if (rand.NextDouble() < 0.8) // 80% for training
                    {
                        trainingFeatures.Add(features);
                        trainingLabels.Add(label);
                    }
                    else // 20% for testing
                    {
                        testingFeatures.Add(features);
                        testingLabels.Add(label);
                    }
                }
            }

            // Train and test the KNN classifier using the extracted data
            TrainAndTestKNNClassifier(trainingFeatures, trainingLabels, testingFeatures, testingLabels);
        }

        static void TrainAndTestKNNClassifier(List<List<double>> trainingFeatures, List<string> trainingLabels,
                                              List<List<double>> testingFeatures, List<string> testingLabels)
        {
            // Initialize KNN classifier
            KNNClassifier knnClassifier = new KNNClassifier();

            // Test the classifier with testing data
            List<string> predictedLabels = knnClassifier.Test(testingFeatures, trainingFeatures, trainingLabels, 3);
            foreach (var label in predictedLabels)
            {
                Console.WriteLine(label);
            }
            // Calculate and print accuracy
            double accuracy = knnClassifier.CalculateAccuracy(predictedLabels, testingLabels);
            Console.WriteLine($"Accuracy: {accuracy}%");
        }
    }
}