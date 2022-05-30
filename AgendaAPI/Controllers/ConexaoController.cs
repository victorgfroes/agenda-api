using AgendaAPI.DTOs.Conexao;
using AgendaAPI.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConexaoController : ControllerBase
    {
        private readonly IQueriesService _queries;
        private readonly JsonSerializerOptions _options;

        public ConexaoController(IQueriesService queries)
        {
            _queries = queries;
            _options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        [HttpPost("SolicitaConexao/{idGoogleSolicitante}/{nomeSolicitante}/{email}")]
        public async Task<ActionResult> SolicitaConexao([FromBody] CreateConexaoDTO conexaoDTO, int idGoogleSolicitante,  string nomeSolicitante, string email)
        {
            if(!_queries.GetOpenAndAcceptedConexao(idGoogleSolicitante, conexaoDTO.Id_Google_Solicitado_FK))
            {
                var conexao = await _queries.RequestConexao(conexaoDTO, idGoogleSolicitante, nomeSolicitante, email);

                if (conexao != null)
                {
                    return Ok(conexao);
                }
                return NotFound();
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Solicitação de conexão já foi enviada para esse usuário!" }, _options));
        }

        [HttpGet("RecuperaSolicitacoesConexoesEmAbertoPorIdGoogle/{idGoogleSolicitado}")]
        public async Task<ActionResult> RecuperaSolicitacoesConexoesEmAbertoPorIdGoogle(int idGoogleSolicitado)
        {
            var conexao = await _queries.GetSolicitacoesConexoesEmAbertoByIdGoogle(idGoogleSolicitado);

            if (conexao != null)
            {
                return Ok(conexao);
            }
            return NotFound();
        }

        [HttpPut("AceitarConexao/{idConexao}/{idGoogleSolicitante}/{idGoogleSolicitado}")]
        public async Task<ActionResult> AceitarConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            var conexao = await _queries.AcceptConexao(idConexao, idGoogleSolicitante, idGoogleSolicitado);

            if (conexao.Equals(0))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("RecusarConexao/{idConexao}/{idGoogleSolicitante}/{idGoogleSolicitado}")]
        public async Task<ActionResult> RecusarConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            var conexao = await _queries.RefuseConexao(idConexao, idGoogleSolicitante, idGoogleSolicitado);

            if (conexao.Equals(0))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("RecuperaConexoesPorIdGoogle/{idGoogleSolicitante}")]
        public async Task<ActionResult> RecuperaConexoesPorIdGoogle(int idGoogleSolicitante)
        {
            var conexao = await _queries.GetConexoesByIdGoogle(idGoogleSolicitante);

            if (conexao != null)
            {
                return Ok(conexao);
            }
            return NotFound();
        }

        [HttpDelete("DeletaConexao/{idConexao}/{idGoogleSolicitante}/{idGoogleSolicitado}")]
        public async Task<ActionResult> DeletaConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            var conexao = await _queries.DeleteConexao(idConexao, idGoogleSolicitante, idGoogleSolicitado);

            if (conexao.Equals(0))
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
