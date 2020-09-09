using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hiywin.Common.IoC
{
    public class IoCOperaters
    {
        private static List<string> _layerAssemblys;

        public static object Init(IServiceCollection serviceDescriptors = null)
        {
            _layerAssemblys = new List<string>();

            IoCAssemblyInit();

            IoCServicesInit();

            IoCManagersInit();

            IoCModelsInit();

            return IoCBuild(serviceDescriptors);
        }

        /// <summary>
        /// 主目录下程序集注入
        /// </summary>
        public static void IoCAssemblyInit()
        {
            var baseDir = AppContext.BaseDirectory;
            DirectoryInfo folder = new DirectoryInfo(baseDir);
            foreach (FileInfo fileInfo in folder.GetFiles("Hiywin.*.dll"))
            {
                IoCContainer.Register(fileInfo.Name.Replace(".dll", string.Empty));

                if (fileInfo.Name.Contains("Service")
                    || fileInfo.Name.Contains("Manager")
                    || fileInfo.Name.Contains("Entities"))
                {
                    _layerAssemblys.Add(fileInfo.FullName);
                }
            }
        }

        /// <summary>
        /// 数据层注入
        /// </summary>
        public static void IoCServicesInit()
        {
            var layersPath = AppContext.BaseDirectory + "ServiceLayers";

            IoCContainer.RegisterLayers(layersPath);
        }

        /// <summary>
        /// 业务层注入
        /// </summary>
        public static void IoCManagersInit()
        {
            var layersPath = AppContext.BaseDirectory + "ManagerLayers";

            IoCContainer.RegisterLayers(layersPath);
        }

        /// <summary>
        /// 实体层注入
        /// </summary>
        public static void IoCModelsInit()
        {
            var layersPath = AppContext.BaseDirectory + "EntityLayers";

            IoCContainer.RegisterLayers(layersPath);
        }

        /// <summary>
        /// 构建IoC容器
        /// </summary>
        /// <param name="serviceDescriptors"></param>
        /// <returns></returns>
        public static object IoCBuild(IServiceCollection serviceDescriptors = null)
        {
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
