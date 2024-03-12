using KNNImplementation;

namespace KNNUnitTest
{
    [TestClass]
    public class UnitTest
    {
        KNNClassifier kNN = new KNNClassifier();
        double[][] sdrdata = new double[3][]
        {
                new double[] { 8039, 8738, 9334, 9558, 9604, 9697, 9772, 9841, 9851, 9922, 9963, 10023, 10121, 10197, 10373, 10459, 10594, 10629, 10664, 11124, 0 },
                new double[] { 9911, 9961, 10007, 10193, 10295, 10353, 10461, 10598, 10612, 10627, 10660, 10772, 10891, 11000, 11070, 11081, 11112, 11305, 11405, 11682, 1 },
                new double[] { 9067, 9993, 10287, 10739, 10792, 10812, 10880, 11007, 11060, 11092, 11140, 11264, 11391, 11416, 11685, 11712, 11769, 11790, 12199, 12624, 2 }
        };
        int numofclass;
        int k;
        double[] unknownSDR = new double[] { 8032, 8739, 9327, 9560, 9621, 9699, 9761, 9827, 9857, 9916, 9970, 10015, 10123, 10175, 10362, 10462, 10589, 10628, 10669, 11122 };

        [TestInitialize]
        public void Initialize()
        {
            KNNClassifier kNN = new KNNClassifier();
            k = 1;
            numofclass = 3;

        }
        [TestCleanup]
        public void Cleanup()
        {

        }
        [TestMethod]
        public void TestClassifier()
        {
            if (k < numofclass)
            {
                //KNNClassifier kNN = new KNNClassifier();
                int sequence = kNN.Classifier(unknownSDR, sdrdata, numofclass, k);
                // Assert
                Assert.AreEqual(sequence, 0); // Update the expected class based on your test case
            }
            else
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    kNN.Classifier(unknownSDR, sdrdata, numofclass, k);
                });


            }


        }
    }
}