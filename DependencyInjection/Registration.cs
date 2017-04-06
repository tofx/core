using TOF.Core.Utils;
using System;
using System.Collections.Generic;

namespace TOF.Core.DependencyInjection
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

        public Registration(Type RegisterType) : this()
        {
            ConcreteType = RegisterType;
        }

        public Registration As<T>()
        {
            return As(typeof(T));
        }

        public Registration As(Type MapType)
        {
            ParameterChecker.NotNull(MapType);

            if (!MapTypes.Contains(MapType))
                MapTypes.Add(MapType);

            return this;
        }

        public Registration AsDefault()
        {
            this.IsDefault = true;
            return this;
        }

        public Registration WithInstance(object Instance)
        {
            this.Instance = Instance;
            return this;
        }

        public Registration WithParameter(string Name, object Value)
        {
            return WithParameter(new NamedParameter(Name, Value));
        }

        public Registration WithParameter(Parameter Parameter)
        {
            this.ActivateParameters.Add(Parameter);
            return this;
        }

        public Registration AsSingleInstance()
        {
            this.IsSingleInstance = true;
            return this;
        }

        public Registration PropertiesAutowired()
        {
            this.IsPropertiesAutowired = true;
            return this;
        }

        public Registration OnActivating(Func<Container, IEnumerable<Parameter>, object> ActivatorFunc)
        {
            Activator = ActivatorFunc;
            return this;
        }
    }
}
