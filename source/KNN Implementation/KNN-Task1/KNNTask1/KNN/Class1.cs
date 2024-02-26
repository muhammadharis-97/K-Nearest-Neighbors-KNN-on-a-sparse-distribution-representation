using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    public class KNN_GlobalVariable
    {

        public int Classifier(double[] unknown, double[][] trainData, int numClasses, int k)
        {
            int n = trainData.Length;
            IndexAndDistance[] info = new IndexAndDistance[n];
            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(unknown, trainData[i]);
                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;

            }
            Array.Sort(info);  // sort by distance

       
           

            int result = Vote(info, trainData, numClasses, k);

            return result;

        }


        // This method calculates the Euclidean distance between two points in n-dimensional space.
        static double Distance(double[] unknown, double[] data)
        {
            // Initialize a variable to store the sum of squared differences
            double sum = 0.0;

            // Loop through each dimension (each element in the arrays)
            for (int i = 0; i < unknown.Length; ++i)
            {
                // Calculate the squared difference between corresponding elements
                double differenceSquared = (unknown[i] - data[i]) * (unknown[i] - data[i]);

                // Add the squared difference to the sum
                sum += differenceSquared;
            }

            // Take the square root of the sum to get the Euclidean distance
            double distance = Math.Sqrt(sum);

            // Return the calculated distance
            return distance;
        }

        static int Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
        {
            int[] votes = new int[numClasses];  // One cell per class
            for (int i = 0; i < k; ++i)
            {       // Just first k
                int idx = info[i].idx;            // Which train item
                int c = (int)trainData[idx][20];   // Class in last cell
                ++votes[c];
            }
            int mostVotes = 0;
            int classWithMostVotes = 0;
            for (int j = 0; j < numClasses; ++j)
            {
                if (votes[j] > mostVotes)
                {
                    mostVotes = votes[j];
                    classWithMostVotes = j;
                }
            }
            return classWithMostVotes;
        }


    }
    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;  // index of a training item
        public double dist;  // distance to unknown
        public int CompareTo(IndexAndDistance other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return +1;
            else return 0;
        }

    }

}

