using AgendaAPI.DTOs.Conexao;
using AgendaAPI.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConexaoController : ControllerBase
    {
        private readonly IQueriesService _queries;

        public ConexaoController(IQueriesService queries)
        {
            _queries = queries;
        }

        [HttpPost("SolicitaConexao/{idGoogleSolicitante}/{email}")]
        public async Task<ActionResult> SolicitaConexao([FromBody] CreateConexaoDTO conexaoDTO, int idSGoogleSolicitante, string email)
        {
            var conexao = await _queries.RequestConexao(conexaoDTO, idSGoogleSolicitante, email);

            if (conexao != null)
            {
                return Ok(conexao);
            }
            return NotFound();
        }

        [HttpGet("RecuperaSolicitacoesConexoesPorIdGoogle/{idGoogleSolicitado}")]
        public async Task<ActionResult> RecuperaSolicitacoesConexoesPorIdGoogle(int idGoogleSolicitado)
        {
            var conexao = await _queries.GetSolicitacoesConexoesByIdGoogle(idGoogleSolicitado);

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
    }
}
