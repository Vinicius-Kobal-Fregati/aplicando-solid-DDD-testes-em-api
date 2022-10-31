using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Dtos
{
    public class ReadHistoricoTarefaDto
    {
        public int FuncionarioId { get; set; }
        public EnumStatusTarefa StatusTarefa { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
