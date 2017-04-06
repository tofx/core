using tofx.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace tofx.Core.DependencyInjection
{
    public class ContainerBuilder
    {
        private Dictionary<Type, Registration> _mapRegistrations = null;

        public ContainerBuilder()
        {
            _mapRegistrations = new Dictionary<Type, Registration>();              
        }
        
        public Registration Register<TMapType>() where TMapType : class
        {
            Type mapType = typeof(TMapType);
            return RegisterType(mapType);
        }

        public Registration RegisterType(Type mapType)
        {
            ParameterChecker.NotNull(mapType);

            // exist registration.
            if (_mapRegistrations.ContainsKey(mapType))
                return _mapRegistrations[mapType];

            // new registration.
            Registration registration = new Registration(mapType);
            _mapRegistrations.Add(mapType, registration);

            return registration;
        }

        public void RegisterTypesInLibs(string libraryPath, string libraryExtension = "*.dll", bool loadFromStartup = true)
        {
            var files = Directory.GetFiles(libraryPath, libraryExtension);

            if (!files.Any())
                return;

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                RegisterTypesInAssembly(assembly, loadFromStartup);
            }
        }

        public void RegisterTypesInAssemblies(string assemblyPath, bool loadFromStartup = true)
        {
            var files = Directory.GetFiles(assemblyPath);

            if (!files.Any())
                return;

            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);

                    if (loadFromStartup)
                        LoadFromStartup(assembly);
                    else
                        RegisterTypesInAssembly(assembly, loadFromStartup);
                }
                catch (FileLoadException)
                {
                    // skip load failed exception.
                }
                catch (BadImageFormatException)
                {
                    // skip load failed exception.
                }
            }
        }

        public void RegisterTypesInAssemblies(IEnumerable<Assembly> assemblies, bool loadFromStartup = true)
        {
            ParameterChecker.NotNull(assemblies);

            if (!assemblies.Any())
                return;

            foreach (var assembly in assemblies)
                RegisterTypesInAssembly(assembly, loadFromStartup);
        }

        public void RegisterTypesInAssembly(Assembly assembly, bool loadFromStartup = true)
        {
            ParameterChecker.NotNull(assembly);

            try
            {
                if (loadFromStartup)
                {
                    LoadFromStartup(assembly);
                }
                else
                {
                    var types = assembly.GetTypes().Where(t => !t.IsPublic && t.IsClass);

                    foreach (var type in types)
                    {
                        var interfaces = type.GetInterfaces();

                        foreach (var interfaceType in interfaces)
                            RegisterType(type).As(interfaceType);
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
                // skip load exception.
            }
        }

        public Container Build()
        {
            var container = Activator.CreateInstance(typeof(Container), _mapRegistrations);
            return (Container)container;
        }

        private void LoadFromStartup(Assembly assembly)
        {
            var startupAttributes =
                assembly.GetCustomAttributes(typeof(StartupAttribute), false) as StartupAttribute[];

            if (startupAttributes != null && startupAttributes.Length > 0)
            {
                foreach (var startupAttribute in startupAttributes)
                {
                    object startupInstance = Activator.CreateInstance(startupAttribute.StartupType);
                    MethodInfo method = startupAttribute.StartupType.GetMethod(startupAttribute.StartupMethod);

                    if (startupInstance == null)
                        continue;
                    if (method == null)
                        continue;

                    method.Invoke(startupInstance, new object[] { this });
                }
            }
        }
    }
}
