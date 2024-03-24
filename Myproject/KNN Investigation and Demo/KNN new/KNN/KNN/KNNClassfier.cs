using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
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
}
