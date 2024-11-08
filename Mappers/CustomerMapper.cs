using api.Dtos.Customer;
using api.Models;

namespace api.Mappers
{
    public static class CustomerMapper
    {
        public static Customer ToModel(this CustomerRequest request)
        {
            string name = request.Name;
            string email = request.Email;

            return new Customer(0, name, email, DateTime.Now, null);
        }

        public static CustomerResponse ToResponse(this Customer customer)
        {
            int id = customer.Id;
            string name = customer.Name;
            string email = customer.Email;
            DateTime createdAt = customer.CreatedAt;
            DateTime? updatedAt = customer.UpdatedAt;

            return new CustomerResponse(id, name, email, createdAt, updatedAt);
        }

    }
}