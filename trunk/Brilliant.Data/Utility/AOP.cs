using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Brilliant.Data.Utility
{
    /// <summary>
    /// AOP代理
    /// </summary>
    public class AOP
    {
        //泛型委托
        private Action<Action> action = null;

        //方法委托
        private Delegate WorkDelegate;

        /// <summary>
        /// AOP实例
        /// </summary>
        public static AOP Start
        {
            [DebuggerStepThrough]
            get
            {
                return new AOP();
            }
        }

        /// <summary>
        /// 执行指定方法（无含返回值）
        /// </summary>
        /// <param name="work">方法</param>
        [DebuggerStepThrough]
        public void Do(Action work)
        {
            if (this.action == null)
            {
                work();
            }
            else
            {
                this.action(work);
            }
        }

        /// <summary>
        /// 执行指定的方法（有返回值）
        /// </summary>
        /// <typeparam name="TReturnType">返回值类型</typeparam>
        /// <param name="work">方法</param>
        /// <returns>返回值</returns>
        [DebuggerStepThrough]
        public TReturnType Do<TReturnType>(Func<TReturnType> work)
        {
            this.WorkDelegate = work;
            if (this.action == null)
            {
                return work();
            }
            else
            {
                TReturnType returnValue = default(TReturnType);
                this.action(() =>
                {
                    Func<TReturnType> workDelegate = WorkDelegate as Func<TReturnType>;
                    returnValue = workDelegate();
                });
                return returnValue;
            }
        }

        /// <summary>
        /// 将指定的方法连缀到方法链中
        /// </summary>
        /// <param name="aopDelegate">方法</param>
        /// <returns>当前对象</returns>
        [DebuggerStepThrough]
        public AOP Combine(Action<Action> aopDelegate)
        {
            if (this.action == null)
            {
                this.action = aopDelegate;
            }
            else
            {
                Action<Action> existingAction = this.action;
                Action<Action> callAnother = (work) =>
                    existingAction(() => aopDelegate(work));
                this.action = callAnother;
            }
            return this;
        }
    }

    /// <summary>
    /// AOP方法扩展
    /// </summary>
    public static class AOPExtensions
    {
        public static AOP Try(this AOP aop)
        {
            return aop.Combine((work) =>
            {
                Try(work, DealException);
            });
        }

        public static void Try(Action work, Action<Exception> errorHandler)
        {
            try
            {
                work();
            }
            catch (Exception ex)
            {
                errorHandler(ex);
                throw ex;
            }
        }

        public static void DealException(Exception ex)
        {

        }

        public static AOP Trace(this AOP aop)
        {
            return aop.Combine((work) =>
            {

            });
        }

        public static AOP Log(this AOP aop)
        {
            return aop.Combine((work) =>
            {

            });
        }

        [DebuggerStepThrough]
        public static void Retry(this AOP aop, int retryDuration, int retryCount, Action<Exception> errorHandler, Action retryFaild, Action work)
        {
            do
            {
                try
                {
                    work();
                }
                catch (Exception ex)
                {
                    errorHandler(ex);
                    System.Threading.Thread.Sleep(retryDuration);
                    throw;
                }
            } while (retryCount-- > 0);

        }

        [DebuggerStepThrough]
        public static void DoNothing()
        {

        }

        [DebuggerStepThrough]
        public static void DoNothing(params object[] whatever)
        {

        }

        [DebuggerStepThrough]
        public static AOP Delay(this AOP aop, int milliseconds)
        {
            return aop.Combine((work) =>
            {
                System.Threading.Thread.Sleep(milliseconds);
                work();
            });
        }

        [DebuggerStepThrough]
        public static AOP MustBeNonNull(this AOP aop, params object[] args)
        {
            return aop.Combine((work) =>
            {
                for (int i = 0; i < args.Length; i++)
                {
                    object arg = args[i];
                    if (arg == null)
                    {
                        throw new ArgumentException(string.Format("Parameter at index {0} is null", i));
                    }
                }
                work();
            });
        }

        [DebuggerStepThrough]
        public static AOP MustBeNonDefault<T>(this AOP aop, params T[] args) where T : IComparable
        {
            return aop.Combine((work) =>
            {
                T defaultvalue = default(T);
                for (int i = 0; i < args.Length; i++)
                {
                    T arg = args[i];
                    if (arg == null || arg.Equals(defaultvalue))
                    {
                        throw new ArgumentException(string.Format("Parameter at index {0} is null", i));
                    }
                }
                work();
            });
        }

        [DebuggerStepThrough]
        public static AOP WhenTrue(this AOP aop, params Func<bool>[] conditions)
        {
            return aop.Combine((work) =>
            {
                foreach (Func<bool> condition in conditions)
                {
                    if (!condition())
                    {
                        return;
                    }
                }
                work();
            });
        }

        [DebuggerStepThrough]
        public static AOP RunAsync(this AOP aop, Action completeCallback)
        {
            return aop.Combine((work) => work.BeginInvoke(asyncresult =>
            {
                work.EndInvoke(asyncresult); completeCallback();
            }, null));
        }

        [DebuggerStepThrough]
        public static AOP RunAsync(this AOP aop)
        {
            return aop.Combine((work) => work.BeginInvoke(asyncresult =>
            {
                work.EndInvoke(asyncresult);
            }, null));
        }
    }
}