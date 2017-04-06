using tofx.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tofx.Core.DependencyInjection
{
    public class ObjectActivator
    {
        public object Create(Container container, Registration registration, params Parameter[] parameters)
        {
            ParameterChecker.NotNull(container);
            ParameterChecker.NotNull(registration);
            
            object instance = null;

            if (registration.Activator != null)
            {
                instance = registration.Activator(
                    container, 
                    BindingRegistrationParametersAndActivateParameters(registration.ActivateParameters, parameters));

                if (instance == null)
                    throw new ObjectActivatingFailedException("ActivatorReturnNull");
                else
                    return instance;
            }

            var parameterTypes = new List<Type>();
            var parameterValues = new List<object>();

            if (registration.ActivateParameters.Count > 0)
            {
                foreach (var parameter in registration.ActivateParameters)
                {
                    var activatorParameter = parameter;

                    if (activatorParameter.CanProvideValue())
                    {
                        parameterTypes.Add(activatorParameter.GetValue().GetType());
                        parameterValues.Add(activatorParameter.GetValue());
                    }
                    else
                    {
                        parameterTypes.Add(activatorParameter.GetValue().GetType());
                        parameterValues.Add(null);
                    }
                }

            }
            
            if (parameterTypes.Any() || (parameters != null && parameters.Any()))
            {
                foreach (var parameter in parameters)
                {
                    var activatorParameter = parameter as Parameter;
                    //var paramQuery = parameterTypes.Where(t => t == parameter.GetValue().GetType());

                    //if (paramQuery.Any())
                    //{
                    //    if (activatorParameter.CanProvideValue())
                    //    {
                    //        int idx = parameterTypes.FindIndex(t => t ==parameter.GetValue().GetType());

                    //        if (idx >= 0)
                    //            parameterValues[idx] = activatorParameter.GetValue();
                    //    }
                    //}
                    //else
                    //{
                        if (activatorParameter.CanProvideValue())
                        {
                            parameterTypes.Add(activatorParameter.GetValue().GetType());
                            parameterValues.Add(activatorParameter.GetValue());
                        }
                        else
                        {
                            parameterTypes.Add(activatorParameter.GetValue().GetType());
                            parameterValues.Add(null);
                        }
                    //}
                }

                var constructor = registration.ConcreteType.GetConstructor(parameterTypes.ToArray());

                if (constructor == null)
                    throw new ObjectActivatingFailedException("ObjectConstructorNotMatchToParameter");

                instance = constructor.Invoke(parameterValues.ToArray());
            }
            else
            {
                if (registration.ConcreteType.GetConstructor(Type.EmptyTypes) == null)
                {
                    var constructorQuery = registration.ConcreteType.GetConstructors()
                        .OrderByDescending(ctor => ctor.GetParameters().Length);

                    if (!constructorQuery.Any())
                        throw new ObjectActivatingFailedException("ObjectConstructorNotFound");

                    var ci = constructorQuery.First();
                    List<object> constructorParams = new List<object>();

                    foreach (var ciParam in ci.GetParameters())
                    {
                        if (ciParam.ParameterType.IsInterface)
                            constructorParams.Add(container.Resolve(ciParam.ParameterType));
                        else
                        {
                            if (ciParam.ParameterType.IsValueType)
                                constructorParams.Add(Activator.CreateInstance(ciParam.ParameterType));
                            else
                            {
                                var concreteClassConstructor = ciParam.ParameterType.GetConstructor(Type.EmptyTypes);

                                if (concreteClassConstructor == null)
                                    throw new ObjectActivatingFailedException("SupportDefaultConstructorOnly");

                                constructorParams.Add(Activator.CreateInstance(ciParam.ParameterType));
                            }
                        }
                    }

                    instance = ci.Invoke(constructorParams.ToArray());
                }
                else
                {
                    instance = Activator.CreateInstance(registration.ConcreteType);
                }
            }

            return instance;
        }

        public object CreateGeneric(
            Container container, Registration registration, 
            Type[] genericParameterTypes, params Parameter[] parameters)
        {
            ParameterChecker.NotNull(container);
            ParameterChecker.NotNull(registration);
            ParameterChecker.NotNull(genericParameterTypes);

            if (genericParameterTypes.Length == 0)
                throw new ObjectActivatingFailedException("GenericTypeDefintionIsEmpty");

            var generiedType = registration.ConcreteType.MakeGenericType(genericParameterTypes);

            object instance;

            if (registration.Activator != null)
            {
                instance = registration.Activator(
                    container, 
                    BindingRegistrationParametersAndActivateParameters(registration.ActivateParameters, parameters));

                if (instance == null)
                    throw new ObjectActivatingFailedException("ActivatorReturnNull");

                return instance;
            }

            var parameterTypes = new List<Type>();
            var parameterValues = new List<object>();

            if (registration.ActivateParameters.Count > 0)
            {
                foreach (var parameter in registration.ActivateParameters)
                {
                    var activatorParameter = parameter;

                    if (activatorParameter.CanProvideValue())
                    {
                        parameterTypes.Add(activatorParameter.GetValue().GetType());
                        parameterValues.Add(activatorParameter.GetValue());
                    }
                    else
                    {
                        parameterTypes.Add(activatorParameter.GetValue().GetType());
                        parameterValues.Add(null);
                    }
                }

            }

            if (parameterTypes.Any() || (parameters != null && parameters.Any()))
            {
                foreach (var parameter in parameters)
                {
                    var activatorParameter = parameter;
                    //var paramQuery = parameterTypes.Where(t => t == parameter.GetValue().GetType());

                    //if (paramQuery.Any())
                    //{
                    //    if (activatorParameter.CanProvideValue())
                    //    {
                    //        int idx = parameterTypes.FindIndex(t => t == parameter.GetValue().GetType());

                    //        if (idx >= 0)
                    //            parameterValues[idx] = activatorParameter.GetValue();
                    //    }
                    //}
                    //else
                    //{
                        //activatorParameter = parameter;

                        if (activatorParameter.CanProvideValue())
                        {
                            parameterTypes.Add(activatorParameter.GetValue().GetType());
                            parameterValues.Add(activatorParameter.GetValue());
                        }
                        else
                        {
                            parameterTypes.Add(activatorParameter.GetValue().GetType());
                            parameterValues.Add(null);
                        }
                    //}
                }

                var constructor = generiedType.GetConstructor(parameterTypes.ToArray());

                if (constructor == null)
                    throw new ObjectActivatingFailedException("ObjectConstructorNotMatchToParameter");

                instance = constructor.Invoke(parameterValues.ToArray());
            }
            else
            {
                if (generiedType.GetConstructor(Type.EmptyTypes) == null)
                {
                    var constructorQuery = generiedType.GetConstructors()
                        .OrderByDescending(ctor => ctor.GetParameters().Length);

                    if (!constructorQuery.Any())
                        throw new ObjectActivatingFailedException("ObjectConstructorNotFound");

                    var ci = constructorQuery.First();
                    var constructorParams = new List<object>();

                    foreach (var ciParam in ci.GetParameters())
                    {
                        if (ciParam.ParameterType.IsInterface)
                            constructorParams.Add(container.Resolve(ciParam.ParameterType));
                        else
                        {
                            if (ciParam.ParameterType.IsValueType)
                                constructorParams.Add(Activator.CreateInstance(ciParam.ParameterType));
                            else
                            {
                                var concreteClassConstructor = ciParam.ParameterType.GetConstructor(Type.EmptyTypes);

                                if (concreteClassConstructor == null)
                                {
                                    throw new ObjectActivatingFailedException("SupportDefaultConstructorOnly");
                                }

                                constructorParams.Add(Activator.CreateInstance(ciParam.ParameterType));
                            }
                        }
                    }

                    instance = ci.Invoke(constructorParams.ToArray());
                }
                else
                {
                    instance = Activator.CreateInstance(generiedType);
                }
            }

            return instance;
        }

        private static IEnumerable<Parameter> BindingRegistrationParametersAndActivateParameters(
            IEnumerable<Parameter> registrationParameters, IEnumerable<Parameter> activateParameters)
        {
            var parameters = new List<Parameter>();

            if (registrationParameters.Any())
            {
                foreach (var param in registrationParameters)
                {
                    int idx = parameters.FindIndex(p => p.GetValue().GetType() == param.GetValue().GetType());

                    if (idx >= 0)
                        parameters[idx] = param;
                    else
                        parameters.Add(param);
                }
            }

            if (activateParameters.Any())
            {
                foreach (var param in activateParameters)
                {
                    int idx = parameters.FindIndex(p => p.GetValue().GetType() == param.GetValue().GetType());

                    if (idx >= 0)
                        parameters[idx] = param;
                    else
                        parameters.Add(param);
                }
            }

            return parameters;
        }
    }
}
