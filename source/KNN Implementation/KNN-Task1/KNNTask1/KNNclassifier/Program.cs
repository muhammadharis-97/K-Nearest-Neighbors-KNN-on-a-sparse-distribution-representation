using System;
using KNN;


/// Load Data


Console.WriteLine("Begin with KNN Classification");
double[][] trainData = Loaddata();

//// Initializing num of features and classes
int numFeatures = 2;
int numClasses = 3;
double[] unknown = new double[] { 2.01, 4.5 };
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




Console.Write("KNN Clasifier");
