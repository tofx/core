using TOF.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TOF.Core.DependencyInjection
{
    public class ObjectActivator
    {
        public object Create(Container Container, Registration Registration, params Parameter[] Parameters)
        {
            ParameterChecker.NotNull(Container);
            ParameterChecker.NotNull(Registration);
            
            object instance = null;

            if (Registration.Activator != null)
            {
                instance = Registration.Activator(
                    Container, 
                    BindingRegistrationParametersAndActivateParameters(Registration.ActivateParameters, Parameters));

                if (instance == null)
                    throw new ObjectActivatingFailedException("ActivatorReturnNull");
                else
                    return instance;
            }

            List<Type> parameterTypes = new List<Type>();
            List<object> parameterValues = new List<object>();

            if (Registration.ActivateParameters.Count > 0)
            {
                foreach (var parameter in Registration.ActivateParameters)
                {
                    var activatorParameter = parameter as Parameter;

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
            
            if (parameterTypes.Any() || (Parameters != null && Parameters.Count() > 0))
            {
                foreach (var parameter in Parameters)
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

                var constructor = Registration.ConcreteType.GetConstructor(parameterTypes.ToArray());

                if (constructor == null)
                    throw new ObjectActivatingFailedException("ObjectConstructorNotMatchToParameter");

                instance = constructor.Invoke(parameterValues.ToArray());
            }
            else
            {
                if (Registration.ConcreteType.GetConstructor(Type.EmptyTypes) == null)
                {
                    var constructorQuery = Registration.ConcreteType.GetConstructors()
                        .OrderByDescending(ctor => ctor.GetParameters().Length);

                    if (!constructorQuery.Any())
                        throw new ObjectActivatingFailedException("ObjectConstructorNotFound");

                    var ci = constructorQuery.First();
                    List<object> parameters = new List<object>();

                    foreach (var ciParam in ci.GetParameters())
                    {
                        if (ciParam.ParameterType.IsInterface)
                            parameters.Add(Container.Resolve(ciParam.ParameterType));
                        else
                        {
                            if (ciParam.ParameterType.IsValueType)
                                parameters.Add(Activator.CreateInstance(ciParam.ParameterType));
                            else
                            {
                                var concreteClassConstructor = ciParam.ParameterType.GetConstructor(Type.EmptyTypes);

                                if (concreteClassConstructor == null)
                                {
                                    throw new ObjectActivatingFailedException("SupportDefaultConstructorOnly");
                                }
                                else
                                {
                                    parameters.Add(Activator.CreateInstance(ciParam.ParameterType));
                                }
                            }
                        }
                    }

                    instance = ci.Invoke(parameters.ToArray());
                }
                else
                {
                    instance = Activator.CreateInstance(Registration.ConcreteType);
                }
            }

            return instance;
        }

        public object CreateGeneric(
            Container Container, Registration Registration, 
            Type[] GenericParameterTypes, params Parameter[] Parameters)
        {
            ParameterChecker.NotNull(Container);
            ParameterChecker.NotNull(Registration);
            ParameterChecker.NotNull(GenericParameterTypes);

            if (GenericParameterTypes.Length == 0)
                throw new ObjectActivatingFailedException("GenericTypeDefintionIsEmpty");

            Type generiedType = Registration.ConcreteType.MakeGenericType(GenericParameterTypes);

            object instance = null;

            if (Registration.Activator != null)
            {
                instance = Registration.Activator(
                    Container, 
                    BindingRegistrationParametersAndActivateParameters(Registration.ActivateParameters, Parameters));

                if (instance == null)
                    throw new ObjectActivatingFailedException("ActivatorReturnNull");
                else
                    return instance;
            }

            List<Type> parameterTypes = new List<Type>();
            List<object> parameterValues = new List<object>();

            if (Registration.ActivateParameters.Count > 0)
            {
                foreach (var parameter in Registration.ActivateParameters)
                {
                    var activatorParameter = parameter as Parameter;

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

            if (parameterTypes.Any() || (Parameters != null && Parameters.Count() > 0))
            {
                foreach (var parameter in Parameters)
                {
                    var activatorParameter = parameter as Parameter;
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
                        activatorParameter = parameter as Parameter;

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
                    List<object> parameters = new List<object>();

                    foreach (var ciParam in ci.GetParameters())
                    {
                        if (ciParam.ParameterType.IsInterface)
                            parameters.Add(Container.Resolve(ciParam.ParameterType));
                        else
                        {
                            if (ciParam.ParameterType.IsValueType)
                                parameters.Add(Activator.CreateInstance(ciParam.ParameterType));
                            else
                            {
                                var concreteClassConstructor = ciParam.ParameterType.GetConstructor(Type.EmptyTypes);

                                if (concreteClassConstructor == null)
                                {
                                    throw new ObjectActivatingFailedException("SupportDefaultConstructorOnly");
                                }
                                else
                                {
                                    parameters.Add(Activator.CreateInstance(ciParam.ParameterType));
                                }
                            }
                        }
                    }

                    instance = ci.Invoke(parameters.ToArray());
                }
                else
                {
                    instance = Activator.CreateInstance(generiedType);
                }
            }

            return instance;
        }

        private IEnumerable<Parameter> BindingRegistrationParametersAndActivateParameters(
            IEnumerable<Parameter> RegistrationParameters, IEnumerable<Parameter> ActivateParameters)
        {
            List<Parameter> parameters = new List<Parameter>();

            if (RegistrationParameters.Any())
            {
                foreach (var param in RegistrationParameters)
                {
                    int idx = parameters.FindIndex(p => p.GetValue().GetType() == param.GetValue().GetType());

                    if (idx >= 0)
                        parameters[idx] = param;
                    else
                        parameters.Add(param);
                }
            }

            if (ActivateParameters.Any())
            {
                foreach (var param in ActivateParameters)
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
