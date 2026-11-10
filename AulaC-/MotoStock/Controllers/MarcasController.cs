using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoStock.Data;
using MotoStock.Models;
using MotoStock.ViewModels;

namespace MotoStock.Controllers
{
    public class MarcasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarcasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
            var marcas = await _context.Marcas
                .OrderBy(m => m.Nome)
                .ToListAsync();
            return View(marcas);
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,PaisOrigem,Descricao,Ativo")] MarcaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var marca = new Marca
                {
                    Nome = viewModel.Nome,
                    PaisOrigem = viewModel.PaisOrigem,
                    Descricao = viewModel.Descricao,
                    Ativo = viewModel.Ativo
                };
                _context.Add(marca);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Marca criada com sucesso.";
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }

            var viewModel = new MarcaViewModel
            {
                Id = marca.Id,
                Nome = marca.Nome,
                PaisOrigem = marca.PaisOrigem,
                Descricao = marca.Descricao,
                Ativo = marca.Ativo
            };
            return View(viewModel);
        }

        // POST: Marcas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,PaisOrigem,Descricao,Ativo")] MarcaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var marca = await _context.Marcas.FindAsync(id);
                    if (marca == null)
                    {
                        return NotFound();
                    }

                    marca.Nome = viewModel.Nome;
                    marca.PaisOrigem = viewModel.PaisOrigem;
                    marca.Descricao = viewModel.Descricao;
                    marca.Ativo = viewModel.Ativo;

                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Marca atualizada com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Marca eliminada com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
            return _context.Marcas.Any(e => e.Id == id);
        }
    }
}
