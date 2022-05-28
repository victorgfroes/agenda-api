using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.Models
{
    public class Agenda
    {
        [Key]
        [Required]
        public int Id_Agenda { get; set; }
        [Required]
        public int Id_Usuario_FK { get; set; }
        [Required]
        public int Id_Google_FK { get; set; }
        [Required]
        [StringLength(100)]
        public string Email_FK { get; set; }
        [Required]
        [StringLength(500)]
        public string Nome_FK { get; set; }
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
