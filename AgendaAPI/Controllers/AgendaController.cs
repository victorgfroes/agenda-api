using AgendaAPI.DTOs.Agenda;
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
    public class AgendaController : ControllerBase
    {
        private readonly IQueriesService _queries;
        private readonly JsonSerializerOptions _options;

        public AgendaController(IQueriesService queries)
        {
            _queries = queries;
            _options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
        }

        [HttpPost("AdicionaAgenda/{idUsuario}/{idGoogle}/{email}/{nome}/{foto}")]
        public async Task<ActionResult> AdicionaAgenda([FromBody] CreateAgendaDTO agendaDTO, int idUsuario, int idGoogle, string email, string nome, string foto)
        {
            var agenda = await _queries.InsertAgenda(agendaDTO, idUsuario, idGoogle, email, nome, foto);

            if (agenda != null)
            {
                return Ok(agenda);
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao inserir agenda!" }, _options));
        }

        [HttpGet("RecuperaAgendasPorIdGoogle/{idGoogle}")]
        public async Task<ActionResult> RecuperaAgendasPorIdGoogle(int idGoogle)
        {
            var agenda = await _queries.GetAgendasByIdGoogle(idGoogle);

            if (agenda.ToList().Count > 0)
            {
                return Ok(agenda);
            }
            return NotFound(JsonSerializer.Serialize(new { mensagem = $"Não existem agendas cadastradas!" }, _options));
        }

        [HttpPut("AtualizaAgendaPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaAgendaPorIdAgenda([FromBody] UpdateAgendaDTO agendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateAgendaByIdAgenda(agendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao atualizar agenda!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Agenda atualizada com sucesso!" }, _options));
        }

        [HttpPut("AtualizaTituloPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaTituloPorIdAgenda([FromBody] UpdateTituloAgendaDTO tituloAgendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateTituloByIdAgenda(tituloAgendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao atualizar agenda!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Agenda atualizada com sucesso!" }, _options));
        }

        [HttpPut("AtualizaDescricaoPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaDescricaoPorIdAgenda([FromBody] UpdateDescricaoAgendaDTO descricaoAgendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateDescricaoByIdAgenda(descricaoAgendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao atualizar agenda!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Agenda atualizada com sucesso!" }, _options));
        }

        [HttpPut("AtualizaDtFimPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaDtFimPorIdAgenda([FromBody] UpdateDtFimAgendaDTO dtFimAgendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateDtFimByIdAgenda(dtFimAgendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao atualizar agenda!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Agenda atualizada com sucesso!" }, _options));
        }

        [HttpDelete("DeletaAgendaPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> DeletaAgendaPorIdAgenda(int idAgenda)
        {
            var agenda = await _queries.DeleteAgendaByIdAgenda(idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound(JsonSerializer.Serialize(new { mensagem = $"Erro ao deletar agenda!" }, _options));
            }
            return Ok(JsonSerializer.Serialize(new { mensagem = $"Agenda deletada com sucesso!" }, _options));
        }
    }
}
