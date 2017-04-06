using tofx.Core.Utils;
using System;
using System.Collections.Generic;

namespace tofx.Core.DependencyInjection
{
    public class Container
    {
        private RegistrationManager _registrationManager = null;
        private ObjectActivator _activator = null;

        public Container(Dictionary<Type, Registration> mapRegistrations)
        {
            ParameterChecker.NotNull(mapRegistrations);
            _registrationManager = new RegistrationManager(mapRegistrations);
            _activator = new ObjectActivator();
        }

        public T Resolve<T>(params object[] parameters)
        {
            var instance = Resolve(typeof(T), parameters);
            return (T)instance;
        }

        //public T ResolveGeneric<T>(Type[] GenericTypeParams, params object[] Parameters)
        //{
        //    var instance = Resolve(typeof(T), GenericTypeParams, Parameters);
        //    return (T)instance;
        //}

        public object Resolve(Type resolveType, params object[] Parameters)
        {
            var registration = _registrationManager.Find(resolveType);

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
                foreach (var property in resolveType.GetProperties())
                {
                    if (property.PropertyType.IsValueType)
                        continue;

                    if (property.PropertyType.IsInterface)
                    {
                        object propertyInstance = Resolve(property.PropertyType);

                        if (propertyInstance != null)
                            property.SetValue(instance, propertyInstance);
                    }
                }
            }

            return instance;
        }

        public object Resolve(Type resolveType, Type[] genericTypeParams, params object[] Parameters)
        {
            var registration = _registrationManager.Find(resolveType);

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

            object instance = _activator.CreateGeneric(this, registration, genericTypeParams, parameters.ToArray());

            if (registration.IsPropertiesAutowired)
            {
                foreach (var property in resolveType.GetProperties())
                {
                    if (property.PropertyType.IsValueType)
                        continue;

                    if (property.PropertyType.IsInterface)
                    {
                        object propertyInstance = Resolve(property.PropertyType);

                        if (propertyInstance != null)
                            property.SetValue(instance, propertyInstance);
                    }
                }
            }

            return instance;
        }

        public TGeneric ResolveGeneric<TGeneric>(Type[] genericTypes, params object[] parameters)
        {
            return (TGeneric)ResolveGeneric(typeof(TGeneric), genericTypes, parameters);
        }

        public object ResolveGeneric(Type resolveType, Type[] genericTypes, params object[] Parameters)
        {
            var registration = (genericTypes == null || genericTypes.Length == 0)
                ? _registrationManager.FindGeneric(resolveType)
                : _registrationManager.FindGenericDefinition(resolveType);

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

            object instance = (genericTypes == null || genericTypes.Length == 0)
                ? _activator.Create(this, registration, parameters.ToArray())
                : _activator.CreateGeneric(this, registration, genericTypes, parameters.ToArray());

            if (registration.IsPropertiesAutowired)
            {
                foreach (var property in resolveType.GetProperties())
                {
                    if (property.PropertyType.IsValueType)
                        continue;

                    if (property.PropertyType.IsInterface)
                    {
                        object propertyInstance = Resolve(property.PropertyType);

                        if (propertyInstance != null)
                            property.SetValue(instance, propertyInstance);
                    }
                }
            }

            return instance;
        }
    }
}
