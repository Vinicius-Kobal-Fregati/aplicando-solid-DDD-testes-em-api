using AutoMapper;
using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Interfaces;

namespace TrilhaApiDesafio.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        ITarefa _tarefa;
        IFuncionario _funcionario;
        IMapper _mapper; // Ver se vou utilizar o map aqui ou na controller.

        public FuncionarioService(ITarefa tarefa, IFuncionario funcionario, IMapper mapper)
        {
            _tarefa = tarefa;
            _funcionario = funcionario;
            _mapper = mapper;
        }

        public Funcionario AtualizarEmail(int id, string email)
        {
            Funcionario funcionario = _funcionario.BuscarPorId(id);
            if (funcionario != null)
            {
                funcionario.Email = email;
                _funcionario.Alterar(funcionario);
            }

            return funcionario;
        }

        public Funcionario AtualizarFuncionario(int id, Funcionario funcionario)
        {
            Funcionario funcionarioAntigo = _funcionario.BuscarPorId(id);
            if (funcionarioAntigo != null && funcionario != null)
            {
                funcionarioAntigo.Telefone = funcionario.Telefone;
                funcionarioAntigo.Nome = funcionario.Nome;
                funcionarioAntigo.Email = funcionario.Email;
                _funcionario.Alterar(funcionario);
            }

            return funcionario;
        }

        public Funcionario AtualizarNome(int id, string nome)
        {
            Funcionario funcionario = _funcionario.BuscarPorId(id);
            if (funcionario != null)
            {
                funcionario.Nome = nome;
                _funcionario.Alterar(funcionario);
            }

            return funcionario;
        }

        public Funcionario AtualizarTelefone(int id, string telefone)
        {
            Funcionario funcionario = _funcionario.BuscarPorId(id);
            if (funcionario != null)
            {
                funcionario.Telefone = telefone;
                _funcionario.Alterar(funcionario);
            }

            return funcionario;
        }

        public void CadastrarFuncionario(Funcionario funcionario)
        {
            _funcionario.Incluir(funcionario);
        }

        public void Deletar(int id)
        {
            _funcionario.Excluir(_funcionario.BuscarPorId(id));
        }

        public IEnumerable<Funcionario> ObterPorEmail(string email)
        {
            return _funcionario.BuscarTodos().Where(funcionario => funcionario.Email.Contains(email));
        }

        public Funcionario ObterPorId(int id)
        {
            return _funcionario.BuscarPorId(id);
        }

        public IEnumerable<Funcionario> ObterPorNome(string nome)
        {
            return _funcionario.BuscarTodos().Where(funcionario => funcionario.Nome.Contains(nome));
        }

        public IEnumerable<Funcionario> ObterPorTelefone(string telefone)
        {
            return _funcionario.BuscarTodos().Where(funcionario => funcionario.Telefone.Contains(telefone));
        }

        public IEnumerable<Funcionario> ObterTodos()
        {
            return _funcionario.BuscarTodos();
        }

        public IEnumerable<Tarefa> TarefasDoFuncionario(int id)
        {
            IEnumerable<Tarefa> tarefas = _tarefa.BuscarTodos();

            if (tarefas != null)
            {
                tarefas = from tarefa in tarefas
                          where tarefa.FuncionarioId.Equals(id)
                          select tarefa;
            }

            return tarefas;
        }
    }
}
