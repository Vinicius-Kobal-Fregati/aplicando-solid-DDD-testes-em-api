using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Entities;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Services
{
    public interface ITarefaService
    {
        ReadTarefaDtos ObterPorId(int id);
        IEnumerable<Tarefa> ObterTodos();
        IEnumerable<Tarefa> ObterPorTitulo(string titulo);
        IEnumerable<Tarefa> ObterPorData(DateTime data);
        IEnumerable<Tarefa> ObterPorStatus(EnumStatusTarefa status);
        Funcionario ObterResponsavel(int id);
        void Criar(Tarefa tarefa);
        void Atualizar(int id, Tarefa tarefa);
        Tarefa AtualizarTitulo(int id, string titulo);
        Tarefa AtualizarDescricao(int id, string descricao);
        Tarefa AtualizarData(int id, DateTime data);
        void AtualizarFuncionario(int id, int idFuncionario);
        Tarefa AtualizarStatus(int id, EnumStatusTarefa status);
        void Deletar(int id);
    }
}
