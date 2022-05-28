using System.ComponentModel.DataAnnotations;

namespace AgendaAPI.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id_Usuario { get; set; }
        [Required]
        public int Id_Google { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(500)]
        public string Nome { get; set; }
        [Required]
        [StringLength(2000)]
        public string Foto { get; set; }
    }
}
