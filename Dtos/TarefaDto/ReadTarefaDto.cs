using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Dtos
{
    public class ReadTarefaDtos
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
        public int FuncionarioId { get; set; }
    }
}
