﻿//Global Variable
using NeoCortexEntities.NeuroVisualizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


/*
 The Neocortex API generates sequences of numbers categorized as 0 as Even,1 as Odd,and 2 as Neither odd nor eveb, crucial for dataset creation. It learns
 from a file using LearnDatafromthefile, then splits the data 70-30 for training and testing using SplitData. The Classifier model is trained on 70% of 
 the data to discern patterns, while 30% is reserved for performance evaluation. Testing employs the K-Nearest Neighbors Classifier, predicting labels using
 Classifier method. Accuracy is assessed with CalculateAccuracy, comparing predicted and actual labels.
    
 For an Example:

 we have Sample Data in a Dataset which we split in training and testing data

 training data = [
  {
    "SequenceName": "S1",
    "SequenceData": [8039, 8738, 9334, 9558, 9604, 9697, 9772, 9841, 9851, 9922, 9963, 10023, 10121, 10197, 10373, 10459, 10594, 10629, 10664, 11124]
  },
{
    "SequenceName": "S2",
    "SequenceData": [9051, 9075, 9133, 9178, 9365, 9448, 9481, 9599, 9635, 9740, 10032, 10224, 10281, 10762, 10778, 10934, 11143, 11306, 11494, 11763]
  },
{
    "SequenceName": "S3",
    "SequenceData": [10808, 10834, 11053, 11085, 11434, 11471, 11479, 11553, 11597, 11634, 11720, 11743, 11766, 11812, 11872, 11897, 11909, 12094, 12332, 12504]
  }, ...
]


 testing Data = [
{
    "SequenceName": "S1",
    "SequenceData": [7665, 8260, 8304, 8495, 9285, 9366, 9388, 9603, 9641, 9707, 9774, 9819, 9837, 10020, 10096, 10149, 10263, 10313, 10873, 10914]
  }
]


 Here's the verdict: The model has predicted the testing data as Class S1, representing sequence S1 SDR's closet to testing data SDR'S.

 The output includes the label class of the testing data and the accuracy of the model. 

*/

namespace KNNImplementation
{
    /// <summary>
    /// KNN class is designed to perform classification tasks on sequences derived from the SDR (Sparse Distributed Representation) dataset
    /// </summary>

    public class KNNClassifier : IClassifier
    {

        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="testFeature">Feature of test Data.</param>
        /// <param name="trainFeature">Feature of train Data.</param>
        /// <returns>The Euclidean distance between the two Feature of training data and testing data.</returns>
        public double CalculateEuclideanDistance(List<double> testFeature, List<double> trainFeature)
        {
            if (testFeature == null || trainFeature == null)
                throw new ArgumentNullException("Both testData and trainData must not be null.");

            if (testFeature.Count != trainFeature.Count)
                throw new ArgumentException("testData and trainData must have the same length.");

            double sumOfSquaredDifferences = 0.0;

            for (int i = 0; i < testFeature.Count; ++i)
            {
                double difference = testFeature[i] - trainFeature[i];
                sumOfSquaredDifferences += difference * difference;
            }

            return Math.Sqrt(sumOfSquaredDifferences);
        }

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="nearestNeighbors">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="trainingLabels">The training label contain the class labels of training data.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>

        private int Vote(IndexAndDistance[] nearestNeighbors, List<string> trainingLabels, int k)
        {
            Dictionary<string, int> votes = new Dictionary<string, int>();

            foreach (string label in trainingLabels)
            {
                votes[label] = 0;
            }

            for (int i = 0; i < k; ++i)
            {
                string neighborLabel = trainingLabels[nearestNeighbors[i].idx];
                votes[neighborLabel]++;
            }

            // Find the class label with the most votes
            string classWithMostVotes = votes.OrderByDescending(pair => pair.Value).First().Key;
            Debug.WriteLine($"  Predicted class for test data: {(classWithMostVotes == "S1" ? "Even" : (classWithMostVotes == "S2" ? "Odd" : (classWithMostVotes == "S3" ? "Neither Odd nor Even" : "Unknown")))}");
            return trainingLabels.IndexOf(classWithMostVotes);
        }

        /// <summary>
        /// Classifies the unknown SDR based on the k-nearest neighbors in the training data using the KNN algorithm.
        /// </summary>
        /// <param name="testingFeatures">The Featurees of testing data containing testing SDR value to be classified.</param>
        /// <param name="trainingFeatures">The Features of training data containing training SDRs value.</param>
        /// <param name="trainingLabels">The training label contain the class labels of training data.</param>
        /// <param name="k">The number of nearest neighbors to consider in the classification.</param>
        /// <returns> The list of predicted labels for the testing features.</returns>

        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k)
        {
            List<string> predictedLabels = new List<string>();

            foreach (var testFeature in testingFeatures)
            {
                IndexAndDistance[] nearestNeighbors = new IndexAndDistance[trainingFeatures.Count];
                for (int i = 0; i < trainingFeatures.Count; i++)
                {
                    double distance = CalculateEuclideanDistance(testFeature, trainingFeatures[i]);
                    nearestNeighbors[i] = new IndexAndDistance { idx = i, dist = distance };
                }


                // Sort distances
                Array.Sort(nearestNeighbors);

                // Display information for the k-nearest items
                Debug.WriteLine("   Nearest Features    /    Euclidean Distance      /     Class label   ");
                Debug.WriteLine("   ==========================================   ");

                for (int i = 0; i < k; i++)
                {
                    int nearestIndex = nearestNeighbors[i].idx;
                    double nearestDistance = nearestNeighbors[i].dist;
                    string nearestClass = trainingLabels[nearestIndex];
                    Debug.WriteLine($"( {trainingFeatures[nearestIndex][0]}, {trainingFeatures[nearestIndex][1]} )  :  {nearestDistance}        {nearestClass}");
                }

                // Vote for the class based on the top k nearest neighbors
                int result = Vote(nearestNeighbors, trainingLabels, k);
                predictedLabels.Add(trainingLabels[result]);
            }
            return predictedLabels;
        }

        /// <summary>
        /// Finding Accuracy of KNN CLassifue
        /// </summary>
        /// <param name="predictedLabels"></param>
        /// <param name="actualLabels"></param>
        /// <returns></returns> Retyrn the accuracy in Percentage 

        public double CalculateAccuracy(List<string> predictedLabels, List<string> actualLabels)
        {
            int correctPredictions = predictedLabels.Where((predictedLabel, index) => predictedLabel == actualLabels[index]).Count();
            double accuracy = (double)correctPredictions / predictedLabels.Count * 100;
            return accuracy;
        }

        public List<SequenceDataEntry>? LoadDataset(string jsonFilePath)
        { 
            throw new NotImplementedException();
        
        }


    }


    /// <summary>
    /// Compares the instance to another based on distance
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
        public int CompareTo(IndexAndDistance other)=> dist.CompareTo(other.dist);
        
    }

    /// <summary>
    /// Represents an entry in the dataset, containing a sequence name and its associated data.
    /// </summary>
    public class SequenceDataEntry
    {
        // The name of the sequence.
        public required string SequenceName { get; set; }
        // The data associated with the sequence.
        public required List<double> SequenceData { get; set; }
    }

    /// <summary>
    /// Class to lean from the JSON file Dataset and split the dataset into training and testing data with respect to spliting ratio.
    /// </summary>
    public class Classfierleaning : IClassifier
    {

        /// <summary>
        /// Loading Dataset from the JSON file 
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public List<SequenceDataEntry> LoadDataset(string jsonFilePath)
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<SequenceDataEntry>>(jsonData);
        }

        /// <summary>
        /// Spliting the dataset in training dataset and testing dataset based on Features and label or value and key
        /// </summary>
        /// <param name="sequenceDataList"></param>
        /// <param name="trainingFeatures"></param>
        /// <param name="trainingLabels"></param>
        /// <param name="testingFeatures"></param>
        /// <param name="testingLabels"></param>
        public static void SplitDataset(List<SequenceDataEntry> sequenceDataList, out List<List<double>> trainingFeatures, out List<string> trainingLabels, out List<List<double>> testingFeatures, out List<string> testingLabels, double Splitingratio)
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

                if (rand.NextDouble() < Splitingratio) // spliting ratio for training
                {
                    trainingFeatures.Add(new List<double>(features));
                    trainingLabels.Add(label);
                }
                else // remaining out of 1 is for testing
                {
                    testingFeatures.Add(new List<double>(features));
                    testingLabels.Add(label);
                }
            }


        }

        public List<string> Classifier(List<List<double>> testingFeatures, List<List<double>> trainingFeatures, List<string> trainingLabels, int k)
        {
            throw new NotImplementedException();
        }

        double IClassifier.CalculateEuclideanDistance(List<double> testFeature, List<double> trainFeature)
        {
            throw new NotImplementedException();
        }
    }



}