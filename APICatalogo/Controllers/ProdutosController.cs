using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context) {
            _context = context;
        }

        [HttpGet("/testeRestricoes/{nome:length(3)}/{valor:decimal:range(2,5)}")] //Só aceita 3 caracteres
        public ActionResult<String> Get(string nome, decimal valor) {
            if (nome is null) return BadRequest("Nome é nulo");

            return $"Nome é {nome} e valor é {valor}";
        }

        //Usando async await
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get() {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (produtos is null) return NotFound("Produtos não encontrados...");
            return produtos;
        }

        [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id) {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null) return NotFound("Produto não encontrado...");
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto) {

            if (produto is null) return BadRequest("Produto inválido!");

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto) {
            if (id != produto.ProdutoId) return BadRequest("Id não correspondente.");

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null) return BadRequest("Produto não encontrado!");

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
