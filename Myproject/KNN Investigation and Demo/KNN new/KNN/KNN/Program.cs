using System;
using System.Collections.Generic;

namespace KNNImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample trainData
            Dictionary<List<double>, List<double>> trainData = new Dictionary<List<double>, List<double>>
            {
                {new List<double> {1.0, 2.0}, new List<double> {0.1, 0.2, 0.3}},  // Features for instance 1
                {new List<double> {3.0, 4.0}, new List<double> {0.4, 0.5, 0.6}},  // Features for instance 2
                {new List<double> {5.0, 6.0}, new List<double> {0.7, 0.8, 0.9}}   // Features for instance 3
                // Add more instances as needed
            };

            // Sample testData
            List<double> testData = new List<double> { 0.9, 1.1 }; // Features for the test instance

            // Example usage of KNNClassifier
            KNNClassifier knnClassifier = new KNNClassifier();
            int numofclass = 2; // Assuming there are 2 classes
            int k = 1; // Number of nearest neighbors to consider
            double predictedLabel = knnClassifier.Classifier(testData, trainData, numofclass, k);
            Console.WriteLine($"Predicted label for test instance: {predictedLabel}");
        }
    }
}