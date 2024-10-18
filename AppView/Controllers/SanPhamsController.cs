using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData.Entities;

namespace AppView.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7184/api/SanPhams";  // API base URL

        public SanPhamsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            var sanPhams = await _httpClient.GetFromJsonAsync<List<SanPham>>(_apiBaseUrl);
            return View(sanPhams);
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _httpClient.GetFromJsonAsync<SanPham>($"{_apiBaseUrl}/{id}");
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenSanPham,MaSanPham,SoLuongTonKho,DonGia,NgayNhapKho,NhaCungCap")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                sanPham.ID = Guid.NewGuid();
                var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, sanPham);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                // Handle errors if needed
            }
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _httpClient.GetFromJsonAsync<SanPham>($"{_apiBaseUrl}/{id}");
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,TenSanPham,MaSanPham,SoLuongTonKho,DonGia,NgayNhapKho,NhaCungCap")] SanPham sanPham)
        {
            if (id != sanPham.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", sanPham);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                // Handle errors if needed
            }
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _httpClient.GetFromJsonAsync<SanPham>($"{_apiBaseUrl}/{id}");
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            // Handle errors if needed
            return View();
        }

        private async Task<bool> SanPhamExists(Guid id)
        {
            var sanPham = await _httpClient.GetFromJsonAsync<SanPham>($"{_apiBaseUrl}/{id}");
            return sanPham != null;
        }
    }
}
