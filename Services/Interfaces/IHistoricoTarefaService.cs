using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Services
{
    public interface IHistoricoTarefaService
    {
        IEnumerable<HistoricoTarefa> ObterHistoricoPorIdDaTarefa(int idTarefa);
        void Criar(HistoricoTarefa historico);
    }
}
