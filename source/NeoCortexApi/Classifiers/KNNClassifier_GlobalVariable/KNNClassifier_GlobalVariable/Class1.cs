using System;
namespace KNNClassifier_GlobalVariable
{
    public class Knnprogram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin with KNN Classification")
            double[][] trainData = Loaddata();
        
        
        
        
        }

        static double[][] Loaddata()
        {
            double[][] data = new double[5][];
            data[0] = new double [] { 2.0, 4.0, 1};
            data[1] = new double[] { 3.0, 4.0, 1 };
            data[2] = new double[] { 4.0, 2.0, 1 };
            data[3] = new double[] { 5.0, 3.0, 1 };
            data[4] = new double[] { 1.0, 4.0, 1 };
            return data;
        }
    }
}
