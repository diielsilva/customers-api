namespace api.Dtos.Customer
{
    public record CustomerResponse(int Id, string Name, string Email, DateTime CreatedAt, DateTime? UpdatedAt)
    {

    }
}