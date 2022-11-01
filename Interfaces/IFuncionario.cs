using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Interfaces
{
    public interface IFuncionario : IQuery<Funcionario>, ICommand<Funcionario>
    {
    }
}
