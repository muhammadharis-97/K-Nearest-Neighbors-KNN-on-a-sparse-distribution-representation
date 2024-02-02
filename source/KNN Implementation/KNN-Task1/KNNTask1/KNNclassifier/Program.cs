using System;
using System.ComponentModel;
using KNN;

//// Instance of a Class KNN_GlobalVariable
KNN_GlobalVariable KNN = new KNN_GlobalVariable();
/// Load Data
static double[][] Loaddata()
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

Console.WriteLine("Begin with KNN Classification");
double[][] trainData = Loaddata();


//// Initializing num of features and classes
int numFeatures = 20;
int numClasses = 10;
double[] unknown = new double[] { 772, 779, 780, 800, 810, 811, 812, 826, 830, 853, 861, 878, 886, 889, 891, 897, 954, 957, 960, 1007 };
//Console.WriteLine("Predictor values: 5.25 3.75 ");

/// Applying classifier for K=1
//int k = 1;
//Console.WriteLine("With k = 1");
//int predicted = KNN.Classifier(unknown, trainData, numClasses, k);
//Console.WriteLine("Predicted class = " + predicted);

/// Applying classifier for K=1
int k = 4;
Console.WriteLine("With k = 4");
int predicted = KNN.Classifier(unknown, trainData, numClasses, k);
Console.WriteLine("Predicted class = " + predicted);
Console.WriteLine("End kNN ");
Console.ReadLine();




Console.Write("KNN Clasifier");
