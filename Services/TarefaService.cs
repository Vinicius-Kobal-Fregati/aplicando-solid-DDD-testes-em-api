using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Interfaces;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public class TarefaService : ITarefaService
    {
        ITarefa _tarefa;
        IFuncionario _funcionario;
        IMapper _mapper;

        public TarefaService(ITarefa tarefa, IFuncionario funcionario, IMapper mapper)
        {
            _tarefa = tarefa;
            _funcionario = funcionario;
            _mapper = mapper;
        }

        public void Atualizar(int id, Tarefa tarefa)
        {
            Tarefa tarefaASerAtualizada = _tarefa.BuscarPorId(id);
            if (tarefaASerAtualizada != null)
            {
                tarefaASerAtualizada.Titulo = tarefa.Titulo;
                tarefaASerAtualizada.Status = tarefa.Status;
                tarefaASerAtualizada.Descricao = tarefa.Descricao;
                tarefaASerAtualizada.Data = tarefa.Data;
                tarefaASerAtualizada.FuncionarioId = tarefa.FuncionarioId;

                _tarefa.Alterar(tarefaASerAtualizada);
            }
        }

        public Tarefa AtualizarData(int id, DateTime data)
        {
            Tarefa tarefaASerAtualizada = _tarefa.BuscarPorId(id);
            if (tarefaASerAtualizada != null)
            {
                tarefaASerAtualizada.Data = data;
                _tarefa.Alterar(tarefaASerAtualizada);
            }

            return tarefaASerAtualizada;
        }

        public Tarefa AtualizarDescricao(int id, string descricao)
        {
            Tarefa tarefaASerAtualizada = _tarefa.BuscarPorId(id);
            if (tarefaASerAtualizada != null)
            {
                tarefaASerAtualizada.Descricao = descricao;
                _tarefa.Alterar(tarefaASerAtualizada);
            }

            return tarefaASerAtualizada;
        }

        public void AtualizarFuncionario(int id, int idFuncionario)
        {
            Tarefa tarefaASerAtualizada = _tarefa.BuscarPorId(id);
            if (tarefaASerAtualizada != null)
            {
                tarefaASerAtualizada.FuncionarioId = idFuncionario;
                _tarefa.Alterar(tarefaASerAtualizada);
            }
        }

        public Tarefa AtualizarStatus(int id, EnumStatusTarefa status)
        {
            Tarefa tarefaASerAtualizada = _tarefa.BuscarPorId(id);
            if (tarefaASerAtualizada != null)
            {
                tarefaASerAtualizada.Status = status;
                _tarefa.Alterar(tarefaASerAtualizada);
            }

            return tarefaASerAtualizada;
        }

        public Tarefa AtualizarTitulo(int id, string titulo)
        {
            Tarefa tarefaASerAtualizada = _tarefa.BuscarPorId(id);
            if (tarefaASerAtualizada != null)
            {
                tarefaASerAtualizada.Titulo = titulo;
                _tarefa.Alterar(tarefaASerAtualizada);
            }

            return tarefaASerAtualizada;
        }

        public void Criar(Tarefa tarefa)
        {
            _tarefa.Incluir(tarefa);
        }

        public void Deletar(int id)
        {
            Tarefa tarefaASerRemovida = _tarefa.BuscarPorId(id);
            _tarefa.Excluir(tarefaASerRemovida);
        }

        public IEnumerable<Tarefa> ObterPorData(DateTime data)
        {
            return _tarefa.BuscarTodos().Where(tarefa => tarefa.Data == data);
        }

        public ReadTarefaDtos ObterPorId(int id)
        {
            Tarefa tarefa = _tarefa.BuscarPorId(id);
            return _mapper.Map<ReadTarefaDtos>(tarefa);
        }

        public IEnumerable<Tarefa> ObterPorStatus(EnumStatusTarefa status)
        {
            return _tarefa.BuscarTodos().Where(tarefa => tarefa.Status == status);
        }

        public IEnumerable<Tarefa> ObterPorTitulo(string titulo)
        {
            return _tarefa.BuscarTodos().Where(tarefa => tarefa.Titulo.Contains(titulo));
        }

        public Funcionario ObterResponsavel(int id)
        {
            int funcionarioId = _tarefa.BuscarTodos().FirstOrDefault
                                (tarefa => tarefa.FuncionarioId == id).FuncionarioId;
            return _funcionario.BuscarPorId(funcionarioId);
        }

        public IEnumerable<Tarefa> ObterTodos()
        {
            return _tarefa.BuscarTodos();
        }
    }
}
