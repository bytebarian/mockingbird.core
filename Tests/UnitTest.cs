using Mockingbird;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tests
{
    public class Tests
    {
        public Func<int, int, int> act;

        [SetUp]
        public void Setup()
        {
            MockEngine.Initialize();

            act = (a, b) =>
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
        }

        [Test]
        public void PublicMethodTest()
        {
            var methodInfo = typeof(Test).GetMethod("GetLargerNumber");
            MockEngine.Mock(methodInfo, act);

            var test = new Test();
            var result = test.GetLargerNumber(1, 2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void VirtualMethodTest()
        {
            var methodInfo = typeof(Test).GetMethod("GetLargerNumberVirtual");
            MockEngine.Mock(methodInfo, act);

            var test = new Test();
            var result = test.GetLargerNumberVirtual(1, 2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void StaticMethodTest()
        {
            var methodInfo = typeof(Test).GetMethod("GetLargerNumberStatic", BindingFlags.Public | BindingFlags.Static);
            MockEngine.Mock(methodInfo, act);

            var result = Test.GetLargerNumberStatic(1, 2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void PrivateMethodTest()
        {
            var methodInfo = typeof(Test).GetMethod("GetLargerNumberPrivte", BindingFlags.NonPublic | BindingFlags.Instance);
            MockEngine.Mock(methodInfo, act);

            var test = new Test();
            var result = test.GetLargerNumberPrivateProxy(1, 2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void ConstructorTest()
        {
            Func<Test> func = () =>
            {
                var instance = new Test();
                instance.Number = 7;
                return instance;
            };

            var constructor = typeof(Test).GetConstructor(Type.EmptyTypes);
            MockEngine.Mock(constructor, func);

            var test = new Test();

            Assert.AreEqual(7, test.Number);
        }

        [Test]
        public void ProperyTest()
        {

        }
    }

    public class Test
    {
        public int Number { get; set; }

        public Test()
        {
            Number = 5;
        }

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

        public virtual int GetLargerNumberVirtual(int a, int b)
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

        public static int GetLargerNumberStatic(int a, int b)
        {
            // if a method is too short, JIT-complier complies it as inline, then we can't update it.
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");
            Trace.WriteLine("XXXXXXXXXXXXXXX");

            if (a < b)
            {
                return b;
            }
            else
            {
                return a;
            }
        }

        private int GetLargerNumberPrivte(int a, int b)
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

        public int GetLargerNumberPrivateProxy(int a, int b)
        {
            return GetLargerNumberPrivte(a, b);
        }
    }

    public class ConcreteTest : Test
    {
        public override int GetLargerNumberVirtual(int a, int b)
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

    public class Test<T> where T : IComparable
    {
        public T GetLargerObject(T a, T b)
        {
            if (a.CompareTo(b) < 0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
    }

    public class TestSingleton
    {
        private static TestSingleton Instance;
        public int Number { get; private set; }

        private TestSingleton()
        {
            Number = 5;
        }

        public static TestSingleton GetInstance()
        {
            if (Instance == null) Instance = new TestSingleton();
            return Instance;
        }
    }
}