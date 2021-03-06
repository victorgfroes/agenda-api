using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.DTOs.Agenda
{
    public class CreateAgendaDTO
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        [Required]
        [StringLength(150)]
        public string Descricao { get; set; }
        [Required]
        public DateTime Dt_Fim { get; set; }
    }
}
