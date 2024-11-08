using System.Data;
using api.Dtos.Customer;
using api.Exceptions;
using api.Mappers;
using api.Models;
using api.Repositories;

namespace api.Services
{
    public class CustomerService(CustomerRepository repository)
    {
        private readonly CustomerRepository _Repository = repository;

        public CustomerResponse Create(CustomerRequest request)
        {
            Customer toStore = request.ToModel();
            string email = toStore.Email;

            bool isEmailAvailable = _Repository.FindByEmail(email) is null;

            if (isEmailAvailable)
            {
                Customer created = _Repository.Store(toStore);

                return created.ToResponse();
            }

            throw new ConstraintConflictException($"Email: {email} is in use");
        }

        public List<CustomerResponse> FindAll()
        {
            List<Customer> retrieved = _Repository.FindAll();

            return retrieved.Select(customer => customer.ToResponse()).ToList();
        }

        public CustomerResponse FindById(int id)
        {
            Customer retrieved = _Repository.FindById(id);

            return retrieved.ToResponse();
        }

        public CustomerResponse Update(int id, CustomerRequest request)
        {
            Customer toUpdate = _Repository.FindById(id);
            string currentEmail = toUpdate.Email;
            string updatedName = request.Name;
            string updatedEmail = request.Email;
            DateTime updateTime = DateTime.Now;

            bool isEmailAvailable = _Repository.FindByEmail(updatedName) == null;
            bool isCurrentEmailEqualsThanUpdatedEmail = currentEmail.Equals(updatedEmail);

            if (isEmailAvailable || isCurrentEmailEqualsThanUpdatedEmail)
            {
                toUpdate.Name = updatedName;
                toUpdate.Email = updatedEmail;
                toUpdate.UpdatedAt = updateTime;

                _Repository.Store(toUpdate);

                return toUpdate.ToResponse();
            }

            throw new ConstraintConflictException($"Email: {updatedEmail} is in use");

        }

        public void Delete(int id)
        {
            _Repository.Delete(id);
        }
    }
}