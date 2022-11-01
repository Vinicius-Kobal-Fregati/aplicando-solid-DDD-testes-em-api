using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Interfaces
{
    public interface IHistoricoTarefa
    {
        void Incluir(HistoricoTarefa historico);
        IEnumerable<HistoricoTarefa> BuscarTodos();
    }
}
