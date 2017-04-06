using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace TOF.Core.Utils.TypeExtensions
{
    public static class NameValueCollectionExtensions
    {
        public static IDictionary<string, string> ToDictionary(this NameValueCollection Collection)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (Collection == null)
                return dic;

            for (int i=0; i<Collection.Count; i++)
            {
                string key = Collection.Keys[i];
                string value = Collection[key];

                dic.Add(key, value);
            }

            return dic;
        }

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this NameValueCollection Collection)
            where TKey: IConvertible
            where TValue: IConvertible
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();
            var converterFactory = TypeConverterFactory.GetTypeConverterFactory();

            if (Collection == null)
                return dic;

            var keyConverter = converterFactory.GetConvertType<TKey>();
            var valueConverter = converterFactory.GetConvertType<TValue>();

            for (int i = 0; i < Collection.Count; i++)
            {
                string key = Collection.Keys[i];
                string value = Collection[key];

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
            this NameValueCollection Collection, Func<string, TKey> KeyConvertAction, Func<string, TValue> ValueConvertAction)
        {
            Dictionary<TKey, TValue> dic = new Dictionary<TKey, TValue>();

            if (Collection == null)
                return dic;

            for (int i = 0; i < Collection.Count; i++)
            {
                string key = Collection.Keys[i];
                string value = Collection[key];

                TKey keyCasted = KeyConvertAction(key);
                TValue valueCasted = ValueConvertAction(value);

                dic.Add(keyCasted, valueCasted);
            }

            return dic;
        }
    }
}
