using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace tofx.Core.Utils.TypeExtensions
{
    public static class NameValueCollectionExtensions
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection collection)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (collection == null)
                return dic;

            for (int i=0; i<collection.Count; i++)
            {
                string key = collection.Keys[i];
                string value = collection[key];

                dic.Add(key, value);
            }

            return dic;
        }

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this NameValueCollection collection)
            where TKey: IConvertible
            where TValue: IConvertible
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            var converterFactory = TypeConverterFactory.GetTypeConverterFactory();

            if (collection == null)
                return dic;

            var keyConverter = converterFactory.GetConvertType<TKey>();
            var valueConverter = converterFactory.GetConvertType<TValue>();

            for (int i = 0; i < collection.Count; i++)
            {
                string key = collection.Keys[i];
                string value = collection[key];

                TKey keyCasted = (keyConverter != null)
                    ? (TKey)keyConverter.Convert(key)
                    : (TKey)Convert.ChangeType(key, typeof(TKey));
                TValue valueCasted = (valueConverter != null)
                    ? (TValue)valueConverter.Convert(value)
                    : (TValue)Convert.ChangeType(value, typeof(TValue));

                dic.Add(keyCasted, valueCasted);
            }

            return dic;
        }


        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this NameValueCollection collection, Func<string, TKey> keyConvertAction, Func<string, TValue> valueConvertAction)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();

            if (collection == null)
                return dic;

            for (int i = 0; i < collection.Count; i++)
            {
                string key = collection.Keys[i];
                string value = collection[key];

                TKey keyCasted = keyConvertAction(key);
                TValue valueCasted = valueConvertAction(value);

                dic.Add(keyCasted, valueCasted);
            }

            return dic;
        }
    }
}
