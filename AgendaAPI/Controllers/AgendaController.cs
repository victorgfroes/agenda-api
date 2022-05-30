﻿using AgendaAPI.DTOs.Agenda;
using AgendaAPI.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AgendaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IQueriesService _queries;

        public AgendaController(IQueriesService queries)
        {
            _queries = queries;            
        }

        [HttpPost("AdicionaAgenda/{idUsuario}/{idGoogle}/{email}/{nome}")]
        public async Task<ActionResult> AdicionaAgenda([FromBody] CreateAgendaDTO agendaDTO, int idUsuario, int idGoogle, string email, string nome)
        {
            var agenda = await _queries.InsertAgenda(agendaDTO, idUsuario, idGoogle, email, nome);

            if (agenda != null)
            {
                return Ok(agenda);
            }
            return NotFound();
        }

        [HttpGet("RecuperaAgendasPorIdGoogle/{idGoogle}")]
        public async Task<ActionResult> RecuperaAgendasPorIdGoogle(int idGoogle)
        {
            var agenda = await _queries.GetAgendasByIdGoogle(idGoogle);

            if (agenda != null)
            {
                return Ok(agenda);
            }
            return NotFound();
        }

        [HttpPut("AtualizaAgendaPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaAgendaPorIdAgenda([FromBody] UpdateAgendaDTO agendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateAgendaByIdAgenda(agendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("AtualizaTituloPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaTituloPorIdAgenda([FromBody] UpdateTituloAgendaDTO tituloAgendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateTituloByIdAgenda(tituloAgendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("AtualizaDescricaoPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaDescricaoPorIdAgenda([FromBody] UpdateDescricaoAgendaDTO descricaoAgendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateDescricaoByIdAgenda(descricaoAgendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("AtualizaDtFimPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> AtualizaDtFimPorIdAgenda([FromBody] UpdateDtFimAgendaDTO dtFimAgendaDTO, int idAgenda)
        {
            var agenda = await _queries.UpdateDtFimByIdAgenda(dtFimAgendaDTO, idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("DeletaAgendaPorIdAgenda/{idAgenda}")]
        public async Task<ActionResult> DeletaAgendaPorIdAgenda(int idAgenda)
        {
            var agenda = await _queries.DeleteAgendaByIdAgenda(idAgenda);

            if (agenda.Equals(0))
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
