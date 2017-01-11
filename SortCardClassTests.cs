using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortCardsList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortCardsList.Tests
{
    [TestClass()]
    public class SortCardClassTests
    {
        [TestMethod()]
        public void SortListTest()
        {
            SortCardClass sc = new SortCardClass();
            try
            {
                sc.SortList(null);
            }
            catch
            {
                Assert.IsFalse(false);
            }

            var c1 = new Card() { From = "Moscow", To = "London" };
            var c2 = new Card() { From = "London", To = "Krakow" };
            var c3 = new Card() { From = "Krakow", To = "Vilnus" };
            var c4 = new Card() { From = "Vilnus", To = "Vankuver" };
            List<Card> list = new List<Card>() { c3, c2, c1, c4 };
            try
            {
                sc.SortList(list);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod()]
        public void GetCardsTest()
        {
            SortCardClass sc = new SortCardClass();
            var res = sc.GetCards(string.Empty);
            Assert.IsNull(res);
            res = sc.GetCards(null);
            Assert.IsNull(res);
            res = sc.GetCards("city1");
            Assert.IsNull(res);
            res = sc.GetCards("city1,city2,city1");
            Assert.IsNull(res);
            res = sc.GetCards("Moscow,London,Krakow,Vilnus,Vankuver");
            Assert.IsNotNull(res);
        }
    }
}