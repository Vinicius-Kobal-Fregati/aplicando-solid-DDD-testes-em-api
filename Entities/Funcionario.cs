using System.ComponentModel.DataAnnotations;

namespace TrilhaApiDesafio.Entities
{
    /// <summary>
    /// Entidade que ilustra um funcionário com suas principais informações, como nome, e-mail e telefone.
    /// </summary>
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }
    }
}