using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNNImplementation
{

    // KNN Algorithm 
    public class KNNClassifier : IClassifier
    {
        
        // Classification Methond using SDRValue in the form of training Data to the Classifier Model
        // For Classification UnknownSDR value of a paricualar squence is used to classify, to see wheater these SDR value belong or near to any of SDR vlaues in Dataset  
        // the number of classification type are defined as numofclass
        // While K is the tune to see how many Neareast Neibour SDr are near to unknownSDR values 
        public int Classifier(double[] unknownSDR, double[][] Sdrdata, int numofclass, int k)
        {
            





            return 0;
        }

    }
}
