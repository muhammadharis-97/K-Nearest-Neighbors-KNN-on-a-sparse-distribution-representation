//Global Variable
using KNNImplementation;
using NeoCortexApi;
using NeoCortexApi.Encoders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using static NeoCortexApiSample.MultiSequenceLearning;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NeoCortexApiSample
{
    class Program
    {
        /// <summary>
        /// This sample shows a typical experiment code for SP and TM.
        /// You must start this code in debugger to follow the trace.
        /// and TM.
        /// </summary>
        /// <param name="args"></param>

        static void Main(string[] args)
        {
            Console.WriteLine("Begin with KNN Classification");



            /// loading dataset SDR value and Sequence list reference name
            /// 
            double[][] trainData = SDRdataset();
            double[] testData = new double[] { 240, 242, 257, 266, 273, 313, 321, 335, 338, 344, 364, 386, 389, 395, 398, 411, 427, 430, 437, 444 };
            int numofclass = 9;

            int K = 1;

            KNNClassifier kNN = new KNNClassifier(trainData);


            int sequence = kNN.Classifier(testData, trainData, numofclass, K);

            Console.WriteLine(" Value of K is equal to 1");
            Console.WriteLine("Predicted class ");
            Console.WriteLine(sequence);


            K = 2;
            Console.WriteLine(" Value of K is equal to 2");
            sequence = kNN.Classifier(testData, trainData, numofclass, K);

            Console.WriteLine("Predicted class ");
            Console.WriteLine(sequence);



            K = 3;
            Console.WriteLine(" Value of K is equal to 3");

            sequence = kNN.Classifier(testData, trainData, numofclass, K);


            Console.WriteLine("Predicted class ");
            Console.WriteLine(sequence);





            //
            // Starts experiment that demonstrates how to learn spatial patterns.
            //SpatialPatternLearning experiment = new SpatialPatternLearning();
            //experiment.Run();

            //
            // Starts experiment that demonstrates how to learn spatial patterns.
            //SequenceLearning experiment = new SequenceLearning();
            //experiment.Run();

            //GridCellSamples gridCells = new GridCellSamples();
            //gridCells.Run();

            //RunMultiSimpleSequenceLearningExperiment();


            //RunMultiSequenceLearningExperiment();
        }

        /// <summary>
        ///  Dataset of SDR values extracted from the sequence of even and odd set of numbers
        /// </summary>
        /// <returns></returns>

        static double[][] SDRdataset()
        {

            double[][] data = new double[10][];
            data[0] = new double[] { 7, 18, 24, 29, 43, 46, 59, 62, 65, 70, 95, 102, 118, 146, 148, 155, 953, 960, 982, 1012, 0 };
            data[1] = new double[] { 25, 31, 44, 48, 49, 52, 65, 71, 87, 88, 90, 95, 100, 110, 111, 115, 128, 137, 176, 188, 1 };
            data[2] = new double[] { 118, 123, 127, 156, 160, 201, 212, 218, 219, 225, 229, 232, 235, 236, 242, 243, 253, 286, 310, 340, 2 };
            data[3] = new double[] { 240, 242, 257, 266, 273, 313, 321, 335, 338, 344, 364, 386, 389, 395, 398, 411, 427, 430, 437, 444, 3 };
            data[4] = new double[] { 302, 314, 324, 327, 340, 345, 350, 362, 390, 400, 425, 431, 435, 442, 446, 466, 498, 499, 506, 518, 4 };
            data[5] = new double[] { 393, 405, 428, 429, 433, 434, 436, 445, 454, 457, 460, 471, 504, 540, 568, 585, 586, 615, 616, 624, 5 };
            data[6] = new double[] { 483, 487, 500, 509, 510, 515, 519, 529, 533, 556, 577, 594, 597, 601, 651, 657, 665, 667, 668, 726, 6 };
            data[7] = new double[] { 579, 587, 595, 607, 617, 633, 635, 637, 641, 654, 661, 664, 677, 701, 711, 725, 735, 755, 788, 814, 7 };
            data[8] = new double[] { 676, 691, 700, 707, 723, 732, 738, 748, 753, 758, 762, 767, 778, 786, 799, 806, 825, 848, 854, 916, 8 };
            data[9] = new double[] { 772, 779, 780, 800, 810, 811, 812, 826, 830, 853, 861, 878, 886, 889, 891, 897, 954, 957, 960, 1007, 9 };
            return data;
        }


        private static void RunMultiSimpleSequenceLearningExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            // This sequence has a difference of 3 between each number. First Number starting from 1
            sequences.Add("S1", new List<double>(new double[] { 0, 2, 4, 6, 8, 10, 12, 14 }));
            // This sequence has a difference of 5 between each number. First Number starting from 3
            sequences.Add("S2", new List<double>(new double[] { 1, 3, 5, 7, 9, 11, 13, 15 }));
            //The sequence is continued by subtracting 2 each time. First Number Starting from 25 
            //sequences.Add("S3", new List<double>(new double[] { 25, 23, 21, 19, 17, 15, 13, 11, 9, 7}));
            //The sequence is even Number. First Number Starting from 0
            // sequences.Add("S4", new List<double>(new double[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18}));
            //The sequence is odd Number. First Number Starting from 0
            // sequences.Add("S5", new List<double>(new double[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19}));
            //This Sequence is of Triangular Number, generated from a pattern of dots that form a triangle.
            // sequences.Add("S6", new List<double>(new double[] { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55}));
            //They are the squares of whole numbers
            // sequences.Add("S7", new List<double>(new double[] { 0, 1, 4, 9, 16, 25, 36, 49, 64, 81}));
            // The sequence is Fibonacci Sequence, found by adding the two numbers before it together.
            // sequences.Add("S8", new List<double>(new double[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34}));


            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);
        }


        /// <summary>
        /// This example demonstrates how to learn two sequences and how to use the prediction mechanism.
        /// First, two sequences are learned.
        /// Second, three short sequences with three elements each are created und used for prediction. The predictor used by experiment privides to the HTM every element of every predicting sequence.
        /// The predictor tries to predict the next element.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment()
        {
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            //sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 0.0, 2.0, 3.0, 4.0, 5.0, 6.0, 5.0, 4.0, 3.0, 7.0, 1.0, 9.0, 12.0, 11.0, 12.0, 13.0, 14.0, 11.0, 12.0, 14.0, 5.0, 7.0, 6.0, 9.0, 3.0, 4.0, 3.0, 4.0, 3.0, 4.0 }));
            //sequences.Add("S2", new List<double>(new double[] { 0.8, 2.0, 0.0, 3.0, 3.0, 4.0, 5.0, 6.0, 5.0, 7.0, 2.0, 7.0, 1.0, 9.0, 11.0, 11.0, 10.0, 13.0, 14.0, 11.0, 7.0, 6.0, 5.0, 7.0, 6.0, 5.0, 3.0, 2.0, 3.0, 4.0, 3.0, 4.0 }));

            // sequences.Add("S1", new List<double>(new double[] { 0.0, 1.0, 2.0, 3.0, 4.0, 2.0, 5.0, }));
            // sequences.Add("S2", new List<double>(new double[] { 8.0, 1.0, 2.0, 9.0, 10.0, 7.0, 11.00 }));

            sequences.Add("S1", new List<double>(new double[] { 2, 4, 7, 10, 13, 16, 19, 22, 25, 28 }));
            // This sequence has a difference of 5 between each number. First Number starting from 3
            sequences.Add("S2", new List<double>(new double[] { 3, 8, 13, 18, 23, 28, 33, 38, 43, 48 }));
            //The sequence is continued by subtracting 2 each time. First Number Starting from 25 
            // sequences.Add("S3", new List<double>(new double[] { 25, 23, 21, 19, 17, 15, 13, 11, 9, 7 }));

            //
            // Prototype for building the prediction engine.
            MultiSequenceLearning experiment = new MultiSequenceLearning();
            var predictor = experiment.Run(sequences);

            //
            // These list are used to see how the prediction works.
            // Predictor is traversing the list element by element. 
            // By providing more elements to the prediction, the predictor delivers more precise result.
            // var list1 = new double[] { 1.0, 2.0, 3.0, 4.0, 2.0, 5.0 };
            // var list2 = new double[] { 2.0, 3.0, 4.0 };
            // var list3 = new double[] { 8.0, 1.0, 2.0 };

            //predictor.Reset();
            //PredictNextElement(predictor, list1);

            //predictor.Reset();
            //PredictNextElement(predictor, list2);

            // predictor.Reset();
            // PredictNextElement(predictor, list3);
        }

        private static void PredictNextElement(Predictor predictor, double[] list)
        {
            Debug.WriteLine("------------------------------");

            foreach (var item in list)
            {
                var res = predictor.Predict(item);

                if (res.Count > 0)
                {
                    foreach (var pred in res)
                    {
                        Debug.WriteLine($"{pred.PredictedInput} - {pred.Similarity}");
                    }

                    var tokens = res.First().PredictedInput.Split('_');
                    var tokens2 = res.First().PredictedInput.Split('-');
                    Debug.WriteLine($"Predicted Sequence: {tokens[0]}, predicted next element {tokens2.Last()}");
                }
                else
                    Debug.WriteLine("Nothing predicted :(");
            }

            Debug.WriteLine("------------------------------");
        }
    }
}

