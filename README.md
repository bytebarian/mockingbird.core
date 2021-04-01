# mockingbird.core
Mocking framework for C# language and .net core which enable to mock any kind of method including virtual, non-virtual, private, static and even constructors. 
Mocking is obtained thatnks to JIT compiler hooking and IL injection.

![how to hook jit compiler](https://github.com/bytebarian/mockingbird.core/blob/master/hook.PNG)

## Initialization
First of all you need to initialize mocking engine. It may take a while for the first time.
```c#
MockEngine.Initialize();
```

## Mocking public, private, virtual and overriden methods

Imagine you have user repository class that have public, virtual or overriden method for getting all users count from database. Using Mockingbird you can mock this method like that. In case of private methods we need to pass additional ```BindingFlags``` enum value like 
```var methodInfo = typeof(UserRepository).GetMethod("GetUsersCount", BindingFlags.NonPublic | BindingFlags.Instance);```

```c#
public class Tests
{
        public Func<int, int, int> act;
        
        [SetUp]
        public void Setup()
        {
            MockEngine.Initialize();

            act = () =>
            {
                return 100;
            };
        }
        
        [Test]
        public void PublicVirtualMethodTest()
        {
            var methodInfo = typeof(UserRepository).GetMethod("GetUsersCount");
            MockEngine.Mock(methodInfo, act);

            var users = new UserRepository();
            var result = users.GetUsersCount();

            Assert.AreEqual(100, result);
        }
}
```
In case of private methods we need to pass additional ```BindingFlags``` enum value like 
```var methodInfo = typeof(UserRepository).GetMethod("GetUsersCount", BindingFlags.NonPublic | BindingFlags.Instance);```

## Mocking static methods

In simmilar way we can also mock static methods. Imagine you have class ```File``` that have method ```Exist``` accepting string argument representing file path and returning value ```true``` or ```false``` depending of existance of given file. Thanks to Mockingbird we can mock that method and write proper unit tests for any code that using that static method. Method can be public or private the only different is defining proper ```BindingFlags```.

```c#
public class Tests
{
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool Exist(string path)
        {
            return true;
        }
        
        [SetUp]
        public void Setup()
        {
            MockEngine.Initialize();
        }
        
        [Test]
        public void StaticMethodTest()
        {
            var methodInfo = typeof(File).GetMethod("Exist", BindingFlags.Public | BindingFlags.Static);
            var mock = typeof(Tests).GetMethod("Exist", BindingFlags.Public | BindingFlags.Static);
            MockEngine.Mock(methodInfo, mock);

            var result = File.Exist("any path");

            Assert.IsTrue(result);
        }
}
```

## Mocking properties

Imagine you have class ```File``` that have ```IsSaved``` property. You can also mock properties getters like that:

```c#
public class Tests
{
        [SetUp]
        public void Setup()
        {
            MockEngine.Initialize();
        }
        
        [Test]
        public void ProperyGetTest()
        {
            Func<bool> func = () => true;

            var methodInfo = typeof(File).GetProperty("IsSaved").GetGetMethod();
            MockEngine.Mock(methodInfo, func);

            var file = new File();

            Assert.IsTrue(file.IsSaved);
        }
}
```

## Mocking constructors

Sometimes you can encounter situaltion when there is some initialization logic inside class constructor, which may call 3-rd party API or databse for some data. In that case while we are runing unit tests we want to get rid of that logic. Using mockingbird we can do it as simply as that:

```c#
public class Tests
{
        [SetUp]
        public void Setup()
        {
            MockEngine.Initialize();
        }
        
        [Test]
        public void ProperyGetTest()
        {
            Action func = () => { };

            var constructor = typeof(DbContext).GetConstructor(Type.EmptyTypes);
            MockEngine.Mock(constructor, func);

            var context = new DbContext();

            ...
        }
}
```
