using AgendaAPI.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IQueriesService _queries;

        public UsuarioController(IQueriesService queries)
        {
            _queries = queries;
        }

        [HttpPost("AdicionaUsuario/{idGoogle}/{email}/{nome}/{foto}")]
        public async Task<ActionResult> AdicionaUsuario(int idGoogle, string email, string nome, string foto)
        {
            var usuario = await _queries.InsertUsuario(idGoogle, email, nome, foto);

            if(usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound();
        }
        
        [HttpGet("ProcuraUsuario/{email}")]
        public async Task<ActionResult> ProcuraUsuario(string email)
        {
            var usuario = await _queries.GetUsuario(email);

            if (usuario != null)
            {
                return Ok(usuario);
            }
            return NotFound();
        }
    }
}
