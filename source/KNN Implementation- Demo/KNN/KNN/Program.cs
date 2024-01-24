using System;
namespace KNNClassifier_GlobalVariable
{
    public class Knnprogram
    {
        //interface IComparable<IndexAndDistance>;
        static void Main(string[] args)
        {
            /// loading data
            Console.WriteLine("Begin with KNN Classification");
            double[][] trainData = Loaddata();

            //// Initializing num of features and classes
            int numFeatures = 2;
            int numClasses = 3;
            double[] unknown = new double[] { 5.25, 3.75 };
            Console.WriteLine("Predictor values: 5.25 3.75 ");

            /// Applying classifier for K=1
            int k = 1;
            Console.WriteLine("With k = 1");
            int predicted = Classifier(unknown, trainData, numClasses, k);
            Console.WriteLine("Predicted class = " + predicted);

            /// Applying classifier for K=1
            k = 4;
            Console.WriteLine("With k = 4");
            predicted = Classifier(unknown, trainData, numClasses, k);
            Console.WriteLine("Predicted class = " + predicted);
            Console.WriteLine("End kNN ");
            Console.ReadLine();




        }

        static double[][] Loaddata()
        {
            double[][] data = new double[5][];
            data[0] = new double[] { 2.0, 4.0, 1 };
            data[1] = new double[] { 3.0, 4.0, 1 };
            data[2] = new double[] { 4.0, 2.0, 2 };
            data[3] = new double[] { 5.0, 3.0, 2 };
            data[4] = new double[] { 1.0, 4.0, 0 };
            return data;
        }

        static int Classifier(double[] unknown, double[][] trainData, int numClasses, int k)
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
            int result = Vote(info, trainData, numClasses, k);
            return result;

        }

        static double Distance(double[] unknown, double[] data)
        {
            double sum = 0.0;
            for (int i = 0; i < unknown.Length; ++i)
                sum += (unknown[i] - data[i]) * (unknown[i] - data[i]);
            return Math.Sqrt(sum);
        }

        static int Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
        {
            int[] votes = new int[numClasses];  // One cell per class
            for (int i = 0; i < k; ++i)
            {       // Just first k
                int idx = info[i].idx;            // Which train item
                int c = (int)trainData[idx][2];   // Class in last cell
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
