using System;
using KNNImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using KNNImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KNN;

namespace YourNamespace
{
    [TestClass]
    public class KNNClassifierTests
    {
        private KNNClassifier knnClassifier;

        [TestInitialize]
        public void Initialize()
        {
            knnClassifier = new KNNClassifier();
        }

        [TestMethod]
        public void TestClassifier()
        {
            // Arrange
            List<List<double>> trainingFeatures = new List<List<double>>()
            {
                new List<double> { 8039, 8738, 9334, 9558, 9604, 9697, 9772, 9841, 9851, 9922, 9963, 10023, 10121, 10197, 10373, 10459, 10594, 10629, 10664, 11124, 0 },
                new List<double> { 9911, 9961, 10007, 10193, 10295, 10353, 10461, 10598, 10612, 10627, 10660, 10772, 10891, 11000, 11070, 11081, 11112, 11305, 11405, 11682, 1 },
                new List<double> { 9067, 9993, 10287, 10739, 10792, 10812, 10880, 11007, 11060, 11092, 11140, 11264, 11391, 11416, 11685, 11712, 11769, 11790, 12199, 12624, 2 }
            };
            List<string> trainingLabels = new List<string> { "Class1", "Class2", "Class3" };
            int k = 1;
            List<double> unknownFeature = new List<double> { 579, 587, 595, 607, 617, 633, 635, 637, 641, 654, 661, 664, 677, 701, 711, 725, 735, 755, 788, 814 };

            // Act
            List<string> predictedLabels = knnClassifier.Classifier(new List<List<double>> { unknownFeature }, trainingFeatures, trainingLabels, k);

            // Assert
            Assert.AreEqual("Class1", predictedLabels[0]); // Update the expected class based on your test case
        }

        [TestMethod]
        public void TestClassifierOutOfRangeK()
        {
            // Arrange
            List<List<double>> trainingFeatures = new List<List<double>>()
            {
                new List<double> { 1, 2, 3 },
                new List<double> { 4, 5, 6 },
                new List<double> { 7, 8, 9 }
            };
            List<string> trainingLabels = new List<string> { "Class1", "Class2", "Class3" };
            int k = 4; // K is out of range

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                knnClassifier.Classifier(null, trainingFeatures, trainingLabels, k);
            });
        }
    }
}