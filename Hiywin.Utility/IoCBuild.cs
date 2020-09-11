using Hiywin.Common.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Utility
{
    public class IoCBuild
    {
        /// <summary>
        /// 构建IoC容器
        /// </summary>
        /// <param name="serviceDescriptors"></param>
        /// <returns></returns>
        public static object Init(IServiceCollection serviceDescriptors = null)
        {
            // 注入依赖
            IoCFrameOperaters.Init();
            IoCJwtOpraters.Init();

            if (serviceDescriptors == null)
            {
                return IoCContainer.Build();
            }
            else
            {
                return IoCContainer.Build(serviceDescriptors);
            }
        }
    }
}
