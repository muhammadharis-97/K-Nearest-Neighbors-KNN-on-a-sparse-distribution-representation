using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace KNNImplementation
{
    /// <summary>
    /// KNN class is designed to perform classification tasks on sequences derived from the SDR (Sparse Distributed Representation) dataset
    /// </summary>
    public class KNNClassifier
    {
        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="testData">The test data vector.</param>
        /// <param name="trainData">The training data vector.</param>
        /// <returns>The Euclidean distance between the two vectors.</returns>
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

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="info">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="trainData">The training data containing class labels.</param>
        /// <param name="numofclass">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>
        public int Vote(IndexAndDistance[] info, Dictionary<string, List<double>> trainData, int numofclass, int k)
        {
            int[] votes = new int[numofclass];

            for (int i = 0; i < k; ++i)
            {
                int idx = info[i].idx;
                string key = trainData.Keys.ElementAt(idx);
                int c = (int)trainData[key].Last();
                ++votes[c];
            }

            int classWithMostVotes = Array.IndexOf(votes, votes.Max());
            return classWithMostVotes;
        }

        /// <summary>
        /// Classifies the unknown SDR based on the k-nearest neighbors in the training data using the KNN algorithm.
        /// </summary>
        /// <param name="testData">The testing data containing unknown SDR to be classified.</param>
        /// <param name="trainData">The training data containing known SDRs.</param>
        /// <param name="numofclass">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider in the classification.</param>
        /// <returns>The predicted class label for the unknown SDR.</returns>
        public int Classifier(Dictionary<string, List<double>> testData, Dictionary<string, List<double>> trainData, int numofclass, int k)
        {
            int n = trainData.Count;
            IndexAndDistance[] info = new IndexAndDistance[n];

            int index = 0;
            foreach (var trainItem in trainData)
            {
                double dist = CalculateEuclideanDistance(testData.Values.First(), trainItem.Value);
                info[index++] = new IndexAndDistance { idx = index, dist = dist };
            }

            Array.Sort(info);

            // Display information for the k-nearest items
            Debug.WriteLine("   Nearest     /    Distance      /     Class   ");
            Debug.WriteLine("   ==========================================   ");
            for (int i = 0; i < k; ++i)
            {
                string key = trainData.Keys.ElementAt(info[i].idx);
                int c = (int)trainData[key].Last();
                string dist = info[i].dist.ToString("F3");
                Debug.WriteLine($"( {key} )  :  {dist}        {c}");
            }

            int result = Vote(info, trainData, numofclass, k);
            return result;
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
            public int idx;

            /// <summary>
            /// Distance to the unknown point.
            /// </summary>
            public double dist;

            /// <summary>
            /// Compares this instance to another based on distance.
            /// </summary>
            public int CompareTo(IndexAndDistance other)
            {
                return dist.CompareTo(other.dist);
            }
    }
    
}
