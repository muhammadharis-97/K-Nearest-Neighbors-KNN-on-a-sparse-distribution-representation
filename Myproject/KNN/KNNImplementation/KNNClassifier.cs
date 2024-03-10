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
    /// 
    /// </summary>

    public class KNNClassifier : IClassifier
    {
        /// <summary>
        /// Calculates the Euclidean distance between two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The Euclidean distance between the two vectors.</returns>
        private double Distance(double[] vector1, double[] vector2)
        {
            double sum = 0.0;

            // Calculate the sum of squared differences for each dimension
            for (int i = 0; i < vector1.Length; ++i)
            {
                double difference = vector1[i] - vector2[i];
                sum += difference * difference;
            }

            // Return the square root of the sum to get the Euclidean distance
            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Determines the class label by majority voting among the k nearest neighbors.
        /// </summary>
        /// <param name="info">An array of IndexAndDistance objects representing the k nearest neighbors.</param>
        /// <param name="trainData">The training data containing class labels.</param>
        /// <param name="numClasses">The total number of classes.</param>
        /// <param name="k">The number of nearest neighbors to consider.</param>
        /// <returns>The class label with the most votes among the nearest neighbors.</returns>

        static int Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
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
                int c = (int)trainData[idx][20];

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

            // Sort the SDRs in the training data by distance to the unknown SDR
            Array.Sort(info);

            // Determine the class label for the unknown SDR based on the k-nearest neighbors
            int result = Vote(info, Sdrdata, numofclass, k);

            return result;
        }

        // GetTestDatasets method to load test datasets from a file
        public static double[][] GetTestDatasets()
        {
            // Call the GetTestDataset method to read datasets from the file
            double[][] testdataset = GetTestDataset("/Users/zakaahmedchishti/Documents/GitHub/se-cloud-2023-2024/MyWork_Exerices/neocortexapi/My Project:KNN /New/New/sdr_data.txt");

            // Print each dataset to the console
            foreach (var dataset in testdataset)
            {
                // Join the elements of the dataset array with commas and print to the console
                string.Join(", ", dataset);
            }
            // Return the loaded test datasets
            return testdataset;
        }


        // GetTestDataset method to read datasets from a file
        public static double[][] GetTestDataset(string filePath)
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

        // ReadSDRDataFromFile method to load SDR data from a file
        public static double[][] ReadSDRDataFromFile()
        {
            // Call the ReadSDRDataFromFileMethod to read SDR data from the file
            double[][] sdrData = ReadSDRDataFromFileMethod("/Users/zakaahmedchishti/Documents/GitHub/se-cloud-2023-2024/MyWork_Exerices/neocortexapi/My Project:KNN /New/New/sdr_data.txt");

            // Print each dataset to the console
            foreach (var dataset in sdrData)
            {
                // Join the elements of the dataset array with commas and print to the console
                string.Join(", ", dataset);
            }
            // Return the loaded SDR data
            return sdrData;
        }


        // ReadSDRDataFromFileMethod to read SDR data from a file
        public static double[][] ReadSDRDataFromFileMethod(string filePath)
        {
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Create a 2D array to store the SDR data
                double[][] sdrData = new double[lines.Length][];

                // Iterate through each line in the file
                for (int i = 0; i < lines.Length; i++)
                {
                    // Split the line by commas to get individual values
                    string[] values = lines[i].Split(',');

                    // Create an array to store the values for the current dataset
                    sdrData[i] = new double[values.Length];

                    // Iterate through each value in the line
                    for (int j = 0; j < values.Length; j++)
                    {
                        // Parse the value to double and store it in the SDR data array
                        if (!double.TryParse(values[j], out sdrData[i][j]))
                        {
                            // Throw a format exception if parsing fails
                            throw new FormatException($"Failed to parse value at line {i + 1}, position {j + 1}");
                        }
                    }
                }
                return sdrData;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file reading or parsing
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Return null to indicate failure
            }
            // Return the loaded SDR data
        }


        /// <summary>
        /// Compares this instance to another based on distance.
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
}