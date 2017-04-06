using System;
using System.Collections.Generic;

namespace TOF.Core.Utils.TypeExtensions
{
    public static class DictionaryExtensions
    {
        public static void Merge<TKey, TValue, TMergeKey, TMergeValue>(
            this IDictionary<TKey, TValue> collection, IEnumerable<KeyValuePair<TMergeKey, TMergeValue>> collectionMerge, 
            bool ThrowIfKeyExists = false, bool OverwriteExists = true)
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
                    if (ThrowIfKeyExists)
                        throw new InvalidOperationException("E_MERGE_KEY_FOUND");
                    if (OverwriteExists)
                        collection[mergeKey] = mergeValue;
                }
                else
                    collection.Add(mergeKey, mergeValue);
            }

            enumerator = null;
        }
        public static void Merge<TKey, TValue, TMergeKey, TMergeValue>(
            this IDictionary<TKey, TValue> collection, IEnumerable<KeyValuePair<TMergeKey, TMergeValue>> collectionMerge, 
            Func<object, TKey> KeyConvertAction, Func<object, TValue> ValueConvertAction,
            bool ThrowIfKeyExists = false, bool OverwriteExists = true)
        {
            if (collectionMerge == null)
                throw new ArgumentNullException("collectionMerge", "E_MERGE_COLLECTION_NOT_FOUND");

            var enumerator = collectionMerge.GetEnumerator();

            foreach (var item in collectionMerge)
            {
                TKey mergeKey = KeyConvertAction(item.Key);
                TValue mergeValue = ValueConvertAction(item.Value);

                if (collection.ContainsKey(mergeKey))
                {
                    if (ThrowIfKeyExists)
                        throw new InvalidOperationException("E_MERGE_KEY_FOUND");
                    if (OverwriteExists)
                        collection[mergeKey] = mergeValue;
                }
                else
                    collection.Add(mergeKey, mergeValue);
            }

            enumerator = null;
        }
    }
}
