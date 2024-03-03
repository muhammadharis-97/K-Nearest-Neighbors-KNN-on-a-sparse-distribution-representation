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
            //Console.WriteLine("Begin with KNN Classification");

 /*         
            
            /// loading dataset SDR value and Sequence list reference name
            /// 
            double[][] trainData = SDRdataset();
            double[] testData = new double[] { 7816, 8229, 8674, 8707, 8946, 9400, 9493, 9543, 9562, 9658, 9695, 9752, 9821, 9912, 9978, 10209, 10300, 10607, 10658, 11084 };
            double[] testData1 = new double[] { 8673, 8700, 9814, 9998, 10129, 10193, 10315, 10487, 10618, 10661, 10732, 10772, 10936, 10955, 11079, 11285, 11392, 11458, 11475, 11532 };
            double[] testData2 = new double[] { 8624, 8915, 9491, 10222, 10399, 10580, 10631, 10711, 10821, 10863, 10888, 10916, 10962, 11004, 11074, 11129, 11233, 11257, 11689, 11703 };


            int numofclass = 3;
            int K = 4;
            
            
            
            Console.WriteLine(" Value of K is equal to 2");
            KNNClassifier kNN = new KNNClassifier(); 

            int sequence = kNN.Classifier(testData, trainData, numofclass, K);
            int sequence1 = kNN.Classifier(testData1, trainData, numofclass, K);
            int sequence2 = kNN.Classifier(testData2, trainData, numofclass, K);


            Console.WriteLine("Predicted class for first test data ");
            if (sequence == 0)
                Console.WriteLine("Even");
            else if (sequence == 1)
                Console.WriteLine("Odd");
            else
                Console.WriteLine("Neither Odd or Even");


            Console.WriteLine("Predicted class for second test data ");
            if (sequence1 == 0)
                Console.WriteLine("Even");
            else if (sequence == 1)
                Console.WriteLine("Odd");
            else
                Console.WriteLine("Neither Odd or Even");


            Console.WriteLine("Predicted class for third test data ");
            if (sequence2 == 0)
                Console.WriteLine("Even");
            else if (sequence == 1)
                Console.WriteLine("Odd");
            else
                Console.WriteLine("Neither Odd or Even");

*/
            //K = 2;
            //Console.WriteLine(" Value of K is equal to 2");
            //sequence = kNN.Classifier(testData, trainData, numofclass, K);

            //Console.WriteLine("Predicted class ");
            //Console.WriteLine(sequence);



            //K = 3;
            //Console.WriteLine(" Value of K is equal to 3");

            //sequence = kNN.Classifier(testData, trainData, numofclass, K);


            //Console.WriteLine("Predicted class ");
            //Console.WriteLine(sequence);





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

            // RunMultiSimpleSequenceLearningExperiment();


            //RunMultiSequenceLearningExperiment();
        }

        /// <summary>
        ///  Dataset of SDR values extracted from the sequence of even and odd set of numbers
        /// </summary>
        /// <returns></returns>
       
        static double[][] SDRdataset()
        {
            double[][] data = new double[3][];
            data[0] = new double[] { 8004, 8398, 8435, 8605, 9834, 10016, 10052, 10148, 10319, 10351, 10393, 10423, 10428, 10471, 10546, 10593, 10788, 10816, 10895, 10984, 0 };
            data[1] = new double[] { 9825, 10003, 10027, 10126, 10204, 10304, 10364, 10398, 10473, 10502, 10534, 10578, 10787, 10813, 10896, 10988, 11064, 11234, 11255, 11406, 1 };
            data[2] = new double[] { 10012, 10028, 10203, 10323, 10354, 10392, 10455, 10516, 10582, 10796, 10812, 10854, 10887, 11007, 11026, 11057, 11228, 11267, 11414, 11541, 2 };


            return data;
        }


            private static void RunMultiSimpleSequenceLearningExperiment()
        { 
           
           
            

            
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();
            /*
            // the sequence has prime number set containing value  2, 3, 5
            sequences.Add("S1", new List<double>(new double[] { 2, 3, 5}));

            // the sequence has not prime number set containing value  4, 6, 9 
            sequences.Add("S2", new List<double>(new double[] { 4, 6, 9 }));

            // the sequence has prime number set containing value 7, 11, 13 
            sequences.Add("S3", new List<double>(new double[] { 7, 11, 13 }));

            // the sequence has not prime number set containing value  10, 15, 20 
            sequences.Add("S4", new List<double>(new double[] { 10, 15, 20 }));

            // the sequence has prime number set containing value  17, 19, 23 
            sequences.Add("S5", new List<double>(new double[] { 17, 19, 23 }));

            // the sequence has prime number set containing value  21, 25, 29 
            sequences.Add("S6", new List<double>(new double[] { 21, 25, 29 }));
            */


            // the sequence has prime number set containing value  2, 3, 5
            sequences.Add("S1", new List<double>(new double[] { 2, 3, 7 }));

            // the sequence has not prime number set containing value  4, 6, 9 
            sequences.Add("S2", new List<double>(new double[] { 10, 15, 21 }));


            /*
            // This sequence has a difference of 3 between each number. First Number starting from 1
            sequences.Add("S1", new List<double>(new double[] { 0, 2, 4, 6, 8, 10, 12, 14 }));
            // This sequence has a difference of 5 between each number. First Number starting from 3
            sequences.Add("S2", new List<double>(new double[] { 1, 3, 5, 7, 9, 11, 13, 15 }));
            //The sequence is continued by subtracting 2 each time. First Number Starting from 25 
            sequences.Add("S3", new List<double>(new double[] { }));
            //The sequence is even Number. First Number Starting from 0
            sequences.Add("S4", new List<double>(new double[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18}));
            
            //The sequence is odd Number. First Number Starting from 0
            sequences.Add("S5", new List<double>(new double[] { 1, 3, 5, 7, 9, 11, 13, 15, 17, 19}));
            //This Sequence is of Triangular Number, generated from a pattern of dots that form a triangle.
             sequences.Add("S6", new List<double>(new double[] { 1, 3, 6, 10, 15, 21, 28, 36, 45}));
            //They are the squares of whole numbers
            sequences.Add("S7", new List<double>(new double[] { 0, 1, 4, 9, 16, 25, 36, 49, 64, 81}));
            // The sequence is Fibonacci Sequence, found by adding the two numbers before it together.
            sequences.Add("S8", new List<double>(new double[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34}));
            */

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

            // This sequence is even number First Number starting from 2

            sequences.Add("S1", new List<double>(new double[] { 2, 4, 6, 8, 10, 12, 14 }));

            // This sequence is odd number First Number starting from 3

            sequences.Add("S2", new List<double>(new double[] { 3, 5, 7, 9, 11, 13, 15 }));

            // this sequence is neither odd or nor even
            sequences.Add("S3", new List<double>(new double[] { 1.5, 3.4, 5.8, 9.1, 12.5, 14.6, 15.8 }));


            // unclassified SDR

            //The sequence is even number  
            sequences.Add("S4", new List<double>(new double[] { 2, 6, 12, 14}));

            // The sequence is even number  
            sequences.Add("S5", new List<double>(new double[] { 3, 9, 13, 15}));

            // The Sequence is neither even nor odd
            sequences.Add("S6", new List<double>(new double[] { 1.7, 3.2, 9.4, 15.6 }));


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

