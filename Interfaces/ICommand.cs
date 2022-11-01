namespace TrilhaApiDesafio.Interfaces
{
    public interface ICommand<T>
    {
        void Incluir(T objeto);
        void Alterar(T objeto);
        void Excluir(T objeto);
    }
}
