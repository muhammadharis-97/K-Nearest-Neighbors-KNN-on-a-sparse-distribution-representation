using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using KNN;
using System.Collections;
using System.Reflection.Emit;
using System.Diagnostics;

namespace KNNImplementation
{
    public class SequenceDataEntry
    {
        public string SequenceName { get; set; }
        public List<double> SequenceData { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = "/Users/zakaahmedchishti/Projects/New/New/Dataset_KNN.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            List<SequenceDataEntry> sequenceDataList = JsonConvert.DeserializeObject<List<SequenceDataEntry>>(jsonData);

            List<List<double>> allFeatures = sequenceDataList.Select(entry => entry.SequenceData).ToList();
            List<string> allLabels = sequenceDataList.Select(entry => entry.SequenceName).ToList();

            // Split data into training and testing sets
            List<List<double>> trainingFeatures = new List<List<double>>();
            List<string> trainingLabels = new List<string>();
            List<List<double>> testingFeatures = new List<List<double>>();
            List<string> testingLabels = new List<string>();

            Random rand = new Random();
            foreach (var entry in sequenceDataList)
            {
                string label = entry.SequenceName;
                List<double> features = entry.SequenceData;

                if (rand.NextDouble() < 0.8) // 80% for training
                {
                    trainingFeatures.Add(features.ToList()); // Add a copy of features
                    trainingLabels.Add(label);
                }
                else // 20% for testing
                {
                    testingFeatures.Add(features.ToList()); // Add a copy of features
                    testingLabels.Add(label);
                }
            }

            Debug.WriteLine("Starting of KNN Classifier on Sparse Distribution Representation");

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

            double accuracy = knnClassifier.CalculateAccuracy(predictedLabels, testingLabels);
            Debug.WriteLine($"Accuracy: {accuracy}%");
        }
    }
}