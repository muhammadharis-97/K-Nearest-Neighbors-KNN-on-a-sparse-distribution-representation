using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KNNImplementation;

namespace KNN
{
    /// KNN class is designed to perform classification tasks on sequences derived from the SDR (Sparse Distributed Representation) dataset
    public class KNNClassifier
    {
        /// Performs classification on the testing features using k-Nearest Neighbors algorithm.
        /// <returns>The list of predicted labels for the testing features.</returns>

        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k)
        {
            List<string> predictedLabels = new List<string>();

            foreach (var testFeature in testingFeatures)
            {
                IndexAndDistance[] nearestNeighbors = new IndexAndDistance[trainingFeatures.Count];

                for (int i = 0; i < trainingFeatures.Count; i++)
                {
                    double distance = CalculateEuclideanDistance(testFeature, trainingFeatures[i]);
                    nearestNeighbors[i] = new IndexAndDistance { Index = i, Distance = distance };
                }

                // Sort distances
                Array.Sort(nearestNeighbors);

                Debug.WriteLine("   Nearest     /    Distance      /     Class   ");
                Debug.WriteLine("   ==========================================   ");

                for (int i = 0; i < k; i++)
                {
                    int nearestIndex = nearestNeighbors[i].Index;
                    double nearestDistance = nearestNeighbors[i].Distance;
                    string nearestClass = trainingLabels[nearestIndex];
                    Debug.WriteLine($"( {trainingFeatures[nearestIndex][0]}, {trainingFeatures[nearestIndex][1]} )  :  {nearestDistance}        {nearestClass}");
                }

                // Vote for the class based on the top k nearest neighbors
                int result = Vote(nearestNeighbors, trainingLabels, k);
                predictedLabels.Add(trainingLabels[result]);
            }

            return predictedLabels;
        }

        // Splits the dataset into training and testing sets.
        public static void SplitDataset(List<SequenceDataEntry> sequenceDataList, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels)
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
                    trainingFeatures.Add(new List<double>(features)); // Add a copy of features
                    trainingLabels.Add(label);
                }
                else // 20% for testing
                {
                    testingFeatures.Add(new List<double>(features)); // Add a copy of features
                    testingLabels.Add(label);
                }
            }
        }



            // Calculates the accuracy of the predicted labels compared to the actual labels.
              public double CalculateAccuracy(List<string> predictedLabels, List<string> testingLabels)
        {
            int correctPredictions = predictedLabels.Where((predictedLabel, index) => predictedLabel == testingLabels[index]).Count();
            double accuracy = (double)correctPredictions / predictedLabels.Count * 100;
            return accuracy;
        }

        // <summary>
        // Calculates the Euclidean distance between two vectors.
        // </summary>
        // <param name="testData">The test data vector.</param>
        // <param name="trainData">The training data vector.</param>
        // <returns>The Euclidean distance between the two vectors.</returns>

        private double CalculateEuclideanDistance(List<double> testingFeatures, List<double> trainingFeatures)
        {
            if (testingFeatures == null || trainingFeatures == null)
                throw new ArgumentNullException("Both testData and trainData must not be null.");

            if (testingFeatures.Count != trainingFeatures.Count)
                throw new ArgumentException("testData and trainData must have the same length.");

            double sumOfSquaredDifferences = 0.0;

            for (int i = 0; i < testingFeatures.Count; ++i)
            {
                double difference = testingFeatures[i] - trainingFeatures[i];
                sumOfSquaredDifferences += difference * difference;
            }

            // Return the square root of the sum of squared differences
            return Math.Sqrt(sumOfSquaredDifferences);
        }

        // <summary>
        // Determines the class label by majority voting among the k nearest neighbors.
        // </summary>
        // <param name="nearestNeighbors">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        // <param name="trainingLabels">The training data containing class labels.</param>
        // <param name="numOfClasses">The total number of classes.</param>
        // <param name="k">The number of nearest neighbors to consider.</param>
        // <returns>The class label with the most votes among the nearest neighbors.</returns>

        private int Vote(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int k)
        {
            Dictionary<string, int> votes = new Dictionary<string, int>();

            foreach (string label in trainingLabels)
            {
                votes[label] = 0;
            }

            for (int i = 0; i < k; ++i)
            {
                string neighborLabel = trainingLabels[nearestNeighbors[i].Index];
                votes[neighborLabel]++;
            }

            // Find the class label with the most votes
            string classWithMostVotes = votes.OrderByDescending(pair => pair.Value).First().Key;
            Debug.WriteLine($"  Predicted class for test data: {(classWithMostVotes == "S1" ? "Even" : (classWithMostVotes == "S2" ? "Odd" : (classWithMostVotes == "S3" ? "Neither Odd nor Even" : "Unknown")))}");
            return trainingLabels.IndexOf(classWithMostVotes);
        }
    }


    // Compares the instance to another based on distance.
    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        /// Index of a training item.
        public int Index;
        /// Distance to the unknown point.
        public double Distance;
        /// Compares this instance to another based on distance.
        public int CompareTo(IndexAndDistance other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}
