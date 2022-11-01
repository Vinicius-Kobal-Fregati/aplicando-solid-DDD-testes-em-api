using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Interfaces;

namespace TrilhaApiDesafio.EfCore
{
    public class HistoricoTarefaEfCore : IHistoricoTarefa
    {
        private readonly OrganizadorContext _context;

        public HistoricoTarefaEfCore(OrganizadorContext context)
        {
            _context = context;
        }

        public IEnumerable<HistoricoTarefa> BuscarTodos()
        {
            return _context.HistoricoTarefas;
        }

        public void Incluir(HistoricoTarefa historico)
        {
            _context.HistoricoTarefas.Add(historico);
            _context.SaveChanges();
        }
    }
}
