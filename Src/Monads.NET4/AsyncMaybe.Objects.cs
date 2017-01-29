using System.Linq;
using System.Threading.Tasks;

namespace System.Monads
{
    public static partial class MaybeObjects
    {
        /// <summary>
		/// Allows to do some <paramref name="action"/> on <paramref name="source"/> if its not null
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns><paramref name="source"/> object</returns>
		public static async Task<TSource> Do<TSource>(this TSource source, Func<TSource, Task> action)
            where TSource : class
        {
            if (source != default(TSource))
            {
                await action(source);
            }

            return source;
        }

        /// <summary>
        /// Allows to do some conversion of <paramref name="source"/> if its not null or return <paramref name="defaultValue"/> otherwise
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="action">Conversion action which should to do</param>
        /// <param name="defaultValue">Value which will return if source is null</param>
        /// <returns>Converted object which returns action if source is not null or <paramref name="defaultValue"/> otherwise</returns>
        public static async Task<TResult> Return<TSource, TResult>(this TSource source, Func<TSource, Task<TResult>> action, TResult defaultValue)
            where TSource : class
        {
            if (source != default(TSource))
            {
                return await action(source);
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retruns the <paramref name="source"/> if both <paramref name="condition"/> is true and <paramref name="source"/> is not null, or null otherwise
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="condition">Condition which should be checked</param>
        /// <returns><paramref name="source"/> if <paramref name="condition"/> is true, or null otherwise</returns>
        public static Task<TSource> If<TSource>(this TSource source, Func<TSource, Task<bool>> condition)
            where TSource : class
        {
            return ReturnIf(source, condition, true);
        }

        /// <summary>
        /// Retruns the <paramref name="source"/> if both <paramref name="condition"/> is false and <paramref name="source"/> is not null, or null otherwise
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="condition">Condition which should be checked</param>
        /// <returns><paramref name="source"/> if <paramref name="condition"/> is true, or null otherwise</returns>
        public static Task<TSource> IfNot<TSource>(this TSource source, Func<TSource, Task<bool>> condition)
            where TSource : class
        {
            return ReturnIf(source, condition, false);
        }

        private static async Task<TSource> ReturnIf<TSource>(TSource source, Func<TSource, Task<bool>> condition, bool expected) where TSource : class
        {
            if ((source != default(TSource)) && (await condition(source) == expected))
            {
                return source;
            }
            else
            {
                return default(TSource);
            }
        }

        /// <summary>
		/// Allows to do <paramref name="action"/> and catch any exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>
		/// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
		/// </returns>
		public static async Task<Tuple<TSource, Exception>> TryDo<TSource>(this TSource source, Func<TSource, Task> action)
            where TSource : class
        {
            if (source != default(TSource))
            {
                try
                {
                    await action(source);
                }
                catch (Exception ex)
                {
                    return new Tuple<TSource, Exception>(source, ex);
                }
            }

            return new Tuple<TSource, Exception>(source, null);
        }

        /// <summary>
        /// Allows to do <paramref name="action"/> and catch exceptions, which handled by <param name="exceptionChecker"/>
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
        /// <returns>
        /// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
        /// </returns>
        public static async Task<Tuple<TSource, Exception>> TryDo<TSource>(this TSource source, Func<TSource, Task> action, Func<Exception, bool> exceptionChecker)
            where TSource : class
        {
            if (source != default(TSource))
            {
                try
                {
                    await action(source);
                }
                catch (Exception ex)
                {
                    if (exceptionChecker(ex))
                    {
                        return new Tuple<TSource, Exception>(source, ex);
                    }
                    throw;
                }
            }

            return new Tuple<TSource, Exception>(source, null);
        }

        /// <summary>
        /// Allows to do <paramref name="action"/> and catch exceptions, which handled by <param name="exceptionChecker"/>
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
        /// <returns>
        /// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
        /// </returns>
        public static async Task<Tuple<TSource, Exception>> TryDo<TSource>(this TSource source, Func<TSource, Task> action, Func<Exception, Task<bool>> exceptionChecker)
            where TSource : class
        {
            if (source != default(TSource))
            {
                try
                {
                    await action(source);
                }
                catch (Exception ex)
                {
                    if (await exceptionChecker(ex))
                    {
                        return new Tuple<TSource, Exception>(source, ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return new Tuple<TSource, Exception>(source, null);
        }

        /// <summary>
		/// Allows to do <paramref name="action"/> and catch <param name="expectedException"/> exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <param name="expectedException">Array of exception types, which should be handled</param>
		/// <returns>
		/// Tuple which contains <paramref name="source"/> and info about exception (if it throws)
		/// </returns>
		public static async Task<Tuple<TSource, Exception>> TryDo<TSource>(this TSource source, Func<TSource, Task> action, params Type[] expectedException)
            where TSource : class
        {
            if (source != default(TSource))
            {
                try
                {
                    await action(source);
                }
                catch (Exception ex)
                {
                    if (expectedException.Any(exp => exp.IsInstanceOfType(ex)) == true)
                    {
                        return new Tuple<TSource, Exception>(source, ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return new Tuple<TSource, Exception>(source, null);
        }

        /// <summary>
		/// Allows to do some conversion of <paramref name="source"/> if its not null and catch any exceptions
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <typeparam name="TResult">Type of result</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <param name="action">Action which should to do</param>
		/// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
		public static async Task<Tuple<TResult, Exception>> TryWith<TSource, TResult>(this TSource source, Func<TSource, Task<TResult>> action)
            where TSource : class
        {
            if (source != default(TSource))
            {
                TResult result = default(TResult);
                try
                {
                    result = await action(source);
                    return new Tuple<TResult, Exception>(result, null);
                }
                catch (Exception ex)
                {
                    return new Tuple<TResult, Exception>(result, ex);
                }
            }

            return new Tuple<TResult, Exception>(default(TResult), null);
        }

        /// <summary>
        /// Allows to do some conversion of <paramref name="source"/> if its not null and catch exceptions, which handled by <param name="exceptionChecker"/>
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
        /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
        public static async Task<Tuple<TResult, Exception>> TryWith<TSource, TResult>(this TSource source,
            Func<TSource, Task<TResult>> action, Func<Exception, bool> exceptionChecker)
            where TSource : class
        {
            if (source != default(TSource))
            {
                TResult result = default(TResult);
                try
                {
                    result = await action(source);
                    return new Tuple<TResult, Exception>(result, null);
                }
                catch (Exception ex)
                {
                    if (exceptionChecker(ex) == true)
                    {
                        return new Tuple<TResult, Exception>(result, ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return new Tuple<TResult, Exception>(default(TResult), null);
        }

        /// <summary>
        /// Allows to do some conversion of <paramref name="source"/> if its not null and catch exceptions, which handled by <param name="exceptionChecker"/>
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <param name="exceptionChecker">Predicate to determine if exceptions should be handled</param>
        /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
        public static async Task<Tuple<TResult, Exception>> TryWith<TSource, TResult>(this TSource source,
            Func<TSource, Task<TResult>> action, Func<Exception, Task<bool>> exceptionChecker)
            where TSource : class
        {
            if (source != default(TSource))
            {
                TResult result = default(TResult);
                try
                {
                    result = await action(source);
                    return new Tuple<TResult, Exception>(result, null);
                }
                catch (Exception ex)
                {
                    if (await exceptionChecker(ex))
                    {
                        return new Tuple<TResult, Exception>(result, ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return new Tuple<TResult, Exception>(default(TResult), null);
        }

        /// <summary>
        /// Allows to do some conversion of <paramref name="source"/> if its not null and catch <param name="expectedException"/> exceptions
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="action">Action which should to do</param>
        /// <param name="expectedException">Array of exception types, which should be handled</param>
        /// <returns>Tuple which contains Converted object and info about exception (if it throws)</returns>
        public static async Task<Tuple<TResult, Exception>> TryWith<TSource, TResult>(this TSource source,
            Func<TSource, Task<TResult>> action, params Type[] expectedException)
            where TSource : class
        {
            if (source != default(TSource))
            {
                TResult result = default(TResult);
                try
                {
                    result = await action(source);
                    return new Tuple<TResult, Exception>(result, null);
                }
                catch (Exception ex)
                {
                    if (expectedException.Any(exp => exp.IsInstanceOfType(ex)) == true)
                    {
                        return new Tuple<TResult, Exception>(result, ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            return new Tuple<TResult, Exception>(default(TResult), null);
        }

        /// <summary>
		/// Handle exception with <param name="handler"/> action
		/// </summary>
		/// <typeparam name="TSource">Type of source object</typeparam>
		/// <param name="source">Source object for operating</param>
		/// <returns><paramref name="source"/> object</returns>
		public static async Task<TSource> Catch<TSource>(this Tuple<TSource, Exception> source, Func<Exception, Task> handler)
        {
            if (source.Item2 != null)
            {
                await handler(source.Item2);
            }

            return source.Item1;
        }
    }
}
