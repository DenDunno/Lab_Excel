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
    public class CellTests
    {

        [TestMethod()]
        public void SetCellTest()
        {
            Cell A1 = new Cell("A1" , 1 , 1);
            Cell A3 = new Cell("A1", 2, 1);

            Grid.cells["A1"] = A1;
            Grid.cells["A3"] = A3;

            try
            {
                A1.SetCell("A3 + A1");
                A1.SetCell("A6");
                A1.SetCell("1 / 0");
            }

            catch
            {
                Assert.Fail(); // всі exception повинні зловитись у самого класа Cell
            }
        }
    }
}