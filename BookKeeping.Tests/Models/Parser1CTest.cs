using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BookKeeping;
using BookKeeping.Models;

namespace BookKeeping.Tests.Models
{
    [TestClass]
    public class Parser1CTest
    {
        [TestMethod]
        public void Parse()
        {
            using (var stream = File.OpenRead("../kl_to_1c_TEST.txt"))
            {
                var objects = Parser1C.Parse(stream, new[] { typeof(Платежное_Поручение) }).Cast<Платежное_Поручение>().ToArray();

                Assert.AreEqual(objects.Length, 32);
                Assert.AreEqual(objects[0].Номер, 828);
                Assert.IsTrue(objects[1].ДатаПоступило > new DateTime(1980, 1, 1));
                Assert.IsTrue(objects[2].ДатаПоступило < new DateTime(2100, 1, 1));
            }
        }
    }
}
