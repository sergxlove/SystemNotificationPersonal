using Microsoft.EntityFrameworkCore;
using System.Text;
using SystemNotificationPersonal.DataAccess.Sqlite.Abstractions;
using SystemNotificationPersonal.DataAccess.Sqlite.Models;

namespace SystemNotificationPersonal.DataAccess.Sqlite.Repositories
{
    public class CodesExitRepository : ICodesExitRepository
    {
        private readonly SystemNotificationDbContext _context;

        public CodesExitRepository(SystemNotificationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetCodeAsync(DateOnly date, CancellationToken token)
        {
            var result = await _context.CodesTable
                .FirstOrDefaultAsync(a => a.Date == date, token);
            if (result is null) return string.Empty;
            return result.Code;
        }

        public async Task<List<CodesExitEntity>> GetCodesAllAsync(CancellationToken token)
        {
            return await _context.CodesTable
                .AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<string> GenerateAsync(CancellationToken token)
        {
            string code = string.Empty;
            StringBuilder codeSb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                codeSb.Append(random.Next(0, 10));
            }
            code = codeSb.ToString();
            CodesExitEntity codes = new CodesExitEntity()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Code = code,
            };
            await _context.CodesTable.AddAsync(codes, token);
            await _context.SaveChangesAsync(token);
            return code;
        }
    }
}
