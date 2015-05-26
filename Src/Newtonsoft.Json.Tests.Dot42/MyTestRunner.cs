using System;
using System.Reflection;
using System.Threading;
using Android.Util;
using Dot42;
using Java.Lang.Reflect;

namespace Newtonsoft.Json.Tests.Dot42
{
    public class MyTestRunner : Thread.IUncaughtExceptionHandler
    {
        private readonly string _tag;

        public MyTestRunner(string tag)
        {
            _tag = tag;
            System.Threading.Thread.DefaultUncaughtExceptionHandler = this;
        }

        [Include]
        public void RunTests(Type type)
        {
            Log.I(_tag, string.Format("running test from '{0}'", type.FullName));

            object instance=null;
            try
            {
                instance = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                Log.E(_tag, string.Format("failed to instantiate '{0}'", type.FullName), ex);
            }

            foreach(var method in type.GetRuntimeMethods())
            {
                if (!method.IsPublic) continue;
                if (method.DeclaringType != type) continue;
                if (method.GetParameters().Length != 0) continue;
                if (!method.IsDefined(typeof(NUnit.Framework.TestAttribute), true))
                    continue;

                Log.I(_tag + "." + type.Name, string.Format("running test '{0}'", method.Name));

                try
                {
                    method.Invoke(instance, null);

                    Log.W(_tag + "." + type.Name, string.Format("test successful for '{0}'", method.Name));
                }
                catch (TargetInvocationException ex)
                {
                    Exception cause = ex.InnerException;
                    if (cause is Junit.Framework.AssertionFailedError)
                        Log.E(_tag + "." + type.Name, string.Format("test failed for '{0}': {1}", method.Name, cause.Message));
                    else
                        Log.E(_tag + "." + type.Name, string.Format("test failed for '{0}'", method.Name), cause);
                }
                catch (Exception ex)
                {
                    Log.E(_tag + "." + type.Name, string.Format("test failed for '{0}'", method.Name), ex);
                }
            }

            Log.I(_tag, string.Format("finished with '{0}'", type.FullName));
        }

        public void UncaughtException(Thread thread, Exception exception)
        {
            Log.I(_tag, string.Format("first chance exception: {0}: {1}", exception.GetType().Name,exception.ToString()));
        }
    }
}