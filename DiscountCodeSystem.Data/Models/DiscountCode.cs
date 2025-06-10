using System.ComponentModel.DataAnnotations;

namespace DiscountCodeSystem.Data.Models
{
    /// <summary>
    /// Model for discount codes.
    /// </summary>
    public class DiscountCode
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
