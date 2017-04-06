using TOF.Core.Utils;
using System;
using System.Collections.Generic;

namespace TOF.Core.DependencyInjection
{
    public static class ParameterExtensions
    {
        public static T Named<T>(this IEnumerable<Parameter> Parameters, string Name)
        {
            T value = default(T);
            var converterFactory = TypeConverterFactory.GetTypeConverterFactory();

            foreach (var parameter in Parameters)
            {
                if (parameter is NamedParameter)
                {
                    NamedParameter namedParameter = parameter as NamedParameter;

                    if (namedParameter.Name != Name)
                        continue;

                    object pv = parameter.GetValue();

                    if (typeof(T).IsValueType)
                    {
                        var converter = converterFactory.GetConvertType(pv.GetType());

                        if (converter == null)
                            throw new InvalidOperationException("UnableToCastValueTypeData");

                        value = (T)converter.Convert(pv);
                    }
                    else
                        value = (T)pv;

                    break;
                }
            }

            return value;
        }

        public static T TypeAs<T>(this IEnumerable<Parameter> Parameters)
        {
            T value = default(T);

            foreach (var parameter in Parameters)
            {
                if (parameter is TypedParameter)
                {
                    TypedParameter tp = parameter as TypedParameter;

                    if (tp.Type == typeof(T))
                    {
                        value = (T)tp.GetValue();
                        break;
                    }
                }
            }

            return value;
        }
    }
}
