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
        ///
        /// </summary>
        /// <param name="unknown"></param>
        /// <param name="data"></param>
        /// <returns></returns>

        static double Distance(double[] unknown, double[] data)
        {
            double sum = 0.0;
            for (int i = 0; i < unknown.Length; ++i)
                sum += (unknown[i] - data[i]) * (unknown[i] - data[i]);
            return Math.Sqrt(sum);
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
