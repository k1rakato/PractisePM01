using Microsoft.VisualStudio.TestTools.UnitTesting;
using REG_MARK_LIB;
using System;

namespace RegMarkDllLibTests
{
    [TestClass]
    public class RegMarkLibUnitTest
    {

        [TestMethod]
        public void CheckMark_IsTrue()
        {
           bool condition = RegMarkDllLib.CheckMark("A777AA52");
            Assert.IsTrue(condition);
        }  
        
        [TestMethod]
        public void CheckMark_MaxValueIsTrue()
        {
           bool condition = RegMarkDllLib.CheckMark("X999XX99");
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void CheckMark_IsFalse()
        {
           bool condition = RegMarkDllLib.CheckMark("B322OL333");
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void CheckMark_Non_Existent_Values_IsFalse()
        {
           bool condition = RegMarkDllLib.CheckMark("Z000ZZ00");
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void GetNextMarkAfter_WithNextNumber()
        {
            string actual = RegMarkDllLib.GetNextMarkAfter("M023OT152");
            Assert.AreEqual("M024OT152", actual);
        }

        [TestMethod]
        public void GetNextMarkAfter_ChangeLetterCombination()
        {
            Assert.AreEqual("A001BT52", RegMarkDllLib.GetNextMarkAfter("A999BC52"));
        }

        [TestMethod]
        public void GetNextMarkAfterInRange_WithinRange()
        {
            Assert.AreEqual("A042BC77", RegMarkDllLib.GetNextMarkAfterInRange("A041BC77", "A030BC77", "A090BC77"));
        }

        [TestMethod]
        public void GetNextMarkAfterInRange_OutOfRange()
        {
            Assert.AreEqual("out of stock", RegMarkDllLib.GetNextMarkAfterInRange("X190AX77", "X120AX77", "X190AX77"));
        }

        [TestMethod]
        public void GetCombinationsCountInRange_ValidRange()
        {
            Assert.AreEqual(5, RegMarkDllLib.GetCombinationsCountInRange("A123BC99", "A127BC99"));
        }

        [TestMethod]
        public void GetCombinationsCountInRange_LargeRange()
        {
            Assert.AreEqual(100, RegMarkDllLib.GetCombinationsCountInRange("A000AA99", "A099AA99"));
        }



        // Hard tests
        [TestMethod]
        public void GetNextMarkAfter_ChangeRegion()
        {
            Assert.AreEqual("A001XA99", RegMarkDllLib.GetNextMarkAfter("A999YX99"));
        }

        [TestMethod]
        public void GetCombinationsCountInRange_ReturnMinus_1()
        {
            Assert.AreEqual(-1, RegMarkDllLib.GetCombinationsCountInRange(" ", " "));
        }

        [TestMethod]
        public void GetNextMarkAfterInRange_LastAvailableMark()
        {
            Assert.AreEqual("out of stock", RegMarkDllLib.GetNextMarkAfterInRange("A999BC99", "A990BC99", "A999BC99"));
        }

        [TestMethod]
        public void GetNextMarkAfterInRange_OnlyOneMark()
        {
            Assert.AreEqual("out of stock", RegMarkDllLib.GetNextMarkAfterInRange("M023OT152", "M023OT152", "M023OT152"));
        }

        [TestMethod]
        public void GetNextMarkAfterInRange_EmptyValues()
        {
            Assert.AreEqual("Invalid format", RegMarkDllLib.GetNextMarkAfterInRange(" ", " ", " "));
        }

    }
}
