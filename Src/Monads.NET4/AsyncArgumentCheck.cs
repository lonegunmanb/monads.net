using System.Threading.Tasks;

namespace System.Monads
{
    public static partial class ArgumentCheck
    {
        /// <summary>
        /// Allows to check <paramref name="source"/> for <paramref name="checkCondition"/> condition and throw exception if it is false
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="checkCondition">Condition which should be checked</param>
        /// <param name="exceptionSource">Action for creation of Exception object</param>
        /// <returns>Source object if it is not null</returns>
        public static async Task<TSource> Check<TSource>(this TSource source, Func<TSource, Task<bool>> checkCondition, Func<TSource, Exception> exceptionSource)
        {
            if (!(await checkCondition(source)))
            {
                throw exceptionSource(source);
            }

            return source;
        }

        /// <summary>
        /// Allows to check <paramref name="source"/> for <paramref name="checkCondition"/> condition and retrun <paramref name="defaultValue"/> if it is false
        /// </summary>
        /// <typeparam name="TSource">Type of source object</typeparam>
        /// <param name="source">Source object for operating</param>
        /// <param name="checkCondition">Condition which should be checked</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns><paramref name="source"/> if <paramref name="checkCondition"/> returns true, or <paramref name="defaultValue"/> otherwise</returns>
        public static async Task<TSource> CheckWithDefault<TSource>(this TSource source, Func<TSource, Task<bool>> checkCondition, TSource defaultValue)
        {
            if (!(await checkCondition(source)))
            {
                return defaultValue;
            }

            return source;
        }
    }
}
