using AgendaAPI.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IQueriesService _queries;
        private readonly JsonSerializerOptions _options;

        public UsuarioController(IQueriesService queries)
        {
            _queries = queries;
            _options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        [HttpPost("AdicionaUsuario/{idGoogle}/{email}/{nome}/{foto}")]
        public async Task<ActionResult> AdicionaUsuario(int idGoogle, string email, string nome, string foto)
        {
            foto = _queries.PercentDecode(foto);

            if (!_queries.GetUsuarioJaCadastrado(idGoogle, email))
            {
                var usuario = await _queries.InsertUsuario(idGoogle, email, nome, foto);

                if (usuario != null)
                {
                    return Ok(usuario);
                }
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao cadastrar usuario!" }, _options));
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Usuario já existente!" }, _options));
        }
        
        [HttpGet("ProcuraUsuario/{email}")]
        public async Task<ActionResult> ProcuraUsuario(string email)
        {
            var usuario = await _queries.GetUsuario(email);

            if (usuario.ToList().Count > 0)
            {
                return Ok(usuario);
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Usuario não existente!" }, _options));
        }        
    }
}
