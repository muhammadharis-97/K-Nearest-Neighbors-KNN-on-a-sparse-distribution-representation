//Global Variable
using NeoCortexEntities.NeuroVisualizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace KNNImplementation
{


    /// <summary>
    /// KNN class is designed to perform classification tasks on sequences derived from the SDR (Sparse Distributed Representation) dataset
    /// </summary>

    public class KNNClassifier : IClassifier
    {
        /// <summary>
        /// Calculates the Euclidean distance between training Data and testing Data features.
        /// </summary>
        /// <param name="testData">The featuers of testing Data.</param>
        /// <param name="trainData">The featuers of testing Data.</param>
        /// <returns>The Euclidean distance between the two vectors.</returns>
        public double Distance(double[] testData, double[] trainData)
        {
            double sum = 0.0;

            for (int i = 0; i < testData.Length; ++i)
            {
                double difference = testData[i] - trainData[i];
                sum += difference * difference;
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="info">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="trainData">The training data containing class labels.</param>
        /// <param name="numofclass">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>

        static int Vote(IndexAndDistance[] info, double[][] trainData, int numofclass, int k)
        {
            int[] votes = new int[numofclass];

            for (int i = 0; i < numofclass; ++i)
            {
                votes[i] = 0;
            }

            for (int i = 0; i < k; ++i)
            {
                int idx = info[i].idx;
                int c = (int)trainData[idx][20];
                ++votes[c];
            }

            int mostVotes = 0;
            int classWithMostVotes = 0;

            // Loop through each class
            for (int j = 0; j < numofclass; ++j)
            {
                if (votes[j] > mostVotes)
                {
                 
                    mostVotes = votes[j];
                    classWithMostVotes = j;
                }
            }

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

        public  int Classifier(double[] testData, double[][] trainData, int numofclass, int k)
        {
            int n = trainData.Length;

            IndexAndDistance[] info = new IndexAndDistance[n];

            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();

                double dist = Distance(testData, trainData[i]);

                curr.idx = i;
                curr.dist = dist;

                info[i] = curr;
            }

            Array.Sort(info);

            Console.WriteLine();
            // Information for the k-nearest items is displayed
            Console.WriteLine("Nearest / Distance / Class");
            Console.WriteLine("==========================");
            for (int i = 0; i < k; ++i)
            {
                int c = (int)trainData[info[i].idx][20];
                string dist = info[i].dist.ToString("F3");
                Console.WriteLine("( " + trainData[info[i].idx][0] +
                  "," + trainData[info[i].idx][1] + " )  :  " +
                  dist + "        " + c);
                  Console.WriteLine();
            }

            int result = Vote(info, trainData, numofclass, k);

            return result;
        }

        /// <summary>
        /// Creating Method to learn data from a datset file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public double[][] LearnDatafromthefile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                double[][] datasets = new double[lines.Length][];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');

                    datasets[i] = new double[values.Length];

                    for (int j = 0; j < values.Length; j++)
                    {
                        if (!double.TryParse(values[j], out datasets[i][j]))
                        {
                            throw new FormatException($"Failed to parse value at line {i + 1}, position {j + 1}");
                        }
                    }
                }

               
                return datasets;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file reading or parsing
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }


        /// <summary>
        /// Finding Accuracy of KNN CLassifue
        /// </summary>
        /// <param name="predictedLabels"></param>
        /// <param name="actualLabels"></param>
        /// <returns></returns>

        public static double CalculateAccuracy(int[] predictedLabels, int[] actualLabels)
        {
            int correctPredictions = 0;
            int totalPredictions = predictedLabels.Length;

            for (int i = 0; i < totalPredictions; i++)
            {
                if (predictedLabels[i] == actualLabels[i])
                {
                    correctPredictions++;
                }
            }

            double accuracy = (double)correctPredictions / totalPredictions * 100;
            return accuracy;
        }


    }


    /// <summary>
    /// Compares the instance to another based on distance.
    /// </summary>
    /// <param name="other">The other IndexAndDistance instance to compare with.</param>
    /// <returns>
    
    public class IndexAndDistance : IComparable<IndexAndDistance>
        {
            /// Represents an index and its distance to an unknown point in a dataset, used for sorting.
            /// Index of a training item.
            public int idx;

            /// Distance to the unknown point.
            public double dist;

            /// Compares this instance to another based on distance.
            /// A negative value if this instance has a smaller distance,
            /// a positive value if this instance has a larger distance,
            /// or zero if both instances have the same distance.
            public int CompareTo(IndexAndDistance other)
            {
                if (this.dist < other.dist)
                    return -1;
                else if (this.dist > other.dist)
                    return 1;
                else
                    return 0;
            }
     }


    
}