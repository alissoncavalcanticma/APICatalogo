using APICatalogo.Context;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CategoriasController
    {
        public readonly AppDbContext _context;

        public CategoriasController(AppDbContext context) {
            _context = context;
        }
    }
}
