using Castle.DynamicProxy;
using log4net;
using System;

namespace M19G1.Common.Logging
{
    public class Interceptor : IInterceptor
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);
        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
            }
        }
    }
}
