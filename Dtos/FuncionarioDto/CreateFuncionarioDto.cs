using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Dtos
{
    public class CreateFuncionarioDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }
    }
}
