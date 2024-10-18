using AppData;
using AppData.Entities;
using AppData.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamRepo _sanPhamRepo;
        private readonly IValidator<SanPham> _validator;
        private readonly AppDbContext _context;

        public SanPhamController(ISanPhamRepo sanPhamRepo,IValidator<SanPham> validator, AppDbContext context)
        {
            _sanPhamRepo = sanPhamRepo;
            _validator = validator;
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sanPham = await _sanPhamRepo.GetAllAsync();
            return Ok(sanPham);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sanPham = await _sanPhamRepo.GetByIdAsync(id);
            if (sanPham == null) return NotFound();
            return Ok(sanPham);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(SanPham sanPham)
        {
            var validatorResult = await _validator.ValidateAsync(sanPham);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }));
            }

            await _sanPhamRepo.AddAsync(sanPham);
            return Ok(sanPham);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SanPham sanPham)
        {
            if (id != sanPham.ID)
            {
                return BadRequest("ID không khớp.");
            }

            var validatorResult = await _validator.ValidateAsync(sanPham);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }));
            }

            await _sanPhamRepo.UpdateAsync(sanPham);
            return Ok(sanPham);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sanPhamRepo.DeleteAsync(id);
            return NoContent();
        }

    }
}
