using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Profiles
{
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<CreateTarefaDto, Tarefa>();
            CreateMap<Tarefa, ReadTarefaDtos>();
            CreateMap<Tarefa, ReadTarefaDtoSemFuncionarioId>();
        }
    }
}
