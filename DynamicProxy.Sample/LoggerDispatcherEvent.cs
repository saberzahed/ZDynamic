using System;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DynamicProxy.Sample
{
    public class LoggerDispatcherEvent : IDispatcherEvent
    {
        private readonly ILogger _logger;

        public LoggerDispatcherEvent(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LoggerDispatcherEvent");
        }

        public void Before(MethodInfo method, object[] args)
        {
            _logger.LogInformation("Call Before Execute => {methodName}", method.Name);
            _logger.LogInformation("Reflected Type => {type}", method.ReflectedType.Name);
            _logger.LogInformation("Arguments => {arguments}", JsonSerializer.Serialize(args));
        }

        public void After(MethodInfo method, object result)
        {
            _logger.LogInformation("Return Type => {type}", method.ReturnType.ToString());
            if (result is Task task)
            {
                var taskResult = task.GetType().GetProperty("Result")?.GetValue(task);
                if (taskResult is null) 
                    return;
                
                _logger.LogInformation("Return Value => {value}", JsonSerializer.Serialize(taskResult));
                return;
            }

            _logger.LogInformation("Return Value => {value}", JsonSerializer.Serialize(result));
        }
    }
}