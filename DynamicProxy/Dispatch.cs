using System;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public class Dispatch<TService, TServiceImplement> : DispatchProxy 
        where TServiceImplement : class, TService, new()
        where TService : class
    {
        private TServiceImplement? _service;
        private IDispatcherEvent? _dispatcherEvent;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            try
            {
                _dispatcherEvent?.CallBefore(targetMethod, args);
            }
            catch
            {
                // ignored
            }

            var result = targetMethod?.Invoke(_service, args);

            try
            {
                _dispatcherEvent?.CallAfter(targetMethod.ReturnParameter, targetMethod.ReturnType, result);
            }
            catch
            {
                // ignored
            }

            return result;
        }


        public static TService? Create(TServiceImplement? service, IDispatcherEvent? dispatcherEvent = null!)
        {
            var proxy = Create<TService, Dispatch<TService, TServiceImplement>>();
            if (proxy is Dispatch<TService, TServiceImplement> dispatch)
                dispatch?.SetParameters(service, dispatcherEvent);

            return proxy;
        }

        private void SetParameters(TServiceImplement? service, IDispatcherEvent? dispatcherEvent = null)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _dispatcherEvent = dispatcherEvent;
        }
    }
}