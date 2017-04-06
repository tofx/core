using tofx.Core.Utils;
using System;
using System.Collections.Generic;

namespace tofx.Core.DependencyInjection
{
    public class Registration
    {
        public Type ConcreteType { get; private set; }
        public List<Type> MapTypes { get; private set; }
        public bool IsDefault { get; private set; }
        public Func<Container, IEnumerable<Parameter>, object> Activator { get; private set; }
        public List<Parameter> ActivateParameters { get; private set; }
        public Action<Container> OnReleasing { get; private set; }
        public object Instance { get; private set; }
        public bool IsSingleInstance { get; private set; }
        public bool IsPropertiesAutowired { get; private set; }

        public Registration()
        {
            IsDefault = false;
            Instance = null;
            MapTypes = new List<Type>();
            ActivateParameters = new List<Parameter>();
        }

        public Registration(Type registerType) : this()
        {
            ConcreteType = registerType;
        }

        public Registration As<T>()
        {
            return As(typeof(T));
        }

        public Registration As(Type mapType)
        {
            ParameterChecker.NotNull(mapType);

            if (!MapTypes.Contains(mapType))
                MapTypes.Add(mapType);

            return this;
        }

        public Registration AsDefault()
        {
            IsDefault = true;
            return this;
        }

        public Registration WithInstance(object instance)
        {
            Instance = instance;
            return this;
        }

        public Registration WithParameter(string name, object value)
        {
            return WithParameter(new NamedParameter(name, value));
        }

        public Registration WithParameter(Parameter parameter)
        {
            ActivateParameters.Add(parameter);
            return this;
        }

        public Registration AsSingleInstance()
        {
            IsSingleInstance = true;
            return this;
        }

        public Registration PropertiesAutowired()
        {
            IsPropertiesAutowired = true;
            return this;
        }

        public Registration OnActivating(Func<Container, IEnumerable<Parameter>, object> activatorFunc)
        {
            Activator = activatorFunc;
            return this;
        }
    }
}
