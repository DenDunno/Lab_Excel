using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void EvaluateTest()
        {

            {
                string actual = "min (0,4) + 2 * ( 1 + 30 mod (49 div 3)) * max(3,2) + 21 div 2";
                actual = actual.Replace(" ", "");
                string expected = "100";
                Assert.AreEqual(expected, Calculator.Evaluate(actual));
            }


            {
                string actual = "max(3 , 5) * min (3 , 5)";
                actual = actual.Replace(" ", "");
                string expected = "15";
                Assert.AreEqual(expected, Calculator.Evaluate(actual));
            }


            {
                string actual = "-(-1)";
                actual = actual.Replace(" ", "");
                string expected = "1";
                Assert.AreEqual(expected, Calculator.Evaluate(actual));
            }


            {
                string actual = "3 * 5 mod 4";
                actual = actual.Replace(" ", "");
                string expected = "3";
                Assert.AreEqual(expected, Calculator.Evaluate(actual));
            }


            try
            {
                string actual = "100 + 1 / 0";
                actual = actual.Replace(" ", "");
                string expected = Calculator.Evaluate(actual);

                Assert.Fail(); // якщо не було exception
            }
            catch (DivideByZeroException)
            {
            }
        }
    }
}