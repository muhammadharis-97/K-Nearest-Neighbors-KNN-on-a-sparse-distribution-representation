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

    /// KNN Algorithm 
    public class KNNClassifier : IClassifier
    {
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
