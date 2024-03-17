using KNNImplementation;

namespace KNNUnitTest
{
    [TestClass]
    public class UnitTest
    {
        KNNClassifier kNN = new KNNClassifier();
        double[][] sdrData = new double[36][];
        double[] Unclassified = new double[0];
        int numofclass;
        int k;
        double trainRatio;
        int actualLabel;

        [TestInitialize]
        public void Initialize()
        {
           
            k = 1;
            numofclass = 3;
            trainRatio = 0.8;
            /// Take one sequence SDR from the dataset having Class label as 1
            Unclassified = [9911, 9961, 10007, 10193, 10295, 10353, 10461, 10598, 10612, 10627, 10660, 10772, 10891, 11000, 11070, 11081, 11112, 11305, 11405, 11682];
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

                    int sequence = kNN.Classifier(Unclassified, sdrData, numofclass, k);
                    // Assert
                    Assert.AreEqual(sequence, actualLabel); // Update the expected class based on your test case

            }
            else
            {
                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    kNN.Classifier(Unclassified, sdrData, numofclass, k);
                });


            }


        }
    }
}