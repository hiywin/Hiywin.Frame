﻿
using System;

namespace Hiywin.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //IoCBuild.Init();

            Console.WriteLine("请输入运行码：");

            //if (Console.ReadLine() == "1")
            //{
            //    TestIoC();
            //}


            Console.Read();
        }

        static void TestIoC()
        {
            //IModuleManager _manager = IoCContainer.Resolve<IModuleManager>();
            //var result = await _manager.GetModluleAllAsync(new Common.Data.QueryData<IFrameService.Structs.SysModuleQuery>());

            //Console.WriteLine($"{result.Data.ModuleName} 今年 {result.Data.ModuleNo} 岁了！！！");

            //Console.WriteLine("请输入运行码：");
            //if (Console.ReadLine() == "1")
            //{
            //    TestIoC();
            //}
        }
    }
}
