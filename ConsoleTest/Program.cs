using Mockingbird;
using Model;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup();
            TestMethod();

            Console.ReadKey();
        }

        public static void Setup()
        {
            MockEngine.Initialize();
        }

        public static void TestMethod()
        {
            //Func<int, int, int> act = (a, b) =>
            //{
            //    if (a > b)
            //    {
            //        return b;
            //    }
            //    else
            //    {
            //        return a;
            //    }
            //};

            //var methodInfo = typeof(Test).GetMethod("GetLargerNumber");
            //MockEngine.Mock(methodInfo, act);

            //var test = new Test();
            //var result = test.GetLargerNumber(1, 2);

            var test = new MyClass();
            test.TestMethod();
        }
    }

    public class MyClass
    {
        public string GetLargerObject<T>(T a, T b) where T : IComparable
        {
            if (a.CompareTo(b) > 0)
            {
                return b.ToString();
            }
            else
            {
                return a.ToString();
            }
        }

        public void TestMethod()
        {
            var test = new Test();
            var result = test.GetLargerObject(1, 2);


            var methodInfo = typeof(Test).GetMethod("GetLargerObject");
            var mock = this.GetType().GetMethod("GetLargerObject");
            MockEngine.Mock(methodInfo, mock);

            var testAfter = new Test();
            var resultAfter = test.GetLargerObject(1, 2);
        }
    }
}
