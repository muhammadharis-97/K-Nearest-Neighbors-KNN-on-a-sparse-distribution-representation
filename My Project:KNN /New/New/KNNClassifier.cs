using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    public class KNNClassifier
    {
        public List<string> Test(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k)
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
                string result = Vote(nearestNeighbors, trainingLabels, trainingLabels.Count, k);

                Debug.WriteLine($"  Predicted class for test data: {(result != "Even" ? "Even" : (result == "Odd" ? "Odd" : (result == "Neither Odd nor Even" ? "Neither Odd nor Even" : "Unknown")))}");

            }

            return predictedLabels;
        }

        public double CalculateAccuracy(List<string> predictedLabels, List<string> actualLabels)
        {
            int correctPredictions = predictedLabels.Where((predictedLabel, index) => predictedLabel == actualLabels[index]).Count();
            double accuracy = (double)correctPredictions / predictedLabels.Count * 100;
            return accuracy;
        }

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="testData">The test data vector.</param>
        /// <param name="trainData">The training data vector.</param>
        /// <returns>The Euclidean distance between the two vectors.</returns>

        private double CalculateEuclideanDistance(List<double> testData, List<double> trainData)
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

            // Return the square root of the sum of squared differences
            return Math.Sqrt(sumOfSquaredDifferences);
        }

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="nearestNeighbors">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="trainingLabels">The training data containing class labels.</param>
        /// <param name="numOfClasses">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>



        private string Vote(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int numOfClasses, int k)
        {
            Dictionary<string, int> votes = new Dictionary<string, int>();

            // Initialize vote counts for each class label
            foreach (string label in trainingLabels)
            {
                votes[label] = 0;
            }

            // Count the votes for each class label based on the nearest neighbors
            for (int i = 0; i < k; ++i)
            {
                string neighborLabel = trainingLabels[nearestNeighbors[i].Index];
                votes[neighborLabel]++;
            }

            // Find the class label with the most votes
            string classWithMostVotes = votes.OrderByDescending(pair => pair.Value).First().Key;
            return classWithMostVotes;
        }

        private int GetMax(int[] arr)
        {
            int max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;
        }
    }


    /// <summary>
    /// Compares the instance to another based on distance.
    /// </summary>
    /// <param name="other">The other IndexAndDistance instance to compare with.</param>
    /// <returns>

    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        /// <summary>
        /// Index of a training item.
        /// </summary>
        public int Index;
        /// <summary>
        /// Distance to the unknown point.
        /// </summary>
        public double Distance;
        /// <summary>
        /// Compares this instance to another based on distance.
        /// </summary>
        public int CompareTo(IndexAndDistance other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}
