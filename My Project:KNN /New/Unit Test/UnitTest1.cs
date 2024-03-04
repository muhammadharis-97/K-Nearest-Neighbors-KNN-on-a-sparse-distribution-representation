using System;
using KNNImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            double[][] sdrdata = new double[3][]
            {
                new double[] { 8039, 8738, 9334, 9558, 9604, 9697, 9772, 9841, 9851, 9922, 9963, 10023, 10121, 10197, 10373, 10459, 10594, 10629, 10664, 11124, 0 },
                new double[] { 9911, 9961, 10007, 10193, 10295, 10353, 10461, 10598, 10612, 10627, 10660, 10772, 10891, 11000, 11070, 11081, 11112, 11305, 11405, 11682, 1 },
                new double[] { 9067, 9993, 10287, 10739, 10792, 10812, 10880, 11007, 11060, 11092, 11140, 11264, 11391, 11416, 11685, 11712, 11769, 11790, 12199, 12624, 2 }
            };
            int numofclass = 3;
            int k = 1;
            double[] unknownSDR = new double[] { 579, 587, 595, 607, 617, 633, 635, 637, 641, 654, 661, 664, 677, 701, 711, 725, 735, 755, 788, 814 };

            // Act
            int sequence = knnClassifier.Classifier(unknownSDR, sdrdata, numofclass, k);

            // Assert
            Assert.AreEqual(sequence, 0); // Update the expected class based on your test case
        }

        [TestMethod]
        public void TestClassifierOutOfRangeK()
        {
            // Arrange
            double[][] sdrdata = new double[3][];
            int numofclass = 3;
            int k = 4; // K is out of range

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                knnClassifier.Classifier(null, sdrdata, numofclass, k);
            });
        }
    }
}
