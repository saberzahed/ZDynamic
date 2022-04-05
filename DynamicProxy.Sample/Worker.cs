using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DynamicProxy.Sample
{
    public class Worker : IHostedService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($">>> Sync Method");
            var customers = _customerRepository.GetAllCustomers();
            Console.WriteLine($">>> Start Iterate");
            foreach (var customer in customers)
            {
                Console.WriteLine($">>> {customer}");
            }

            Console.WriteLine($">>> Async Method");
            var customersAsync = await _customerRepository.GetAllCustomersAsync();
            Console.WriteLine($">>> Start Iterate");
            foreach (var customer in customersAsync)
            {
                Console.WriteLine($">>> {customer}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}