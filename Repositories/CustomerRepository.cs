using api.Data;
using api.Exceptions;
using api.Models;

namespace api.Repositories
{
    public class CustomerRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _DbContext = context;

        public Customer Store(Customer toStore)
        {
            bool isANewCustomer = toStore.Id == 0;

            if (isANewCustomer)
            {
                _DbContext.Customer.Add(toStore);
            }

            _DbContext.SaveChanges();

            Customer created = FindById(toStore.Id);

            return created;
        }

        public List<Customer> FindAll()
        {
            return [.. _DbContext.Customer];
        }

        public Customer FindById(int id)
        {
            Customer? retrieved = _DbContext.Customer.Find(id);

            if (retrieved is not null)
            {
                return retrieved;
            }

            throw new ModelNotFoundException($"Customer {id} not found");
        }

        public Customer? FindByEmail(string email)
        {
            Customer? retrieved = _DbContext.Customer.FirstOrDefault(customer => customer.Email == email);

            return retrieved;
        }

        public Customer Delete(int id)
        {
            Customer retrieved = FindById(id);

            _DbContext.Customer.Remove(retrieved);
            _DbContext.SaveChanges();

            return retrieved;
        }
    }
}