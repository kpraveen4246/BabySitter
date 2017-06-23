using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BabysitterKata.Tests
{
    class BabysitterPaySheet_Tests
    {
        [TestCase(0, 0, 0)]
        [TestCase(3, 2, 3)]
        [TestCase(2, 5, 3)]
        [TestCase(1, 2, 5)]
        public void BabysitterPaySheet_GivenThreeValidParameters_CreateBabysitterPaySheet(int hrsBeforeBed, int hrsBedToMidnight, int hrsAfterMidnight)
        {
            BabysitterPaySheet paySheet = new BabysitterPaySheet(hrsBeforeBed, hrsBedToMidnight, hrsAfterMidnight);

            Assert.IsInstanceOf(typeof(BabysitterPaySheet), paySheet);
        }

        [TestCase(2, 4)]
        [TestCase(1, 6)]
        [TestCase(0, 3)]
        [TestCase(4, 0)]
        [TestCase(0, 0)]
        public void BabysitterPaySheet_GivenTwoValidParameters_CreateBabysitterPaySheet(int hrsBeforeBed, int hrsBedToMidnight)
        {
            BabysitterPaySheet paySheet = new BabysitterPaySheet(hrsBeforeBed, hrsBedToMidnight);

            Assert.IsInstanceOf(typeof(BabysitterPaySheet), paySheet);
        }

        [TestCase(1, 2, 3, 76)]
        [TestCase(2, 3, 1, 64)]
        [TestCase(3, 1, 0, 44)]
        [TestCase(6, 1, 2, 112)]
        public void BabysitterPaySheet_GivenThreeValidParameters_ReturnCorrectPay(int hrsBeforeBed, int hrsBedToMidnight, int hrsAfterMidnight, int expectedPay)
        {
            BabysitterPaySheet paySheet = new BabysitterPaySheet(hrsBeforeBed, hrsBedToMidnight, hrsAfterMidnight);

            Assert.AreEqual(expectedPay, paySheet.Pay);
        }

        [TestCase(1, 0, 12)]
        [TestCase(0, 0, 0)]
        [TestCase(3, 1, 44)]
        [TestCase(2, 4, 56)]
        [TestCase(2, 2, 40)]
        public void BabysitterPaySheet_GivenTwoValidParameters_ReturnCorrectPay(int hrsBeforeBed, int hrsBedToMidnight, int expectedPay)
        {
            BabysitterPaySheet paySheet = new BabysitterPaySheet(hrsBeforeBed, hrsBedToMidnight);

            Assert.AreEqual(expectedPay, paySheet.Pay);
        }
    }
}
