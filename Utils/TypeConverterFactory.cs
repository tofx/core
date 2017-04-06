using TOF.Core.Abstractions;
using TOF.Core.Utils.TypeConverters;
using System;
using System.Collections.Generic;

namespace TOF.Core.Utils
{
    public class TypeConverterFactory
    {
        private Dictionary<Type, ITypeConverter> _converters = null;

        public TypeConverterFactory(IEnumerable<ITypeConverter> Converters)
        {
            ParameterChecker.NotNull(Converters);

            _converters = new Dictionary<Type, ITypeConverter>();

            foreach (var converter in Converters)
                _converters.Add(converter.GetCompatibleType(), converter);
        }

        public static TypeConverterFactory GetTypeConverterFactory()
        {
            var converters = new List<ITypeConverter>();

            // register built-in type converters.
            converters.Add(new IntegerConverter());
            converters.Add(new LongConverter());
            converters.Add(new ShortConverter());
            converters.Add(new FloatConverter());
            converters.Add(new DoubleConverter());
            converters.Add(new DecimalConverter());
            converters.Add(new BooleanConverter());
            converters.Add(new CharConverter());
            converters.Add(new DateTimeConverter());
            converters.Add(new StringConverter());
            converters.Add(new ByteArrayConverter());
            converters.Add(new GuidConverter());

            return new TypeConverterFactory(converters);
        }

        public ITypeConverter GetConvertType<T>()
        {
            if (typeof(T).IsEnum)
                return new EnumConverter(typeof(T));

            if (_converters.ContainsKey(typeof(T)))
                return _converters[typeof(T)];

            return null;
        }

        public ITypeConverter GetConvertType(Type T)
        {
            if (T.IsEnum)
                return new EnumConverter(T);

            if (_converters.ContainsKey(T))
                return _converters[T];

            return null;
        }
    }
}
