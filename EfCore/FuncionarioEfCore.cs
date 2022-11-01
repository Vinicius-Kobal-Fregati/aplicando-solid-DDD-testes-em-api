using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Interfaces;

namespace TrilhaApiDesafio.EfCore
{
    public class FuncionarioEfCore : IFuncionario
    {
        private readonly OrganizadorContext _context;

        public FuncionarioEfCore(OrganizadorContext context)
        {
            _context = context;
        }

        public void Alterar(Funcionario objeto)
        {
            _context.Funcionarios.Update(objeto);
            _context.SaveChanges();
        }

        public Funcionario BuscarPorId(int id)
        {
            return _context.Funcionarios.Find(id);
        }

        public IEnumerable<Funcionario> BuscarTodos()
        {
            return _context.Funcionarios;
        }

        public void Excluir(Funcionario objeto)
        {
            _context.Funcionarios.Remove(objeto);
            _context.SaveChanges();
        }

        public void Incluir(Funcionario objeto)
        {
            _context.Funcionarios.Add(objeto);
            _context.SaveChanges();
        }
    }
}
