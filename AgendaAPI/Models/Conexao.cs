using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.Models
{
    public class Conexao
    {
        [Key]
        [Required]
        public int Id_Conexao { get; set; }        
        [Required]
        public int Id_Google_Solicitante_FK { get; set; }
        [Required]
        [StringLength(500)]
        public string Nome_Solicitante_FK { get; set; }
        [Required]
        [StringLength(100)]
        public string Email_Solicitante_FK { get; set; }
        [Required]
        [StringLength(500)]
        public string Foto_Solicitante_FK { get; set; }
        [Required]
        public int Id_Google_Solicitado_FK { get; set; }
        [Required]
        [StringLength(100)]
        public string Email_Solicitado_FK { get; set; }
        public bool Aceito { get; set; }        
    }
}
