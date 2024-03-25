using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    /// <summary>
    /// KNN class is designed to perform classification tasks on sequences derived from the SDR (Sparse Distributed Representation) dataset
    /// </summary>
    public class KNNClassifier
    {
        //// <summary>
        /// Performs classification on the testing features using k-Nearest Neighbors algorithm.
        /// </summary>
        /// <param name="testingFeatures">The list of features for which predictions are to be made.</param>
        /// <param name="trainingFeatures">The list of features used for training the classifier.</param>
        /// <param name="trainingLabels">The list of labels corresponding to the training features.</param>
        /// <param name="k">The number of nearest neighbors to consider for classification.</param>
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
                int result = Vote(nearestNeighbors, trainingLabels, trainingLabels.Count, k);
                predictedLabels.Add(trainingLabels[result]);
            }

            return predictedLabels;
        }

        /// <summary>
        /// Calculates the accuracy of the predicted labels compared to the actual labels.
        /// </summary>
        /// <param name="predictedLabels">The list of predicted labels.</param>
        /// <param name="actualLabels">The list of actual labels.</param>
        /// <returns>The accuracy percentage.</returns>
        /// 
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

        private int Vote(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int numOfClasses, int k)
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
