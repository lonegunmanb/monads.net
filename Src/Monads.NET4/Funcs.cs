using System.Threading.Tasks;

namespace System.Monads
{
    public static class Funcs
    {
        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>
        /// <param name="action">Func which should be invoked</param>        
        public static async Task Execute(this Func<Task> action)
        {
            if (action != null)
            {
                await action();
            }
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>
        /// <typeparam name="T">Type of action argument</typeparam>
        /// <param name="action">Func which should be invoked</param>
        /// <param name="arg">Func argument</param>
        public static async Task Execute<T>(this Func<T, Task> action, T arg)
        {
            if (action != null)
            {
                await action(arg);
            }
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <param name="action">Func which should be invoked</param>
        /// <param name="arg1">Func argument 1</param>
        /// <param name="arg2">Func argument 2 </param>
        public static async Task Execute<T1, T2>(this Func<T1, T2, Task> action, T1 arg1, T2 arg2)
        {
            if (action != null)
            {
                await action(arg1, arg2);
            }
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <typeparam name="T3">Type of action argument 3 </typeparam>
        /// <param name="action">Func which should be invoked</param>
        /// <param name="arg1">Func argument 1</param>
        /// <param name="arg2">Func argument 2</param>
        /// <param name="arg3">Func argument 3 </param>
        public static async Task Execute<T1, T2, T3>(this Func<T1, T2, T3, Task> action, T1 arg1, T2 arg2, T3 arg3)
        {
            if (action != null)
            {
                await action(arg1, arg2, arg3);
            }
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <typeparam name="T3">Type of action argument 3</typeparam>
        /// <typeparam name="T4">Type of action argument 4</typeparam>                
        /// <param name="action">Func which should be invoked</param>
        /// <param name="arg1">Func argument 1</param>
        /// <param name="arg2">Func argument 2</param>
        /// <param name="arg3">Func argument 3</param>
        /// <param name="arg4">Func argument 4</param>        
        public static async Task Execute<T1, T2, T3, T4>(this Func<T1, T2, T3, T4, Task> action, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4)
        {
            if (action != null)
            {
                await action(arg1, arg2, arg3, arg4);
            }
        }

        /// <summary>
        /// Allows to invoke <paramref name="action"/> action if it is not null
        /// </summary>	    
        /// <typeparam name="T1">Type of action argument 1</typeparam>
        /// <typeparam name="T2">Type of action argument 2</typeparam>
        /// <typeparam name="T3">Type of action argument 3</typeparam>
        /// <typeparam name="T4">Type of action argument 4</typeparam>
        /// <typeparam name="T5">Type of action argument 5</typeparam>
        /// <param name="action">Func which should be invoked</param>
        /// <param name="arg1">Func argument 1</param>
        /// <param name="arg2">Func argument 2</param>
        /// <param name="arg3">Func argument 3</param>
        /// <param name="arg4">Func argument 4</param>
        /// <param name="arg5">Func argument 5</param>        
        public static async Task Execute<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5, Task> action, T1 arg1, T2 arg2, T3 arg3,
            T4 arg4, T5 arg5)
        {
            if (action != null)
            {
                await action(arg1, arg2, arg3, arg4, arg5);
            }
        }
    }
}
