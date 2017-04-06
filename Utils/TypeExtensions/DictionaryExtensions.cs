using System;
using System.Collections.Generic;

namespace tofx.Core.Utils.TypeExtensions
{
    public static class DictionaryExtensions
    {
        public static void Merge<TKey, TValue, TMergeKey, TMergeValue>(
            this IDictionary<TKey, TValue> collection, IEnumerable<KeyValuePair<TMergeKey, TMergeValue>> collectionMerge, 
            bool throwIfKeyExists = false, bool overwriteExists = true)
        {
            if (collectionMerge == null)
                throw new ArgumentNullException("collectionMerge", "E_MERGE_COLLECTION_NOT_FOUND");

            var enumerator = collectionMerge.GetEnumerator();
            var converterFactory = TypeConverterFactory.GetTypeConverterFactory();
            var keyConverter = converterFactory.GetConvertType<TKey>();
            var valueConverter = converterFactory.GetConvertType<TValue>();
            
            foreach (var item in collectionMerge)
            {
                TKey mergeKey = (keyConverter != null) 
                    ? (TKey)keyConverter.Convert(item.Key) 
                    : (TKey)Convert.ChangeType(item.Key, typeof(TKey));
                TValue mergeValue = (valueConverter != null)
                    ? (TValue)valueConverter.Convert(item.Value) 
                    : (TValue)Convert.ChangeType(item.Value, typeof(TValue));

                if (collection.ContainsKey(mergeKey))
                {
                    if (throwIfKeyExists)
                        throw new InvalidOperationException("E_MERGE_KEY_FOUND");
                    if (overwriteExists)
                        collection[mergeKey] = mergeValue;
                }
                else
                    collection.Add(mergeKey, mergeValue);
            }

            enumerator = null;
        }
        public static void Merge<TKey, TValue, TMergeKey, TMergeValue>(
            this IDictionary<TKey, TValue> collection, IEnumerable<KeyValuePair<TMergeKey, TMergeValue>> collectionMerge, 
            Func<object, TKey> keyConvertAction, Func<object, TValue> valueConvertAction,
            bool throwIfKeyExists = false, bool overwriteExists = true)
        {
            if (collectionMerge == null)
                throw new ArgumentNullException("collectionMerge", "E_MERGE_COLLECTION_NOT_FOUND");

            var enumerator = collectionMerge.GetEnumerator();

            foreach (var item in collectionMerge)
            {
                TKey mergeKey = keyConvertAction(item.Key);
                TValue mergeValue = valueConvertAction(item.Value);

                if (collection.ContainsKey(mergeKey))
                {
                    if (throwIfKeyExists)
                        throw new InvalidOperationException("E_MERGE_KEY_FOUND");
                    if (overwriteExists)
                        collection[mergeKey] = mergeValue;
                }
                else
                    collection.Add(mergeKey, mergeValue);
            }

            enumerator = null;
        }
    }
}
