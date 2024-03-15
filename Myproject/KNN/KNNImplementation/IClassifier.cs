using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeoCortexApi.Entities;
using System.Collections.Generic;
using NeoCortexApi.Classifiers;

namespace KNNImplementation
{
    public interface IClassifier
    {

        public int Classifier(double[] testData, double[][] trainData, int numofclass, int k);

        public double Distance(double[] testData, double[] trainData);

        public double[][] LearnDatafromthefile(string filePath);
       




    }
}
