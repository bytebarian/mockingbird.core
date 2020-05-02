using Mockingbird;
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
