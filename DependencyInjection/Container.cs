using TOF.Core.Utils;
using System;
using System.Collections.Generic;

namespace TOF.Core.DependencyInjection
{
    public class Container
    {
        private RegistrationManager _registrationManager = null;
        private ObjectActivator _activator = null;

        public Container(Dictionary<Type, Registration> MapRegistrations)
        {
            ParameterChecker.NotNull(MapRegistrations);
            _registrationManager = new RegistrationManager(MapRegistrations);
            _activator = new ObjectActivator();
        }

        public T Resolve<T>(params object[] Parameters)
        {
            var instance = Resolve(typeof(T), Parameters);
            return (T)instance;
        }

        //public T ResolveGeneric<T>(Type[] GenericTypeParams, params object[] Parameters)
        //{
        //    var instance = Resolve(typeof(T), GenericTypeParams, Parameters);
        //    return (T)instance;
        //}

        public object Resolve(Type ResolveType, params object[] Parameters)
        {
            var registration = _registrationManager.Find(ResolveType);

            if (registration == null)
                throw new TypeResolveFailedException();

            List<Parameter> parameters = new List<Parameter>();

            if (Parameters.Length > 0)
            {
                foreach (var p in Parameters)
                {
                    if (p is Parameter)
                        parameters.Add(p as Parameter);
                    else
                        parameters.Add(new ValueParameter(p));
                }
            }

            object instance = _activator.Create(this, registration, parameters.ToArray());

            if (registration.IsPropertiesAutowired)
            {
                foreach (var property in ResolveType.GetProperties())
                {
                    if (property.PropertyType.IsValueType)
                        continue;

                    if (property.PropertyType.IsInterface)
                    {
                        object propertyInstance = this.Resolve(property.PropertyType);

                        if (propertyInstance != null)
                            property.SetValue(instance, propertyInstance);
                    }
                }
            }

            return instance;
        }

        public object Resolve(Type ResolveType, Type[] GenericTypeParams, params object[] Parameters)
        {
            var registration = _registrationManager.Find(ResolveType);

            if (registration == null)
                throw new TypeResolveFailedException();

            if (!registration.ConcreteType.IsGenericTypeDefinition)
                throw new TypeResolveFailedException();

            List<Parameter> parameters = new List<Parameter>();

            if (Parameters.Length > 0)
            {
                foreach (var p in Parameters)
                {
                    if (p is Parameter)
                        parameters.Add(p as Parameter);
                    else
                        parameters.Add(new ValueParameter(p));
                }
            }

            object instance = _activator.CreateGeneric(this, registration, GenericTypeParams, parameters.ToArray());

            if (registration.IsPropertiesAutowired)
            {
                foreach (var property in ResolveType.GetProperties())
                {
                    if (property.PropertyType.IsValueType)
                        continue;

                    if (property.PropertyType.IsInterface)
                    {
                        object propertyInstance = this.Resolve(property.PropertyType);

                        if (propertyInstance != null)
                            property.SetValue(instance, propertyInstance);
                    }
                }
            }

            return instance;
        }

        public TGeneric ResolveGeneric<TGeneric>(Type[] GenericTypes, params object[] Parameters)
        {
            return (TGeneric)ResolveGeneric(typeof(TGeneric), GenericTypes, Parameters);
        }

        public object ResolveGeneric(Type ResolveType, Type[] GenericTypes, params object[] Parameters)
        {
            var registration = (GenericTypes == null || GenericTypes.Length == 0)
                ? _registrationManager.FindGeneric(ResolveType)
                : _registrationManager.FindGenericDefinition(ResolveType);

            if (registration == null)
                throw new TypeResolveFailedException();

            List<Parameter> parameters = new List<Parameter>();

            if (Parameters.Length > 0)
            {
                foreach (var p in Parameters)
                {
                    if (p is Parameter)
                        parameters.Add(p as Parameter);
                    else
                        parameters.Add(new ValueParameter(p));
                }
            }

            object instance = (GenericTypes == null || GenericTypes.Length == 0)
                ? _activator.Create(this, registration, parameters.ToArray())
                : _activator.CreateGeneric(this, registration, GenericTypes, parameters.ToArray());

            if (registration.IsPropertiesAutowired)
            {
                foreach (var property in ResolveType.GetProperties())
                {
                    if (property.PropertyType.IsValueType)
                        continue;

                    if (property.PropertyType.IsInterface)
                    {
                        object propertyInstance = this.Resolve(property.PropertyType);

                        if (propertyInstance != null)
                            property.SetValue(instance, propertyInstance);
                    }
                }
            }

            return instance;
        }
    }
}
