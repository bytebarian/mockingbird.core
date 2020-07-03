# mockingbird.core
Mocking framework for C# language and .net core which enable to mock any kind of method including virtual, non-virtual, private, static and even constructors. 
Mocking is obtained thatnks to JIT compiler hooking and IL injection.

![how to hook jit compiler](https://github.com/bytebarian/mockingbird.core/blob/master/hook.PNG)

## Usage
First of all you need to initialize mocking engine. It may take a while for the first time.
```c#
MockEngine.Initialize();
```
After that you can use it like that
```c#
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
        
        ...
        
            var act = (a, b) =>
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

            var testAfter = new Test();
            var resultAfter = testAfter.GetLargerNumber(1, 2);
```
