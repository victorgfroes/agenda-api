using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.DTOs.Agenda
{
    public class UpdateTituloAgendaDTO
    {
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }
    }
}
