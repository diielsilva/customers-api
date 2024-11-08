namespace api.Models
{
    public class Customer(int id, string name, string email, DateTime createdAt, DateTime? updatedAt) : Model(id, createdAt, updatedAt)
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
    }
}