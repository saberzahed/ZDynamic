using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IDispatcherEvent
    {
        public void CallBefore(MethodInfo? method, object?[]? args);
        void CallAfter(ParameterInfo targetMethodReturnParameter, Type targetMethodReturnType, object? result);
    }
}