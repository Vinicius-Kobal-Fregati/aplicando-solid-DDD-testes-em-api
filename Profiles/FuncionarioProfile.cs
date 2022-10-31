using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Entities;

namespace TrilhaApiDesafio.Profiles
{
    public class FuncionarioProfile : Profile
    {
        public FuncionarioProfile()
        {
            CreateMap<CreateFuncionarioDto, Funcionario>();
            CreateMap<Funcionario, ReadFuncionarioDto>();
        }
    }
}
