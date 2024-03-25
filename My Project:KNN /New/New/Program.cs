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
            TrainAndTestKNNClassifier(jsonFilePath);
        }

        static void TrainAndTestKNNClassifier(string jsonFilePath)
        {
            List<SequenceDataEntry> sequenceDataList = LoadDataset(jsonFilePath);

            // Split data into training and testing sets
            SplitDataset(sequenceDataList, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels);

            Debug.WriteLine("Starting of KNN Classifier on Sparse Distribution Representation");

            // Initialize and test KNN classifier
            KNNClassifier knnClassifier = new KNNClassifier();
            List<string> predictedLabels = knnClassifier.Classifier(testingFeatures, trainingFeatures, trainingLabels, k: 3);

            // Calculate accuracy
            double accuracy = knnClassifier.CalculateAccuracy(predictedLabels, testingLabels);
            Debug.WriteLine($"Accuracy of KNN Classifier: {accuracy}%");
        }

        static List<SequenceDataEntry> LoadDataset(string jsonFilePath)
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<SequenceDataEntry>>(jsonData);
        }

        static void SplitDataset(List<SequenceDataEntry> sequenceDataList, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels)
        {
            trainingFeatures = new List<List<double>>();
            trainingLabels = new List<string>();
            testingFeatures = new List<List<double>>();
            testingLabels = new List<string>();

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
        }

        static void TrainAndTestKNNClassifier(List<List<double>> trainingFeatures, List<string> trainingLabels,
                                              List<List<double>> testingFeatures, List<string> testingLabels)
        {
            // Initialize KNN classifier
            KNNClassifier knnClassifier = new KNNClassifier();

            // Test the classifier with testing data
            List<string> predictedLabels = knnClassifier.Classifier(testingFeatures, trainingFeatures, trainingLabels, 3);
            foreach (var label in predictedLabels)
            {
                Console.WriteLine(label);
            }

            double accuracy = knnClassifier.CalculateAccuracy(predictedLabels, testingLabels);
            Debug.WriteLine($"Accuracy: {accuracy}%");
        }
    }
}