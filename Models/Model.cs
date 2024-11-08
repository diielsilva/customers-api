using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public abstract class Model(int id, DateTime createdAt, DateTime? updatedAt)
    {
        [Key]
        public int Id { get; set; } = id;
        public DateTime CreatedAt { get; set; } = createdAt;
        public DateTime? UpdatedAt { get; set; } = updatedAt;
    }
}