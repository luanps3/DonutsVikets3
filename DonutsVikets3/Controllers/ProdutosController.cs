using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonutsVikets.Models;
using DonutsVikets3.Data;
using System.IO;

namespace DonutsVikets3.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly DonutsVikets3Context _context;

        public ProdutosController(DonutsVikets3Context context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var donutsVikets3Context = _context.Produto.Include(p => p.Categoria);
            return View(await donutsVikets3Context.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nome");
            return View(new ProdutoViewModel());
        }

        // POST: Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel vm)
        {
            NormalizePreco(vm);
            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nome", vm.CategoriaId);
                return View(vm);
            }

            var produto = new Produto
            {
                Nome = vm.Nome,
                Descricao = vm.Descricao ?? string.Empty,
                Preco = vm.Preco,
                CategoriaId = vm.CategoriaId
            };

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }
                produto.ImagemUrl = "/img/" + fileName;
            }

            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            var vm = new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                ImagemUrl = produto.ImagemUrl,
                CategoriaId = produto.CategoriaId
            };
            ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nome", vm.CategoriaId);
            return View(vm);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProdutoViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            NormalizePreco(vm);
            if (!ModelState.IsValid)
            {
                ViewData["CategoriaId"] = new SelectList(_context.Set<Categoria>(), "Id", "Nome", vm.CategoriaId);
                return View(vm);
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            produto.Nome = vm.Nome;
            produto.Descricao = vm.Descricao ?? string.Empty;
            produto.Preco = vm.Preco;
            produto.CategoriaId = vm.CategoriaId;

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }
                produto.ImagemUrl = "/img/" + fileName;
            }

            _context.Update(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void NormalizePreco(ProdutoViewModel vm)
        {
            // Accept 10,50 or 10.50 from raw form
            var raw = Request.Form["Preco"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(raw))
            {
                var normalized = raw.Replace('.', ',');
                if (decimal.TryParse(normalized, out var parsed))
                {
                    vm.Preco = parsed;
                }
            }
        }

        // DELETE remains unchanged
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.Produto.Include(p => p.Categoria).FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null) _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id) => _context.Produto.Any(e => e.Id == id);
    }
}
