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

        //Constructor for Unit Test
        public KNNClassifier(double[][] sdrdata)
        {
            Console.WriteLine("Unit Test started") ;
            double[] unknownSDR = new double[] { 579, 587, 595, 607, 617, 633, 635, 637, 641, 654, 661, 664, 677, 701, 711, 725, 735, 755, 788, 814 };
            int numofclass = 9;


            //Distance
            int n = sdrdata.Length;
            IndexAndDistance[] info = new IndexAndDistance[n];
            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();

                double dist = Distance(unknownSDR, sdrdata[i]);

                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;

                Console.WriteLine(dist);
            }
            Array.Sort(info);





            //Classifier
            int K = 1;
            int sequence = Classifier(unknownSDR, sdrdata, numofclass, K);
            Console.WriteLine(" Value of K is equal to 1");
            Console.WriteLine("Predicted class ");
            Console.WriteLine(sequence);
            Console.WriteLine("Unit Test Ended");


        }



        /// <summary>
        /// Distance Calculation between Known SDR with unknown SDR to see weather the SDR point is nearest to unknown SDR 
        /// </summary>
        /// <param name="unknownSDR"></param>
        /// <param name="sdrdata"></param>
        /// <returns></returns>

        static double Distance(double[] unknownSDR, double[] Sdrdata)
        {
            double sum = 0.0;

            for (int i = 0; i < unknownSDR.Length; ++i)
            {
                double difference = unknownSDR[i] - Sdrdata[i];
                
                double squaredDifference = difference * difference;
            
                sum += squaredDifference;
            }

            double euclideanDistance = Math.Sqrt(sum);

            return euclideanDistance;
        }

        /// <summary>
        /// The Method is for classify test SDR value based on voting, Voting between no of classes classify from the train SDR.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="trainData"></param>
        /// <param name="numClasses"></param>
        /// <param name="k"></param>
        /// <returns></returns>
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
        ///  Classification Methond using SDRValue in the form of training Data to the Classifier Model
        /// For Classification UnknownSDR value of a paricualar squence is used to classify, to see wheater these SDR value belong or near to any of SDR vlaues in Dataset
        /// the number of classification type are defined as numofclass
        /// While K is the tunner to see how many Neareast Neibour SDr are near to unknownSDR values and what would be the optimial value 
        /// </summary>
        /// <param name="unknownSDR"></param>
        /// <param name="Sdrdata"></param>
        /// <param name="numofclass"></param>
        /// <param name="k"></param>
        /// <returns></returns>

        public int Classifier(double[] unknownSDR, double[][] Sdrdata, int numofclass, int k)
        {
            int n = Sdrdata.Length;
            
            IndexAndDistance[] info = new IndexAndDistance[n];
            
            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();
                
                double dist = Distance(unknownSDR, Sdrdata[i]);
                
                curr.idx = i;
                
                curr.dist = dist;
                
                info[i] = curr;

            }
            Array.Sort(info);  // sort by distance


            int result = Vote(info, Sdrdata, numofclass, k);

            return result;

        }

        /// <summary>
        /// Comparing Class to compare index of Sdr training data with the distance computed between with test SDR and Train SDR at given Index
        /// </summary>

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
}
