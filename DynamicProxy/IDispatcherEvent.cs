using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IDispatcherEvent
    {
        void Before(MethodInfo methodInfo, object[] args);
        void After(MethodInfo methodInfo, object result);
    }
}