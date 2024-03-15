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

        public int Classifier(double[] unknownSDR, double[][] Sdrdata, int numofclass, int k);

        public double Distance(double[] unknownSDR, double[] SdrData);

        public double[][] LearnDatafromthefile(string filePath);
       




    }
}
