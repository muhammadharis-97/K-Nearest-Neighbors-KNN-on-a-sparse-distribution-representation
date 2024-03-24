using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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

    public class KNNClassifier
    {
        public double CalculateEuclideanDistance(List<double> testData, List<double> trainData)
        {
            if (testData == null || trainData == null)
                throw new ArgumentNullException("Both testData and trainData must not be null.");

            if (testData.Count != trainData.Count)
                throw new ArgumentException("testData and trainData must have the same length.");

            double sumOfSquaredDifferences = 0.0;

            for (int i = 0; i < testData.Count; ++i)
            {
                double difference = testData[i] - trainData[i];
                sumOfSquaredDifferences += difference * difference;
            }

            return Math.Sqrt(sumOfSquaredDifferences);
        }

        public int Vote(IndexAndDistance[] info, List<int> trainLabels, int numofclass, int k)
        {
            int[] votes = new int[numofclass];

            for (int i = 0; i < k; ++i)
            {
                int idx = info[i].idx;
                int c = trainLabels[idx];
                ++votes[c];
            }

            int classWithMostVotes = Array.IndexOf(votes, Max(votes));
            return classWithMostVotes;
        }

        private int Max(int[] arr)
        {
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;
        }

        public List<int> Test(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<int> trainingLabels, int k)
        {
            List<int> predictedLabels = new List<int>();

            foreach (var testData in testingFeatures)
            {
                IndexAndDistance[] info = new IndexAndDistance[trainingFeatures.Count];

                for (int i = 0; i < trainingFeatures.Count; i++)
                {
                    double dist = CalculateEuclideanDistance(testData, trainingFeatures[i]);
                    info[i] = new IndexAndDistance { idx = i, dist = dist };
                }

                Array.Sort(info);

                int result = Vote(info, trainingLabels, 3, k);
                predictedLabels.Add(result);
            }

            return predictedLabels;
        }

        public double CalculateAccuracy(List<int> predictedLabels, List<int> actualLabels)
        {
            int correctPredictions = 0;
            for (int i = 0; i < predictedLabels.Count; i++)
            {
                if (predictedLabels[i] == actualLabels[i])
                    correctPredictions++;
            }
            double accuracy = (double)correctPredictions / predictedLabels.Count * 100;
            return accuracy;
        }
    }

    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;
        public double dist;

        public int CompareTo(IndexAndDistance other)
        {
            return dist.CompareTo(other.dist);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Read JSON data from file
            string jsonFilePath = "C:\\Users\\Lenovo\\Documents\\GitHub\\Global_Variables\\Myproject\\KNN Investigation and Demo\\KNN new\\KNN\\KNN\\Dataset_KNN.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserialize JSON data into Dataset object
            Dataset dataset = JsonConvert.DeserializeObject<Dataset>(jsonData);

            // Separate the data into training and testing sets
            List<List<double>> trainingFeatures = new List<List<double>>();
            List<int> trainingLabels = new List<int>();
            List<List<double>> testingFeatures = new List<List<double>>();
            List<int> testingLabels = new List<int>();

            foreach (var entry in dataset.Data)
            {
                foreach (var sequenceData in entry.SequenceData)
                {
                    // Extract features and SDR data from each entry
                    string sequenceName = sequenceData.Key;
                    List<double> features = sequenceData.Value;
                    List<double> sdrData = sequenceData.Value; // Assuming SDR data is also present

                    // Determine the label based on the sequence name (example logic)
                    int label = DetermineLabel(sequenceName);

                    // Randomly assign data to training or testing set (example logic)
                    if (new Random().NextDouble() < 0.8) // 80% for training
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

        static int DetermineLabel(string sequenceName)
        {
            // Example logic to determine the label based on the sequence name
            // You can implement your own logic here based on your specific dataset
            // For this example, we'll simply assign labels based on sequence names
            switch (sequenceName)
            {
                case "S1":
                    return 0;
                case "S2":
                    return 1;
                case "S3":
                    return 2;
                default:
                    return -1; // Unknown label
            }
        }

        static void TrainAndTestKNNClassifier(List<List<double>> trainingFeatures, List<int> trainingLabels,
                                              List<List<double>> testingFeatures, List<int> testingLabels)
        {
            // Initialize KNN classifier
            KNNClassifier knnClassifier = new KNNClassifier();

            // Test the classifier with testing data
            List<int> predictedLabels = knnClassifier.Test(testingFeatures, trainingFeatures, trainingLabels, 3);

            // Calculate and print accuracy
            double accuracy = knnClassifier.CalculateAccuracy(predictedLabels, testingLabels);
            Console.WriteLine($"Accuracy: {accuracy}%");
        }
    }
}