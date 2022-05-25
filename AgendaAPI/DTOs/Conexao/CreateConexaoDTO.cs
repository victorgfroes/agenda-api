using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.DTOs.Conexao
{
    public class CreateConexaoDTO
    {
        [Required]
        public int Id_Google_Solicitado_FK { get; set; }
        [Required]
        [StringLength(100)]
        public string Email_Solicitado_FK { get; set; }
    }
}
