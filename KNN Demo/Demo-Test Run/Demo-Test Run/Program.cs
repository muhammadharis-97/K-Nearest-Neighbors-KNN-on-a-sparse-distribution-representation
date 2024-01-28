using System;
namespace KNN
{
    class KNNProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin k-NN classification demo ");
            double[][] trainData = LoadData();
            int numFeatures = 2;
            int numClasses = 3;
            double[] unknown = new double[] { 5.25, 1.75 };
            Console.WriteLine("Predictor values: 5.25 1.75 ");
            int k = 1;
            Console.WriteLine("With k = 1");
            int predicted = Classify(unknown, trainData,
            numClasses, k);
            Console.WriteLine("Predicted class = " + predicted);
            k = 4;
            Console.WriteLine("With k = 4");
            predicted = Classify(unknown, trainData,
            numClasses, k);
            Console.WriteLine("Predicted class = " + predicted);
            Console.WriteLine("End k-NN demo ");
            Console.ReadLine();
        }
        static int Classify(double[] unknown,
        double[][] trainData, int numClasses, int k)
        { . . }
        static int Vote(IndexAndDistance[] info,
        double[][] trainData, int numClasses, int k)
        { . . }
        static double Distance(double[] unknown,
        double[] data)
        { . . }
        static double[][] LoadData() { . . }
    } // Program class
    public class IndexAndDistance : IComparable<IndexAndDistance>
    {
        public int idx;  // Index of a training item
        public double dist;  // To unknown
                             // Need to sort these to find k closest
        public int CompareTo(IndexAndDistance other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return +1;
            else return 0;
        }
    }
} // ns