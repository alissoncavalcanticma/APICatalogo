using APICatalogo.Context;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {

        public readonly AppDbContext _context;

        public ServicesController(AppDbContext context) {
            _context = context;
        }


        [HttpGet]
        public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }
    }
}
