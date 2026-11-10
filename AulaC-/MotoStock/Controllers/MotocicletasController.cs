using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoStock.Data;
using MotoStock.Models;
using MotoStock.ViewModels;

namespace MotoStock.Controllers
{
    public class MotocicletasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotocicletasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Motocicletas
                .Include(m => m.Marca).Include(m => m.Categoria).Include(m => m.Fornecedor)
                .OrderBy(m => m.Modelo).ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.Motocicletas
                .Include(m => m.Marca).Include(m => m.Categoria).Include(m => m.Fornecedor)
                .Include(m => m.MovimentosStock)
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,MarcaId,CategoriaId,FornecedorId,Modelo,Cilindrada,Ano,Cor,Matricula,NumeroQuadro,Quilometragem,TipoMotor,DataRegisto,PrecoCompra,PrecoVenda,Estado,Descricao,Observacoes")] MotocicletaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
                return View(vm);
            }
            var m = new Motocicleta
            {
                MarcaId = vm.MarcaId, CategoriaId = vm.CategoriaId, FornecedorId = vm.FornecedorId,
                Modelo = vm.Modelo, Cilindrada = vm.Cilindrada, Ano = vm.Ano, Cor = vm.Cor,
                Matricula = vm.Matricula, NumeroQuadro = vm.NumeroQuadro, Quilometragem = vm.Quilometragem,
                TipoMotor = vm.TipoMotor, DataRegisto = vm.DataRegisto, PrecoCompra = vm.PrecoCompra,
                PrecoVenda = vm.PrecoVenda, Estado = vm.Estado, Descricao = vm.Descricao, Observacoes = vm.Observacoes
            };
            _context.Add(m);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Motocicleta criada com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.Motocicletas.FindAsync(id);
            if (m == null) return NotFound();
            var vm = new MotocicletaViewModel
            {
                Id = m.Id, MarcaId = m.MarcaId, CategoriaId = m.CategoriaId, FornecedorId = m.FornecedorId,
                Modelo = m.Modelo, Cilindrada = m.Cilindrada, Ano = m.Ano, Cor = m.Cor,
                Matricula = m.Matricula, NumeroQuadro = m.NumeroQuadro, Quilometragem = m.Quilometragem,
                TipoMotor = m.TipoMotor, DataRegisto = m.DataRegisto, PrecoCompra = m.PrecoCompra,
                PrecoVenda = m.PrecoVenda, Estado = m.Estado, Descricao = m.Descricao, Observacoes = m.Observacoes
            };
            await PopulateDropdowns(vm);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MarcaId,CategoriaId,FornecedorId,Modelo,Cilindrada,Ano,Cor,Matricula,NumeroQuadro,Quilometragem,TipoMotor,DataRegisto,PrecoCompra,PrecoVenda,Estado,Descricao,Observacoes")] MotocicletaViewModel vm)
        {
            if (id != vm.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(vm);
                return View(vm);
            }
            try
            {
                var m = await _context.Motocicletas.FindAsync(id);
                if (m == null) return NotFound();
                m.MarcaId = vm.MarcaId; m.CategoriaId = vm.CategoriaId; m.FornecedorId = vm.FornecedorId;
                m.Modelo = vm.Modelo; m.Cilindrada = vm.Cilindrada; m.Ano = vm.Ano; m.Cor = vm.Cor;
                m.Matricula = vm.Matricula; m.NumeroQuadro = vm.NumeroQuadro; m.Quilometragem = vm.Quilometragem;
                m.TipoMotor = vm.TipoMotor; m.DataRegisto = vm.DataRegisto; m.PrecoCompra = vm.PrecoCompra;
                m.PrecoVenda = vm.PrecoVenda; m.Estado = vm.Estado; m.Descricao = vm.Descricao; m.Observacoes = vm.Observacoes;
                _context.Update(m);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Motocicleta atualizada com sucesso.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotocicletaExists(vm.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var m = await _context.Motocicletas
                .Include(m => m.Marca).Include(m => m.Categoria).Include(m => m.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _context.Motocicletas.FindAsync(id);
            if (m != null)
            {
                _context.Motocicletas.Remove(m);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Motocicleta eliminada com sucesso.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MotocicletaExists(int id) => _context.Motocicletas.Any(e => e.Id == id);

        private async Task PopulateDropdowns(MotocicletaViewModel? vm = null)
        {
            var marcas = await _context.Marcas.Where(m => m.Ativo).OrderBy(m => m.Nome)
                .Select(m => new SelectListItem(m.Nome, m.Id.ToString())).ToListAsync();
            var categorias = await _context.Categorias.Where(c => c.Ativo).OrderBy(c => c.Nome)
                .Select(c => new SelectListItem(c.Nome, c.Id.ToString())).ToListAsync();
            var fornecedores = await _context.Fornecedores.Where(f => f.Ativo).OrderBy(f => f.Nome)
                .Select(f => new SelectListItem(f.Nome, f.Id.ToString())).ToListAsync();
            if (vm != null) { vm.Marcas = marcas; vm.Categorias = categorias; vm.Fornecedores = fornecedores; }
            else { ViewData["Marcas"] = marcas; ViewData["Categorias"] = categorias; ViewData["Fornecedores"] = fornecedores; }
        }
    }
}