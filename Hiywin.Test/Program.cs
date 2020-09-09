using Hiywin.Common.IoC;
using Hiywin.IFrameManager;
using System;

namespace Hiywin.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var dd = Assembly.Load("Hiywin.IFrameService");

            IoCOperaters.Init();

            Console.WriteLine("请输入运行码：");

            if (Console.ReadLine() == "1")
            {
                TestIoC();
            }


            Console.Read();
        }

        static void TestIoC()
        {
            IModuleManager _manager = IoCContainer.Resolve<IModuleManager>();
            var result = _manager.GetModuleManager();

            Console.WriteLine($"{result.Name} 今年 {result.Age} 岁了！！！");

            Console.WriteLine("请输入运行码：");
            if (Console.ReadLine() == "1")
            {
                TestIoC();
            }
        }
    }
}
