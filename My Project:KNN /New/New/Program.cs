//Global Variable
using KNNImplementation;
using NeoCortexApi;
using System.IO;
using System.Text;
using NeoCortexApi.Encoders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using static NeoCortexApiSample.MultiSequenceLearning;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace NeoCortexApiSample
{
    class Program
    {

        // Main method to start the program
        static void Main(string[] args)
        {


            

                //RunMultiSequenceLearningExperiment();

                // start experiement that demonstrates how to predict the squence based on HTM predcited cells.
                KNNClassificationExperiment("/Users/zakaahmedchishti/Projects/New/New/sdr_dataset.txt");

        }

        private static void KNNClassificationExperiment(string Datasetfilepath)
        {
            // Defining the value of K
            int k = 3;

            // Intailizing the number of classes 
            int numofclass = 3;
            double[] features = new double[20];

            // Set the ratio for splitting (70% training, 30% testing)
            double trainRatio = 0.7;

            /// creating an Instance of the class KNN
            KNNClassifier kNN = new KNNClassifier();

            // Learning data from the dataset file
            double[][] sdrData = kNN.LoadDataFromFile(Datasetfilepath);

            // Call the method to split the data
            var (trainDataset, testDataset) = kNN.SplitData(sdrData, trainRatio);

            // Extracting the Actual labels from test dataset
            int[] actualLabels = kNN.ExtractActualLabelsFromDataset(testDataset);
            int[] predictedlabels = new int[testDataset.Length];


            // Starting the KNN Classifier
            Debug.WriteLine(" Starting of KNN Classifier on Sparse Distribution Representation");
            //Debug.WriteLine();



            int i = 0;
            Debug.WriteLine($"  Value of K is equal to {k}");

            // Looping through each test dataset
            foreach (var testData in testDataset)
            {

                // Classifying the test data using KNN Classifier 
                int prediction = kNN.Classifier(testData, trainDataset, numofclass, k);

                predictedlabels[i] = prediction;

                i = i + 1;

                // Displaying the predicted class for the test data
                Debug.WriteLine($"  Predicted class for test data: {(prediction == 0 ? "Even" : (prediction == 1 ? "Odd" : (prediction == 2 ? "Neither Odd nor Even" : "Unknown")))}");
            }



            double accuracy = kNN.CalculateAccuracy(predictedlabels, actualLabels);
            Debug.WriteLine("  Calculated Accuracy   =   " + accuracy);





        }


        /// <summary>
        /// Runs a multi-sequence learning experiment using simple sequences.
        /// </summary>
        private static void RunMultiSimpleSequenceLearningExperiment()
        {
            // Initialize a dictionary to store sequences, where each sequence is represented by a list of doubles.
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            // Define the first sequence (S1) with prime numbers: 2, 3, 7.
            sequences.Add("S1", new List<double>(new double[] { 2, 3, 7 }));

            // Define the second sequence (S2) with non-prime numbers: 10, 15, 21.
            sequences.Add("S2", new List<double>(new double[] { 10, 15, 21 }));

            // Initialize the multi-sequence learning experiment.
            MultiSequenceLearning experiment = new MultiSequenceLearning();

            // Run the experiment to build the prediction engine.
            var predictor = experiment.Run(sequences);
        }


        /// <summary>
        /// Runs a multi-sequence learning experiment using various types of sequences.
        /// </summary>
        private static void RunMultiSequenceLearningExperiment()
        {
            // Initialize a dictionary to store sequences, where each sequence is represented by a list of doubles.
            Dictionary<string, List<double>> sequences = new Dictionary<string, List<double>>();

            // Define the first sequence (S1) with even numbers: 2, 4, 6, 8, 10, 12, 14.
            sequences.Add("S1", new List<double>(new double[] { 2, 4, 6, 8, 10, 12, 14 }));

            // Define the second sequence (S2) with even numbers: 2, 6, 12, 14.
            //sequences.Add("S2", new List<double>(new double[] { 2, 6, 12, 14 }));

            // Define the third sequence (S3) with odd numbers starting from 3: 3, 5, 7, 9, 11, 13, 15.
            sequences.Add("S3", new List<double>(new double[] { 3, 5, 7, 9, 11, 13, 15 }));

            // Define the fourth sequence (S4) with odd numbers: 3, 9, 13, 15.
            //sequences.Add("S4", new List<double>(new double[] { 3, 9, 13, 15 }));

            // Define the fifth sequence (S5) with numbers that are neither odd nor even: 4.5, 11.4, 12.8, 15.5, 16.6, 17.7.
            sequences.Add("S5", new List<double>(new double[] { 4.5, 11.4, 12.8, 15.5, 16.6, 17.7 }));

            // Define the sixth sequence (S6) with numbers that are neither odd nor even: 4.5, 11.4, 12.8, 16.6.
            //sequences.Add("S6", new List<double>(new double[] { 4.5, 11.4, 12.8, 16.6 }));

            // Initialize the multi-sequence learning experiment.
            MultiSequenceLearning experiment = new MultiSequenceLearning();

            // Run the experiment to build the prediction engine.
            var predictor = experiment.Run(sequences);
        }

        /// <summary>
        /// Predicts the next element in the sequence using the provided predictor.
        /// </summary>
        /// <param name="predictor">The predictor used for making predictions.</param>
        /// <param name="list">The list of elements for which predictions need to be made.</param>
        private static void PredictNextElement(Predictor predictor, double[] list)
        {
            // Output a separator for better readability in debug output.
            Debug.WriteLine("------------------------------");

            // Iterate through each element in the provided list.
            foreach (var item in list)
            {
                // Predict the next element based on the current item in the sequence.
                var res = predictor.Predict(item);

                // Check if predictions are available.
                if (res.Count > 0)
                {
                    // Output each prediction along with its similarity score.
                    foreach (var pred in res)
                    {
                        Debug.WriteLine($"{pred.PredictedInput} - {pred.Similarity}");
                    }

                    // Extract the predicted sequence and the next predicted element.
                    var tokens = res.First().PredictedInput.Split('_');
                    var tokens2 = res.First().PredictedInput.Split('-');

                    // Output the predicted sequence and the next predicted element.
                    Debug.WriteLine($"Predicted Sequence: {tokens[0]}, predicted next element {tokens2.Last()}");
                }
                else
                {
                    // Output a message if no predictions are available.
                    Debug.WriteLine("Nothing predicted :(");
                }
            }

            // Output a separator for better readability in debug output.
            Debug.WriteLine("------------------------------");
        }
    }
}