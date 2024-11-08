using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext(DbContextOptions parameters) : DbContext(parameters)
    {
        public DbSet<Customer> Customer { get; set; }
    }
}