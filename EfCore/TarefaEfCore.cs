using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Interfaces;

namespace TrilhaApiDesafio.EfCore
{
    public class TarefaEfCore : ITarefa
    {
        private readonly OrganizadorContext _context;

        public TarefaEfCore(OrganizadorContext context)
        {
            _context = context;
        }

        public void Alterar(Tarefa objeto)
        {
            _context.Tarefas.Update(objeto);
            _context.SaveChanges();
        }

        public Tarefa BuscarPorId(int id)
        {
            return _context.Tarefas.Find(id);
        }

        public IEnumerable<Tarefa> BuscarTodos()
        {
            return _context.Tarefas;
        }

        public void Excluir(Tarefa objeto)
        {
            _context.Tarefas.Remove(objeto);
            _context.SaveChanges();
        }

        public void Incluir(Tarefa objeto)
        {
            _context.Tarefas.Add(objeto);
            _context.SaveChanges();
        }
    }
}
