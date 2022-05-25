using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.DTOs.Agenda
{
    public class UpdateDtFimAgendaDTO
    {
        [Required]
        public DateTime Dt_Fim { get; set; }
    }
}
