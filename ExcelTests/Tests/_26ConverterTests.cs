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
    public class _26ConverterTests
    {
        private int expected;
        private string _26;
        private int actual;


        [TestMethod()]
        public void ConvertTest()
        {
            {
                expected = 200;
                _26 = _26Converter.ConvertTo26(expected);
                actual = _26Converter.ConvertTo10(_26);
                Assert.AreEqual(expected, actual);
            }


            {
                expected = 1000;
                _26 = _26Converter.ConvertTo26(expected);
                actual = _26Converter.ConvertTo10(_26);
                Assert.AreEqual(expected, actual);
            }


            {
                expected = 10002;
                _26 = _26Converter.ConvertTo26(expected);
                actual = _26Converter.ConvertTo10(_26);
                Assert.AreEqual(expected, actual);
            }


            {
                expected = 43241;
                _26 = _26Converter.ConvertTo26(expected);
                actual = _26Converter.ConvertTo10(_26);
                Assert.AreEqual(expected, actual);
            }


            {
                expected = 123464356;
                _26 = _26Converter.ConvertTo26(expected);
                actual = _26Converter.ConvertTo10(_26);
                Assert.AreEqual(expected, actual);
            }
        }

        

        
    }
}