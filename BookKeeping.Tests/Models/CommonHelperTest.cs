using System;
using System.Linq;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using BookKeeping;
using BookKeeping.Models;

namespace BookKeeping.Tests.Models
{
    [TestClass]
    public class CommonHelperTest
    {
        private class AB
        {
            public int A { get; set; }
            public int B { get; set; }
        }

        [TestMethod]
        public void Contains()
        {
            Assert.IsTrue("Hello world!".Contains("WORLD", StringComparison.OrdinalIgnoreCase));
            Assert.IsFalse("Hello world!".Contains("test", StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod]
        public void ContainsKey()
        {
        }

        [TestMethod]
        public void AsEnumerable()
        {
            var dict = new NameValueCollection()
            {
                ["A"] = "1,2,3",
                ["B"] = "4,5,6"
            };

            var objects = dict.AsEnumerable(typeof(AB), CultureInfo.CurrentCulture).Cast<AB>().ToArray();

            Assert.AreEqual(objects.Length, 3);
            Assert.AreEqual(objects[0].A, 1);
            Assert.AreEqual(objects[1].B, 5);
        }
    }
}
