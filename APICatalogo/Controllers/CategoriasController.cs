using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoriasController : ControllerBase
    {
        public readonly AppDbContext _context;

        public CategoriasController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get() {

            try {
                var categorias = _context.Categorias.AsNoTracking().ToList();
                if (categorias is null) return NotFound("Categorias não encontradas...");

                return Ok(categorias);

                //Testando lançamento de exceção
                //throw new DataMisalignedException();
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro não esperado...");
            }
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> getCategoriasProdutos() {
            var categorias = _context.Categorias.Include(p => p.Produtos).ToList();

            if (categorias is null) return NotFound("Categorias não encontradas...");
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id) {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null) return NotFound("Categoria não encontrada...");

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Categoria> Post(Categoria categoria) {

            if (categoria is null) return BadRequest("Categoria inválida!");

            _context.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId}, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Categoria> Put(int id, Categoria categoria) {
            if (id != categoria.CategoriaId) return BadRequest("CategoriaId não correspondente!");

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }


        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id) {

            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);

            if (categoria is null) return NotFound("Categoria não encontrada!");

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }



    }
}
