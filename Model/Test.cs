using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Model
{
    public class Test
    {
        public int Number { get; private set; }
        public int NextNumber { get; private set; }

        public Test()
        {
            Number = 5;
        }

        public void SetNumber(int number)
        {
            NextNumber = number;
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static int GetLargerNumberStatic(int a, int b)
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

        public string GetLargerObject<T>(T a, T b) where T : IComparable
        {
            if (a.CompareTo(b) < 0)
            {
                return b.ToString();
            }
            else
            {
                return a.ToString();
            }
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
