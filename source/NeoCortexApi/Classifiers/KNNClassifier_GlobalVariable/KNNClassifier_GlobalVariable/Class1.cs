using System;
namespace KNNClassifier_GlobalVariable
{
    public class Knnprogram
    {
        static void Main(string[] args)
        {
            /// loading data
            Console.WriteLine("Begin with KNN Classification")
            double[][] trainData = Loaddata();

            //// Initializing num of features and classes
            int numFeatures = 2;
            int numClasses = 3;
            double[] unknown = new double[] { 5.25, 1.75 };
            Console.WriteLine("Predictor values: 5.25 1.75 ");

            /// Applying classifier
            int k = 1;
            Console.WriteLine("With k = 1");
            int predicted = Classify(unknown, trainData, numClasses, k);
            Console.WriteLine("Predicted class = " + predicted);


        }

        static double[][] Loaddata()
        {
            double[][] data = new double[5][];
            data[0] = new double[] { 2.0, 4.0, 1 };
            data[1] = new double[] { 3.0, 4.0, 1 };
            data[2] = new double[] { 4.0, 2.0, 1 };
            data[3] = new double[] { 5.0, 3.0, 1 };
            data[4] = new double[] { 1.0, 4.0, 1 };
            return data;
        }

        static int Classify(double[] unknown, double[][] trainData, int numClasses, int k)
        {
            int n = trainData.Length;
            IndexAndDistance[] info = new IndexAndDistance[n];
            for (int i = 0; i < n; ++i)
            {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(unknown, trainData[i]);
                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;
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

}