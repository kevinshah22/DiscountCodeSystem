using Microsoft.EntityFrameworkCore;
using DiscountCodeSystem.Data.Models;

namespace DiscountCodeSystem.Data
{
    public class DiscountDBContext:DbContext
    {
        public DiscountDBContext(DbContextOptions<DiscountDBContext> options) : base(options) { }

        public DbSet<DiscountCode> DiscountCodes => Set<DiscountCode>();
    }
}
