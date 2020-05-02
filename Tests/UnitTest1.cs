using Mockingbird;
using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            MockEngine.Initialize();
        }

        [Test]
        public void TestMethod()
        {
            Func<int, int, int> act = (a, b) =>
            {
                if (a > b)
                {
                    return b;
                }
                else
                {
                    return a;
                }
            };

            var methodInfo = typeof(Test).GetMethod("GetLargerNumber");
            MockEngine.Mock(methodInfo, act);

            var test = new Test();
            var result = test.GetLargerNumber(1, 2);

            Assert.AreEqual(1, result);
        }
    }

    public class Test
    {
        public int GetLargerNumber(int a, int b)
        {
            if (a < b)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
    }
}