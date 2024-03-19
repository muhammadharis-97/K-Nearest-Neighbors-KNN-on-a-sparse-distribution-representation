using KNNImplementation;

namespace KNNUnitTest
{
    [TestClass]
    public class UnitTest
    {
        KNNClassifier kNN = new KNNClassifier();
        double[][] sdrData = new double[36][];
        double[] testdata = new double[0];
        int numofclass;
        int k;
        int actualLabel;

        [TestInitialize]
        public void Initialize()
        {
           
            k = 3;
            numofclass = 3;
            /// Take one sequence SDR from the dataset having Class label as 1
            testdata = [8816, 8865, 8953, 9771, 9784, 10108, 10177, 10205, 10401, 10427, 10561, 10598, 10610, 10629, 10751, 10993, 11306, 11341, 11426, 11500];
            actualLabel = 1;
            sdrData = kNN.LearnDatafromthefile("C:\\Users\\Lenovo\\Documents\\GitHub\\Global_Variables\\Myproject\\KNN\\KNNImplementation\\Dataset\\sdr_dataset.txt");
        }
        [TestCleanup]
        public void Cleanup()
        {

        }
        [TestMethod]
        public void TestClassifier()
        {
            

            if (k < sdrData.Length)
            {

                    int sequence = kNN.Classifier(testdata, sdrData, numofclass, k);
                    // Assert
                    Assert.AreEqual(sequence, actualLabel); // Update the expected class based on your test case

            }
            else
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    kNN.Classifier(testdata, sdrData, numofclass, k);
                });


            }


        }
    }
}