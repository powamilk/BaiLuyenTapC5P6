using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    internal class SanPhamRepo : ISanPhamRepo
    {
        private readonly AppDbContext _context;

        public SanPhamRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(SanPham sanPham)
        {
            await _context.SanPhams.AddAsync(sanPham);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SanPham>> GetAllAsync()
        {
            return await _context.SanPhams.ToListAsync();
        }

        public async Task<SanPham> GetByIdAsync(Guid id)
        {
            return await _context.SanPhams.FindAsync(id);
        }

        public Task UpdateAsync(SanPham sanPham)
        {
            _context.SanPhams.Update(sanPham);
            return _context.SaveChangesAsync();
        }
    }
}
