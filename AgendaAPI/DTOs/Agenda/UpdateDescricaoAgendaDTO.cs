using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.DTOs.Agenda
{
    public class UpdateDescricaoAgendaDTO
    {
        [Required]
        [StringLength(150)]
        public string Descricao { get; set; }
    }
}
