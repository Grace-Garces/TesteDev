namespace PessoasAPI.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class CriarPessoaDto
    {
        [Required]
        public string? Nome { get; set; }
        [Required]
        public string? CPF { get; set; }
        [Required]
        public string? UF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
