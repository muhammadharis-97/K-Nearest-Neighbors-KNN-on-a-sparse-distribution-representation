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


    //// <summary>
    ///  KNN classifier to Classifiy the sequence from SDR dataset
    /// </summary>

    public class KNNClassifier : IClassifier
    {
        /// <summary>
        /// Calculates the Euclidean distance between unknownSDR and SDRdata.
        /// </summary>
        /// <param name="unknownSDR">The first vector.</param>
        /// <param name="SdrData">The second vector.</param>
        /// <returns>The Euclidean distance between the two vectors.</returns>
        private double Distance(double[] unknownSDR, double[] SdrData)
        {
            double sum = 0.0;

            // Calculate the sum of squared differences for each dimension
            for (int i = 0; i < unknownSDR.Length; ++i)
            {
                double difference = unknownSDR[i] - SdrData[i];
                sum += difference * difference;
            }

            // Return the square root of the sum to get the Euclidean distance
            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="info">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="SDRData">The training data containing class labels.</param>
        /// <param name="numClasses">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>

        static int Vote(IndexAndDistance[] info, double[][] SDRData, int numClasses, int k)
        {
            // Array to store the number of votes for each class
            int[] votes = new int[numClasses];

            // Initialize votes to zero for each class
            for (int i = 0; i < numClasses; ++i)
            {
                votes[i] = 0;
            }

            // Loop through the first k neighbors
            for (int i = 0; i < k; ++i)
            {
                // Get the index of the i-th neighbor
                int idx = info[i].idx;

                // Determine the class label of the i-th neighbor
                int c = (int)SDRData[idx][20];

                // Increment the vote count for the corresponding class
                ++votes[c];
            }

            // Variables to keep track of the class with the most votes
            int mostVotes = 0;
            int classWithMostVotes = 0;

            // Loop through each class
            for (int j = 0; j < numClasses; ++j)
            {
                // Check if the current class has more votes than the previous maximum
                if (votes[j] > mostVotes)
                {
                    // Update the mostVotes and classWithMostVotes variables
                    mostVotes = votes[j];
                    classWithMostVotes = j;
                }
            }

            // Return the class label with the most votes
            return classWithMostVotes;
        }

        /// <summary>
        /// Classifies the unknown SDR based on the k-nearest neighbors in the training data using the KNN algorithm.
        /// </summary>
        /// <param name="unknownSDR">The unknown SDR to be classified.</param>
        /// <param name="Sdrdata">The training data containing known SDRs.</param>
        /// <param name="numofclass">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider in the classification.</param>
        /// <returns>The predicted class label for the unknown SDR.</returns>

        public int Classifier(double[] unknownSDR, double[][] Sdrdata, int numofclass, int k)
        {
            int n = Sdrdata.Length;

            // Array to store the index and distance of each SDR in the training data
            IndexAndDistance[] info = new IndexAndDistance[n];

            // Compute the distance between the unknown SDR and each SDR in the training data
            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();

                double dist = Distance(unknownSDR, Sdrdata[i]);

                curr.idx = i;
                curr.dist = dist;

                info[i] = curr;
            }

            // sorting the training index-distance items that are stored
            Array.Sort(info);

            // Information for the k-nearest items is displayed
            Console.WriteLine("Nearest / Distance / Class");
            Console.WriteLine("==========================");
            for (int i = 0; i < k; ++i)
            {
                int c = (int)Sdrdata[info[i].idx][2];
                string dist = info[i].dist.ToString("F3");
                Console.WriteLine("( " + Sdrdata[info[i].idx][0] +
                  "," + Sdrdata[info[i].idx][1] + " )  :  " +
                  dist + "        " + c);
                Console.WriteLine();
            }

            // Determine the class label for the unknown SDR based on the k-nearest neighbors
            int result = Vote(info, Sdrdata, numofclass, k);

            return result;
        }

        /// <summary>
        /// GetTestDataset method to read datasets from a file
        /// </summary>
        /// <returns></returns>

        public static double[][] LoadDatafromthefile(string filePath)
        {
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Create a 2D array to store the datasets
                double[][] datasets = new double[lines.Length][];

                // Iterate through each line in the file
                for (int i = 0; i < lines.Length; i++)
                {
                    // Split the line by commas to get individual values
                    string[] values = lines[i].Split(',');

                    // Create an array to store the values for the current dataset
                    datasets[i] = new double[values.Length];

                    // Iterate through each value in the line
                    for (int j = 0; j < values.Length; j++)
                    {
                        // Parse the value to double and store it in the dataset array
                        if (!double.TryParse(values[j], out datasets[i][j]))
                        {
                            // Throw a format exception if parsing fails
                            throw new FormatException($"Failed to parse value at line {i + 1}, position {j + 1}");
                        }
                    }
                }

                // Return the loaded datasets
                return datasets;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file reading or parsing
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Return null to indicate failure
            }
        }


    }


    /// <summary>
    /// Compares the instance to another based on distance.
    /// </summary>
    /// <param name="other">The other IndexAndDistance instance to compare with.</param>
    /// <returns>
    /// A negative value if this instance has a smaller distance,
    /// a positive value if this instance has a larger distance,
    /// or zero if both instances have the same distance.
    /// </returns>
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