using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Profiles
{
    public class HistoricoTarefaProfile : Profile
    {
        public HistoricoTarefaProfile()
        {
            CreateMap<HistoricoTarefa, ReadHistoricoTarefaDto>();
        }
    }
}
