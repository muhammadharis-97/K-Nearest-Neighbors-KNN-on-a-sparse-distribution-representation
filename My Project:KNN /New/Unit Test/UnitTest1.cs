
using System.Security.Principal;

namespace Unit_Test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1(double[][] sdrdata, int K = 1)
    {

        //unknownSDR is the Static Input for testing

        double[] unknownSDR = new double[] { 579, 587, 595, 607, 617, 633, 635, 637, 641, 654, 661, 664, 677, 701, 711, 725, 735, 755, 788, 814 };

        int numofclass = 9;

        //Exceptions
        if (K > 3)
        {
            throw new System.ArgumentOutOfRangeException("K cann't acceed the 3 Value");
        }


        int sequence = Classifier(unknownSDR, sdrdata, numofclass, K);



        Console.WriteLine(" Value of K is equal to 1");
        Console.WriteLine("Predicted class ");
        Console.WriteLine(sequence);

        // This verfies that the sequence is the respective 8 cleass or not?
        // Change it to test the test results. 
        Assert.AreEqual(sequence, 8);



    }











}
