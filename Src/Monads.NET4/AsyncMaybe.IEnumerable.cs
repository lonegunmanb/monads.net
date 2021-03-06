﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Monads
{
    public static partial class MaybeIEnumerable
    {
        /// <summary>
        /// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
        /// </summary>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <returns>Source collection</returns>
        public static async Task<IEnumerable> Do(this IEnumerable source, Func<object, Task> action)
        {
            if (source != null)
            {
                foreach (var element in source)
                {
                    await action(element);
                }
            }

            return source;
        }

        /// <summary>
        /// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements</typeparam>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <returns>Source collection</returns>
        public static async Task<IEnumerable<TSource>> Do<TSource>(this IEnumerable<TSource> source,
            Func<TSource, Task> action)
        {
            if (source != null)
            {
                foreach (var element in source)
                {
                    await action(element);
                }
            }

            return source;
        }

        /// <summary>
        /// Allows to do some <paramref name="action"/> on each element of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements</typeparam>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do (with zero-based index)</param>
        /// <returns>Source collection</returns>
        public static async Task<IEnumerable<TSource>> Do<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, Task> action)
        {
            if (source != null)
            {
                foreach (var element in source.Select((s, i) => new {Source = s, Index = i}))
                {
                    await action(element.Source, element.Index);
                }
            }

            return source;
        }

        /// <summary>
        /// Allows to do some conversion of <paramref name="source"/> collection elements if its not null
        /// </summary>
        /// <typeparam name="TSource">Type of collection elements</typeparam>
        /// <typeparam name="TResult">Type of result collection elements</typeparam>
        /// <param name="source">Source collection for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <returns>Converted collection</returns>
        public static async Task<IEnumerable<TResult>> With<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> action)
        {
            if (source != null)
            {
                return await Task.WhenAll(source.Select(action));
            }
            else
            {
                return null;
            }
        }
    }
}
