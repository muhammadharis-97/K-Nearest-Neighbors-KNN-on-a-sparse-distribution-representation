//Global Variable
using NeoCortexApi;
using NeoCortexApi.Encoders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            // List of SDR value and Sequence list name Dateset 




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

