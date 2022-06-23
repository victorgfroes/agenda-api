using AgendaAPI.DTOs.Conexao;
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

        [HttpPost("SolicitaConexao/{idGoogleSolicitante}/{nomeSolicitante}/{email}/{foto}")]
        public async Task<ActionResult> SolicitaConexao([FromBody] CreateConexaoDTO conexaoDTO, int idGoogleSolicitante, string nomeSolicitante, string email, string foto)
        {
            foto = _queries.PercentDecode(foto);
            
            if (!_queries.GetOpenConexao(idGoogleSolicitante, conexaoDTO.Id_Google_Solicitado_FK) || !_queries.GetAcceptedConexao(idGoogleSolicitante, conexaoDTO.Id_Google_Solicitado_FK))
            {
                var conexao = await _queries.RequestConexao(conexaoDTO, idGoogleSolicitante, nomeSolicitante, email, foto);

                if (conexao != null)
                {
                    return Ok(conexao);
                }
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao solicitar conexão!" }, _options));
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Solicitação de conexão já foi enviada para esse usuário!" }, _options));
        }

        [HttpGet("RecuperaSolicitacoesConexoesEmAbertoPorIdGoogle/{idGoogleSolicitado}")]
        public async Task<ActionResult> RecuperaSolicitacoesConexoesEmAbertoPorIdGoogle(int idGoogleSolicitado)
        {
            var conexao = await _queries.GetSolicitacoesConexoesEmAbertoByIdGoogle(idGoogleSolicitado);

            if (conexao.ToList().Count > 0)
            {
                return Ok(conexao);
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Não existem solicitações de conexão em aberto para esse usuário!" }, _options));
        }

        [HttpPut("AceitarConexao/{idConexao}/{idGoogleSolicitante}/{idGoogleSolicitado}")]
        public async Task<ActionResult> AceitarConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            var conexao = await _queries.AcceptConexao(idConexao, idGoogleSolicitante, idGoogleSolicitado);

            if (conexao.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao aceitar conexão!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Solicitação de conexão foi aceita!" }, _options));
        }

        [HttpPut("RecusarConexao/{idConexao}/{idGoogleSolicitante}/{idGoogleSolicitado}")]
        public async Task<ActionResult> RecusarConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            var conexao = await _queries.RefuseConexao(idConexao, idGoogleSolicitante, idGoogleSolicitado);

            if (conexao.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao recusar conexão!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Solicitação de conexão foi recusada!" }, _options));
        }

        [HttpGet("RecuperaConexoesPorIdGoogle/{idGoogleSolicitante}")]
        public async Task<ActionResult> RecuperaConexoesPorIdGoogle(int idGoogleSolicitante)
        {
            var conexao = await _queries.GetConexoesByIdGoogle(idGoogleSolicitante);

            if (conexao.ToList().Count > 0)
            {
                return Ok(conexao);
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Não existem conexões com outros usuários!" }, _options));
        }

        [HttpDelete("DeletaConexao/{idConexao}/{idGoogleSolicitante}/{idGoogleSolicitado}")]
        public async Task<ActionResult> DeletaConexao(int idConexao, int idGoogleSolicitante, int idGoogleSolicitado)
        {
            var conexao = await _queries.DeleteConexao(idConexao, idGoogleSolicitante, idGoogleSolicitado);

            if (conexao.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao excluir conexão!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Conexão excluída!" }, _options));
        }
    }
}
