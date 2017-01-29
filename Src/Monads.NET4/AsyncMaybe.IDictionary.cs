using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Monads
{
    public static partial class MaybeIDictionary
    {
        /// <summary>
        /// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TKey">Type of keys of dictionary</typeparam>
        /// <typeparam name="TValue">Type of values of dictionary</typeparam>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <returns>Source dictionary</returns>
        public static async Task<IDictionary<TKey, TValue>> Do<TKey, TValue>(this IDictionary<TKey, TValue> source, Func<TKey, TValue, Task> action)
        {
            if (source != null)
            {
                foreach (var element in source)
                {
                    await action(element.Key, element.Value);
                }
            }

            return source;
        }
    }
}
