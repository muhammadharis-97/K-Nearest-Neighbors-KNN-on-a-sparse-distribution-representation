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

    // KNN Algorithm 
    public class KNNClassifier : IClassifier
    {

        /// <summary>
        /// Distance Calculation between Known SDR with unknown SDR to see weather the SDR is nearest to unknown SDR 
        /// </summary>
        /// <param name="unknownSDR"></param>
        /// <param name="sdrdata"></param>
        /// <returns></returns>

        static double Distance(double[] unknownSDR, double[] sdrdata)
        {
            double sum = 0.0;

            for (int i = 0; i < unknownSDR.Length; ++i)
            {
                double difference = unknownSDR[i] - sdrdata[i];
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


        /// <summary>
        ///  Classification Methond using SDRValue in the form of training Data to the Classifier Model
        /// For Classification UnknownSDR value of a paricualar squence is used to classify, to see wheater these SDR value belong or near to any of SDR vlaues in Dataset
        /// the number of classification type are defined as numofclass
        /// While K is the tunner to see how many Neareast Neibour SDr are near to unknownSDR values
        /// </summary>
        /// <param name="unknownSDR"></param>
        /// <param name="Sdrdata"></param>
        /// <param name="numofclass"></param>
        /// <param name="k"></param>
        /// <returns></returns>

        public int Classifier(double[] unknownSDR, double[][] Sdrdata, int numofclass, int k)
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




    }
}
