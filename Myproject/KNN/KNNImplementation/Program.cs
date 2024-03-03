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

            
            
            /// loading dataset SDR value and Sequence list reference name
            /// 
            double[][] trainData = SDRdataset();
            double[] testData = new double[] { 7816, 8229, 8674, 8707, 8946, 9400, 9493, 9543, 9562, 9658, 9695, 9752, 9821, 9912, 9978, 10209, 10300, 10607, 10658, 11084 };
            double[] testData1 = new double[] { 8673, 8700, 9814, 9998, 10129, 10193, 10315, 10487, 10618, 10661, 10732, 10772, 10936, 10955, 11079, 11285, 11392, 11458, 11475 };

            int numofclass = 2;
            int K = 1;
            
            
            
            Console.WriteLine(" Value of K is equal to 1");
            KNNClassifier kNN = new KNNClassifier(); 

            int sequence = kNN.Classifier(testData, trainData, numofclass, K);
            int sequence1 = kNN.Classifier(testData1, trainData, numofclass, K);


            Console.WriteLine("Predicted class for first test data ");
            if (sequence == 0)
                Console.WriteLine("Even");
            else
                Console.WriteLine("Odd");

            Console.WriteLine("Predicted class for second test data ");
            if (sequence1 == 0)
                Console.WriteLine("Even");
            else
                Console.WriteLine("Odd");


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


            RunMultiSequenceLearningExperiment();
        }

        /// <summary>
        ///  Dataset of SDR values extracted from the sequence of even and odd set of numbers
        /// </summary>
        /// <returns></returns>
       
        static double[][] SDRdataset()
        {
            double[][] data = new double[2][];
            data[0] = new double[] { 7676, 8328, 8437, 9266, 9318, 9354, 9393, 9470, 9501, 9827, 9941, 10087, 10109, 10273, 10409, 10441, 10496, 10767, 10860, 10998, 0 };
            data[1] = new double[] { 8985, 9354, 9501, 9941, 10109, 10131, 10239, 10273, 10409, 10441, 10496, 10674, 10692, 10710, 10767, 10860, 10998, 11281, 11349, 1 };

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

            //sequences.Add("S1", new List<double>(new double[] { 2, 4, 6, 8, 10, 12, 14 }));

            // This sequence is odd number First Number starting from 3

            //sequences.Add("S2", new List<double>(new double[] { 3, 5, 7, 9, 11, 13, 15 }));
            
            // unclassified SDR

            // The sequence is even number  
            sequences.Add("S1", new List<double>(new double[] { 2, 6, 12, 14}));

            // The sequence is even number  
            sequences.Add("S2", new List<double>(new double[] { 3, 9, 13, 15}));

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

