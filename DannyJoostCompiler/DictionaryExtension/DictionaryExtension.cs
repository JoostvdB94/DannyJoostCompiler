using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.DictionaryExtension
{
    public static class DictionaryExtension
    {

        public static V GetValue<K, V>(this Dictionary<K, V> dictionary, K key)
        {
            V value;
            dictionary.TryGetValue(key, out value);
            return (value == null) ? default(V) : value;
        }
    }
}
