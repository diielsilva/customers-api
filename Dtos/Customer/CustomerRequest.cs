using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Customer
{
    public record CustomerRequest([Required][StringLength(255, MinimumLength = 3)] string Name, [Required][EmailAddress] string Email)
    {

    }
}