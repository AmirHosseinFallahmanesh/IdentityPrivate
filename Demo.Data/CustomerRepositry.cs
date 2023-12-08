using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Data
{

    public interface ICustomerRepository
    {
        int Add(Customer customer);
    }
    public class CustomerRepositry : ICustomerRepository
    {
        private readonly DemoContext context;

        public CustomerRepositry(DemoContext context)
        {
            this.context = context;
        }
        public int Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer.CustomerId;
        }
    }
}
