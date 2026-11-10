using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotoStock.Data;
using MotoStock.Models;
using MotoStock.ViewModels;

namespace MotoStock.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedores = await _context.Fornecedores.OrderBy(f => f.Nome).ToListAsync();
            return View(fornecedores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var fornecedor = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);
            if (fornecedor == null) return NotFound();
            return View(fornecedor);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NIF,Email,Telefone,Morada,CodigoPostal,Localidade,DataRegisto,Ativo")] FornecedorViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var f = new Fornecedor
            {
                Nome = vm.Nome, NIF = vm.NIF, Email = vm.Email, Telefone = vm.Telefone,
                Morada = vm.Morada, CodigoPostal = vm.CodigoPostal, Localidade = vm.Localidade,
                DataRegisto = vm.DataRegisto, Ativo = vm.Ativo
            };
            _context.Add(f);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Fornecedor criado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var f = await _context.Fornecedores.FindAsync(id);
            if (f == null) return NotFound();
            return View(new FornecedorViewModel
            {
                Id = f.Id, Nome = f.Nome, NIF = f.NIF, Email = f.Email, Telefone = f.Telefone,
                Morada = f.Morada, CodigoPostal = f.CodigoPostal, Localidade = f.Localidade,
                DataRegisto = f.DataRegisto, Ativo = f.Ativo
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NIF,Email,Telefone,Morada,CodigoPostal,Localidade,DataRegisto,Ativo")] FornecedorViewModel vm)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid) return View(vm);
            try
            {
                var f = await _context.Fornecedores.FindAsync(id);
                if (f == null) return NotFound();
                f.Nome = vm.Nome; f.NIF = vm.NIF; f.Email = vm.Email; f.Telefone = vm.Telefone;
                f.Morada = vm.Morada; f.CodigoPostal = vm.CodigoPostal; f.Localidade = vm.Localidade;
                f.DataRegisto = vm.DataRegisto; f.Ativo = vm.Ativo;
                _context.Update(f);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Fornecedor atualizado com sucesso.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(vm.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var f = await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);
            if (f == null) return NotFound();
            return View(f);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var f = await _context.Fornecedores.FindAsync(id);
            if (f != null)
            {
                _context.Fornecedores.Remove(f);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Fornecedor eliminado com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id) => _context.Fornecedores.Any(e => e.Id == id);
    }
}
