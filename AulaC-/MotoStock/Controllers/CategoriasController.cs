using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoStock.Data;
using MotoStock.Models;
using MotoStock.ViewModels;

namespace MotoStock.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            var categorias = await _context.Categorias
                .OrderBy(c => c.Nome)
                .ToListAsync();
            return View(categorias);
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Ativo")] CategoriaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var categoria = new Categoria
                {
                    Nome = viewModel.Nome,
                    Descricao = viewModel.Descricao,
                    Ativo = viewModel.Ativo
                };
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Categoria criada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var viewModel = new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Ativo = categoria.Ativo
            };
            return View(viewModel);
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Ativo")] CategoriaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoria = await _context.Categorias.FindAsync(id);
                    if (categoria == null)
                    {
                        return NotFound();
                    }

                    categoria.Nome = viewModel.Nome;
                    categoria.Descricao = viewModel.Descricao;
                    categoria.Ativo = viewModel.Ativo;

                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Categoria atualizada com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Categoria eliminada com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
