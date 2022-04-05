using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamicProxy.Sample
{
    public interface ICustomerRepository
    {
        Task<List<CustomerModel>> GetAllCustomersAsync();
        List<CustomerModel> GetAllCustomers();
    }

    public class CustomerRepository : ICustomerRepository
    {
        public Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            var list = new List<CustomerModel>()
            {
                new CustomerModel() { FirstName = "Taliah", LastName = "Gough" },
                new CustomerModel() { FirstName = "Alfred", LastName = "Humphrey" },
                new CustomerModel() { FirstName = "Shane", LastName = "Woolley" },
                new CustomerModel() { FirstName = "Alfred", LastName = "Humphrey" },
            };

            return Task.FromResult(list);
        }

        public List<CustomerModel> GetAllCustomers()
        {
            return new List<CustomerModel>()
            {
                new CustomerModel() { FirstName = "Taliah", LastName = "Gough" },
                new CustomerModel() { FirstName = "Alfred", LastName = "Humphrey" },
                new CustomerModel() { FirstName = "Shane", LastName = "Woolley" },
                new CustomerModel() { FirstName = "Alfred", LastName = "Humphrey" },
            };
        }
    }

    public class CustomerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString() =>
            $"{FirstName}-{LastName}";
    }
}