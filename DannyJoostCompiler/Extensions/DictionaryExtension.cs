using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyJoostCompiler.Extensions
{
    public static class DictionaryExtension
    {

        public static V GetValue<K,V>(this Dictionary<K, V> dictionary, K key)
        {
            V value;
            return dictionary.TryGetValue(key, out value) ? value : default(V);
        }
    }
}
