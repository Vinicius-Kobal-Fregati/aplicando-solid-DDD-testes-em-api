using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Interfaces
{
    public interface ITarefa : ICommand<Tarefa>, IQuery<Tarefa>
    {
    }
}
