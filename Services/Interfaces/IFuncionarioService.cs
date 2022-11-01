using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Services
{
    public interface IFuncionarioService
    {
        IEnumerable<Funcionario> ObterTodos();
        IEnumerable<Tarefa> TarefasDoFuncionario(int id);
        Funcionario ObterPorId(int id);
        IEnumerable<Funcionario> ObterPorNome(string nome);
        IEnumerable<Funcionario> ObterPorEmail(string email);
        IEnumerable<Funcionario> ObterPorTelefone(string telefone);
        void CadastrarFuncionario(Funcionario funcionario);
        Funcionario AtualizarFuncionario(int id, Funcionario funcionario);
        Funcionario AtualizarNome(int id, string nome);
        Funcionario AtualizarEmail(int id, string email);
        Funcionario AtualizarTelefone(int id, string telefone);
        void Deletar(int id);
    }
}
