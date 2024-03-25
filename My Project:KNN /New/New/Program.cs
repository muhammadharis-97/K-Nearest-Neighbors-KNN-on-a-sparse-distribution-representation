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
    // Represents an entry in the dataset, containing a sequence name and its associated data.
    public class SequenceDataEntry
    {
        // The name of the sequence.
        public string SequenceName { get; set; }
        // The data associated with the sequence.
        public List<double> SequenceData { get; set; }
    }

    class Program
    {
        // Main method to start the program
        static void Main(string[] args)
        {
            string jsonFilePath = "/Users/zakaahmedchishti/Projects/New/New/Dataset_KNN.json";

            // start experiement that demonstrates how to predict the squence based on HTM predcited cells.
            KNNClassificationExperiment(jsonFilePath);
        }

        // Conducts a KNN classification experiment using the provided dataset. Method Classifier is called inside this method. 
        static void KNNClassificationExperiment(string jsonFilePath)
        {
            List<SequenceDataEntry> sequenceDataEntries = LoadDataset(jsonFilePath);

            // Split data into training and testing sets
            KNN.KNNClassifier.SplitDataset(sequenceDataEntries, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels);

            Debug.WriteLine("Starting KNN Classifier on Sparse Distribution Representation");

            // Initialize and test KNN classifier
            KNNClassifier knnClassifier = new KNNClassifier();
            List<string> predictedLabels = knnClassifier.Classifier(testingFeatures, trainingFeatures, trainingLabels, k: 3);

            // Calculate accuracy
            double accuracy = knnClassifier.CalculateAccuracy(predictedLabels, testingLabels);
            Debug.WriteLine($"Accuracy of KNN Classifier: {accuracy}%");
        }

        // Loads the dataset from the specified JSON file.
        static List<SequenceDataEntry> LoadDataset(string jsonFilePath)
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<SequenceDataEntry>>(jsonData);
        }
    }
}


//TestingFeatures is our test Data
//TrainingFeatures is our training Data

//Training Labels
//Testing Labels
