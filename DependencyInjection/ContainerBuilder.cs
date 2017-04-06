using TOF.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TOF.Core.DependencyInjection
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

        public Registration RegisterType(Type MapType)
        {
            ParameterChecker.NotNull(MapType);

            // exist registration.
            if (_mapRegistrations.ContainsKey(MapType))
                return _mapRegistrations[MapType];

            // new registration.
            Registration registration = new Registration(MapType);
            _mapRegistrations.Add(MapType, registration);

            return registration;
        }

        public void RegisterTypesInLibs(string LibraryPath, string LibraryExtension = "*.dll", bool LoadFromStartup = true)
        {
            var files = Directory.GetFiles(LibraryPath, LibraryExtension);

            if (!files.Any())
                return;

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);
                RegisterTypesInAssembly(assembly, LoadFromStartup);
            }
        }

        public void RegisterTypesInAssemblies(string AssemblyPath, bool LoadFromStartup = true)
        {
            var files = Directory.GetFiles(AssemblyPath);

            if (!files.Any())
                return;

            foreach (var file in files)
            {
                try
                {
                    var assembly = Assembly.LoadFile(file);

                    if (LoadFromStartup)
                        this.LoadFromStartup(assembly);
                    else
                        RegisterTypesInAssembly(assembly, LoadFromStartup);
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

        public void RegisterTypesInAssemblies(IEnumerable<Assembly> Assemblies, bool LoadFromStartup = true)
        {
            ParameterChecker.NotNull(Assemblies);

            if (!Assemblies.Any())
                return;

            foreach (var assembly in Assemblies)
                RegisterTypesInAssembly(assembly, LoadFromStartup);
        }

        public void RegisterTypesInAssembly(Assembly Assembly, bool LoadFromStartup = true)
        {
            ParameterChecker.NotNull(Assembly);

            try
            {
                if (LoadFromStartup)
                {
                    this.LoadFromStartup(Assembly);
                }
                else
                {
                    var types = Assembly.GetTypes().Where(t => !t.IsPublic && t.IsClass);

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

        private void LoadFromStartup(Assembly Assembly)
        {
            var startupAttributes =
                Assembly.GetCustomAttributes(typeof(StartupAttribute), false) as StartupAttribute[];

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
