using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Interfaces;

namespace TrilhaApiDesafio.Services
{
    public class HistoricoTarefaService : IHistoricoTarefaService
    {
        private IHistoricoTarefa _tarefa;

        public HistoricoTarefaService(IHistoricoTarefa tarefa)
        {
            _tarefa = tarefa;
        }

        public void Criar(HistoricoTarefa historico)
        {
            _tarefa.Incluir(historico);
        }

        public IEnumerable<HistoricoTarefa> ObterHistoricoPorIdDaTarefa(int idTarefa)
        {
            var historico = _tarefa.BuscarTodos().Where(tarefa => tarefa.TarefaId == idTarefa);

            return historico;
        }
    }
}
