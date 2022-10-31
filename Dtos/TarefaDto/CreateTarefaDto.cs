using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Dtos
{
    public class CreateTarefaDto
    {
        [Required(ErrorMessage = "O campo título é obrigatório")]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
        public int FuncionarioId { get; set; }
    }
}
