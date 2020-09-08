using Hiywin.Common.IoC;
using Hiywin.Common.Linq;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hiywin.Utility
{
    public class IoCOperaters
    {
        private static List<string> _layerAssemblys;

        public static object Init()
        {
            _layerAssemblys = new List<string>();

            IoCAssemblyInit();

            IoCServicesInit();

            IoCManagersInit();

            IoCModelsInit();

            return IoCBuild();
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
                IoCContainer.Register(fileInfo.Name.Replace(".dll",string.Empty));

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
            
            IoCLayersInit(layersPath);
        }

        /// <summary>
        /// 业务层注入
        /// </summary>
        public static void IoCManagersInit()
        {
            var layersPath = AppContext.BaseDirectory + "ManagerLayers";

            IoCLayersInit(layersPath);
        }

        /// <summary>
        /// 实体层注入
        /// </summary>
        public static void IoCModelsInit()
        {
            var layersPath = AppContext.BaseDirectory + "EntityLayers";

            IoCLayersInit(layersPath);
        }

        /// <summary>
        /// 加载主目录、文件夹程序集，统一注入
        /// </summary>
        /// <param name="folderPath"></param>
        private static void IoCLayersInit(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            foreach (FileInfo fileInfo in folder.GetFiles("Hiywin.*.dll"))
            {
                var fileName = fileInfo.Name;
                var interfaceAssemblyName = string.Empty;

                var fileNameArray = fileName.Split('.');
                foreach (var tempName in fileNameArray)
                {
                    if (tempName.Contains("Service") || tempName.Contains("Manager"))
                    {
                        interfaceAssemblyName = fileName.Replace(tempName, "I" + tempName);
                    }
                    if (tempName.Contains("Models"))
                    {
                        interfaceAssemblyName = fileName.Replace(tempName, "Entities");
                    }
                }
                if (!string.IsNullOrEmpty(interfaceAssemblyName))
                {
                    IoCContainer.Register(fileInfo.FullName, interfaceAssemblyName);
                }                    
            }
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

        /// <summary>
        /// 复制文件到指定目录运行
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private static string PrepareShadowCopies(string folderPath)
        {
            // 准备 Shadow Copy 的目标目录
            var target = Path.Combine(AppContext.BaseDirectory, "app_data", "apps-cache");

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            // 找到插件目录下的 .dll，拷贝
            Directory.EnumerateDirectories(folderPath)
               .SelectMany(path => Directory.EnumerateFiles(path, "*.dll"))
               .ForEach(dll => File.Copy(dll, Path.Combine(target, Path.GetFileName(dll)), true));

            return target;
        }
    }
}
