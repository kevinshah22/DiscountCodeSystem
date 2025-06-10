using DiscountCodeSystem.Data;
using DiscountCodeSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscountCodeService.Services
{
    /// <summary>
    /// Code store class useful for database operation and calling data layer to save or update the codes.
    /// </summary>
    public class CodeStore
    {
        private readonly DiscountDBContext _context;
        private readonly object _lock = new();

        public CodeStore(DiscountDBContext context)
        {
            _context = context;
        }

        public async Task<(bool, List<string>)> GenerateAsync(int count, int length)
        {
            var newCodes = new List<string>();
            var existing = await _context.DiscountCodes.Select(c => c.Code).ToHashSetAsync();

            var rng = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            lock (_lock)
            {
                for (int i = 0; i < count; i++)
                {
                    string code;
                    do
                    {
                        code = new string(Enumerable.Range(0, length).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
                    } while (existing.Contains(code));

                    existing.Add(code);
                    newCodes.Add(code);

                    _context.DiscountCodes.Add(new DiscountCode { Code = code });
                }

                _context.SaveChanges();
            }

            return (true, newCodes);
        }

        public async Task<uint> UseCodeAsync(string code)
        {
            var entity = await _context.DiscountCodes.FirstOrDefaultAsync(x => x.Code == code);

            if (entity == null)
                return 1; // Not found
            if (entity.IsUsed)
                return 2; // Already used

            entity.IsUsed = true;
            await _context.SaveChangesAsync();
            return 0; // Success
        }

    }
}
