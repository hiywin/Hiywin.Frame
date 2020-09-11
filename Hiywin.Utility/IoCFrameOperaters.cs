using Hiywin.Common.IoC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hiywin.Utility
{
    public class IoCFrameOperaters
    {
        private static List<string> _assemblyArrays;
        private static List<string> _layerAssemblys;

        public static void Init()
        {
            _assemblyArrays = new List<string>();
            _layerAssemblys = new List<string>();

            IoCAssemblyInit();

            IoCModelsInit();    //必须先加载Models

            IoCManagersInit();

            IoCServicesInit();
        }

        /// <summary>
        /// 数据层注入
        /// </summary>
        private static void IoCServicesInit()
        {
            var layersPath = AppContext.BaseDirectory + "ServiceLayers";

            IoCContainer.RegisterLayers(layersPath);
        }

        /// <summary>
        /// 业务层注入
        /// </summary>
        private static void IoCManagersInit()
        {
            var layersPath = AppContext.BaseDirectory + "ManagerLayers";

            IoCContainer.RegisterLayers(layersPath);
        }

        /// <summary>
        /// 实体层注入
        /// </summary>
        private static void IoCModelsInit()
        {
            var layersPath = AppContext.BaseDirectory + "EntityLayers";

            IoCContainer.RegisterLayers(layersPath);
        }

        /// <summary>
        /// 主目录下程序集注入
        /// </summary>
        private static void IoCAssemblyInit()
        {
            var baseDir = AppContext.BaseDirectory;
            DirectoryInfo folder = new DirectoryInfo(baseDir);
            foreach (FileInfo fileInfo in folder.GetFiles("Hiywin.*.dll"))
            {
                _assemblyArrays.Add(fileInfo.Name.Replace(".dll", string.Empty));

                if (fileInfo.Name.Contains("Service")
                    || fileInfo.Name.Contains("Manager")
                    || fileInfo.Name.Contains("Entities"))
                {
                    _layerAssemblys.Add(fileInfo.FullName);
                }
            }
            if (_assemblyArrays.Count > 0)
            {
                IoCContainer.Register(_assemblyArrays.ToArray());
            }

        }
    }
}
