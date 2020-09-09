using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hiywin.Common.IoC
{
    public class IoCContainer
    {
        private static ContainerBuilder _builder = new ContainerBuilder();
        private static IContainer _container;
        private static string[] _otherAssembly;
        private static string[] _otherAssemblyFrom;
        private static List<Type> _types = new List<Type>();
        private static Dictionary<Type, Type> _dicType = new Dictionary<Type, Type>();

        public static IContainer InitAutofac(IServiceCollection services)
        {
            return _container;
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="assemblies">程序集名称的集合</param>
        public static void Register(params string[] assemblies)
        {
            _otherAssembly = assemblies;
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="assemblies">程序集名称的集合，全路径</param>
        public static void RegisterFrom(params string[] assemblies)
        {
            _otherAssemblyFrom = assemblies;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="types"></param>
        public static void Register(params Type[] types)
        {
            _types.AddRange(types.ToList());
        }

        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="implementationAssemblyName">接口实现程序集</param>
        /// <param name="interfaceAssemblyName">接口程序集</param>
        public static void Register(string implementationAssemblyFullName, string interfaceAssemblyName)
        {
            interfaceAssemblyName = interfaceAssemblyName.Replace(".dll", string.Empty);
            var implementactionAssembly = Assembly.LoadFrom(implementationAssemblyFullName);
            var interfaceAssembly = Assembly.Load(interfaceAssemblyName);
            var implementationTypes =
                implementactionAssembly.DefinedTypes.Where(t =>
                t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsNested);
            foreach (var type in implementationTypes)
            {
                var typeNameSpace = type.Namespace;

                if (interfaceAssemblyName.Contains("Entities") && typeNameSpace.Contains("Models"))
                {
                    interfaceAssemblyName = typeNameSpace.Replace("Models", "Entities");
                }
                var interfaceTypeName = interfaceAssemblyName + ".I" + type.Name;
                var interfaceType = interfaceAssembly.GetType(interfaceTypeName);
                if (interfaceType != null)
                {
                    if (interfaceType.IsAssignableFrom(type))
                    {
                        _dicType.Add(interfaceType, type);
                    }
                }
            }
        }

        /// <summary>
        /// 加载主目录、文件夹程序集，统一注入
        /// </summary>
        /// <param name="folderPath"></param>
        public static void RegisterLayers(string folderPath)
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
        /// 注册程序集
        /// </summary>
        /// <param name="assemblyName">程序集</param>
        /// <param name="namespaceName">命名空间</param>
        public static void RegisterNew(string assemblyName, string namespaceName)
        {
            var implementationAssembly = Assembly.Load(assemblyName);

            var interfaceAssembly = implementationAssembly;
            var implementationTypes =
               implementationAssembly.DefinedTypes.Where(t =>
               t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsNested).OrderBy(t => t.Namespace);
            foreach (var type in implementationTypes)
            {
                var interfaceTypeName = string.Empty;
                if (type.Namespace != null)
                {
                    if (type.Namespace.Contains("Services") && type.Namespace.Contains("Hiywin"))
                    {
                        interfaceTypeName = namespaceName + ".IServices.I" + type.Name;
                    }
                    if (type.Namespace.Contains("Managers") && type.Namespace.Contains("Hiywin"))
                    {
                        interfaceTypeName = namespaceName + ".IManagers.I" + type.Name;
                    }
                    if (!string.IsNullOrEmpty(interfaceTypeName) && !interfaceTypeName.Contains("OperationLogEvent"))
                    {
                        var interfaceType = interfaceAssembly.GetType(interfaceTypeName);
                        if (interfaceType != null)
                        {
                            if (interfaceType.IsAssignableFrom(type))
                            {
                                _dicType.Add(interfaceType, type);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 从指定路径注册程序集
        /// </summary>
        /// <param name="assemblyName">完整路径程序集</param>
        /// <param name="namespaceName">命名空间</param>
        public static void RegisterLoadFrom(string assemblyName, string namespaceName)
        {
            var implementationAssembly = Assembly.LoadFrom(assemblyName);

            var interfaceAssembly = implementationAssembly;
            var implementationTypes =
                implementationAssembly.DefinedTypes.Where(t =>
                t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsNested).OrderBy(t => t.Namespace);
            var applicationNamespace = string.Empty;
            foreach (var type in implementationTypes)
            {
                var interfaceTypeName = string.Empty;
                if (type.Namespace.Contains("Services"))
                {
                    interfaceTypeName = namespaceName + ".IServices.I" + type.Name;
                }
                if (type.Namespace.Contains("Managers"))
                {
                    interfaceTypeName = namespaceName + ".IManagers.I" + type.Name;
                }
                if (!string.IsNullOrEmpty(interfaceTypeName))
                {
                    var interfaceType = interfaceAssembly.GetType(interfaceTypeName);
                    if (interfaceType != null)
                    {
                        if (interfaceType.IsAssignableFrom(type))
                        {
                            _dicType.Add(interfaceType, type);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 注册类
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        public static void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _dicType.Add(typeof(TInterface), typeof(TImplementation));
        }

        /// <summary>
        /// 注册一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void Register<T>(T instance) where T : class
        {
            _builder.RegisterInstance(instance).SingleInstance();
        }

        /// <summary>
        /// 构建IOC容器，需在各种Register后调用
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IContainer Build()
        {
            if (_otherAssembly != null)
            {
                foreach (var item in _otherAssembly)
                {
                    _builder.RegisterAssemblyTypes(Assembly.Load(item));
                }
            }
            if (_otherAssemblyFrom != null)
            {
                foreach (var item in _otherAssemblyFrom)
                {
                    _builder.RegisterAssemblyTypes(Assembly.LoadFrom(item));
                }
            }
            if (_types != null)
            {
                foreach (var type in _types)
                {
                    _builder.RegisterType(type);
                }
            }
            if (_dicType != null)
            {
                foreach (var dicType in _dicType)
                {
                    _builder.RegisterType(dicType.Value).As(dicType.Key);
                }
            }

            _container = _builder.Build();

            return _container;
        }

        /// <summary>
        /// 构建IOC容器，需在各种Register后调用
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider Build(IServiceCollection services)
        {
            if (_otherAssembly != null)
            {
                foreach (var item in _otherAssembly)
                {
                    _builder.RegisterAssemblyTypes(Assembly.Load(item));
                }
            }
            if (_otherAssemblyFrom != null)
            {
                foreach (var item in _otherAssemblyFrom)
                {
                    _builder.RegisterAssemblyTypes(Assembly.LoadFrom(item));
                }
            }
            if (_types != null)
            {
                foreach (var type in _types)
                {
                    _builder.RegisterType(type);
                }
            }
            if (_dicType != null)
            {
                foreach (var dicType in _dicType)
                {
                    _builder.RegisterType(dicType.Value).As(dicType.Key);
                }
            }

            _builder.Populate(services);
            _container = _builder.Build();

            //第三方IOC接管.Net Core内置DI容器
            return new AutofacServiceProvider(_container);
        }

        /// <summary>
        /// 从容器中获取对象 Resolve an instance of the default requested type from the container
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
