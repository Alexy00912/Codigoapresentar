using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoStock.Data;
using MotoStock.Models;
using MotoStock.ViewModels;

namespace MotoStock.Controllers
{
    public class MovimentosStockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovimentosStockController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.MovimentosStock
                .Include(m => m.Motocicleta)
                .OrderByDescending(m => m.DataMovimento).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.MovimentosStock.Include(m => m.Motocicleta).FirstOrDefaultAsync(m => m.Id == id);
            if (m == null) return NotFound();
            return View(m);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MotocicletaId,TipoMovimento,DataMovimento,Quantidade,Preco,Motivo,Observacoes")] MovimentoStockViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
                return View(vm);
            }
            var m = new MovimentoStock
            {
                MotocicletaId = vm.MotocicletaId, TipoMovimento = vm.TipoMovimento,
                DataMovimento = vm.DataMovimento, Quantidade = vm.Quantidade, Preco = vm.Preco,
                Motivo = vm.Motivo, Observacoes = vm.Observacoes
            };
            _context.Add(m);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Movimento de stock registado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.MovimentosStock.FindAsync(id);
            if (m == null) return NotFound();
            var vm = new MovimentoStockViewModel
            {
                Id = m.Id, MotocicletaId = m.MotocicletaId, TipoMovimento = m.TipoMovimento,
                DataMovimento = m.DataMovimento, Quantidade = m.Quantidade, Preco = m.Preco,
                Motivo = m.Motivo, Observacoes = m.Observacoes
            };
            await PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MotocicletaId,TipoMovimento,DataMovimento,Quantidade,Preco,Motivo,Observacoes")] MovimentoStockViewModel vm)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
                return View(vm);
            }
            try
            {
                var m = await _context.MovimentosStock.FindAsync(id);
                if (m == null) return NotFound();
                m.MotocicletaId = vm.MotocicletaId; m.TipoMovimento = vm.TipoMovimento;
                m.DataMovimento = vm.DataMovimento; m.Quantidade = vm.Quantidade; m.Preco = vm.Preco;
                m.Motivo = vm.Motivo; m.Observacoes = vm.Observacoes;
                _context.Update(m);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Movimento de stock atualizado com sucesso.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimentoStockExists(vm.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.MovimentosStock.Include(m => m.Motocicleta).FirstOrDefaultAsync(m => m.Id == id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _context.MovimentosStock.FindAsync(id);
            if (m != null)
            {
                _context.MovimentosStock.Remove(m);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Movimento de stock eliminado com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentoStockExists(int id) => _context.MovimentosStock.Any(e => e.Id == id);

        private async Task PopulateDropdowns(MovimentoStockViewModel? vm = null)
        {
            var motos = await _context.Motocicletas.OrderBy(m => m.Modelo)
                .Select(m => new SelectListItem($"{m.Modelo} ({m.NumeroQuadro})", m.Id.ToString())).ToListAsync();
            if (vm != null) vm.Motocicletas = motos;
            else ViewData["Motocicletas"] = motos;
        }
    }
}
