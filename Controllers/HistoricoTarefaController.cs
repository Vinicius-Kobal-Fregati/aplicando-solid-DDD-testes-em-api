using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Entities;
using AutoMapper;
using TrilhaApiDesafio.Dtos;
using TrilhaApiDesafio.Services;

namespace TrilhaApiDesafio.Controllers
{
    /// <summary>
    /// Controller da entidade HistoricoTarefa, ela manipula os eventos, determinando qual resposta enviar para o usuário dependendo da 
    /// requisição e do que foi passado.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HistoricoTarefaController : ControllerBase
    {
        private IMapper _mapper;
        private IHistoricoTarefaService _historico;

        /// <summary>
        /// Construtor da controller funcionario.
        /// </summary>
        /// <param name="context">Contexto passado a controller.</param>
        public HistoricoTarefaController(IMapper mapper, IHistoricoTarefaService historico)
        {
            _mapper = mapper;
            _historico = historico;
        }

        /// <summary>
        /// Deve ser utilizado para saber todo o histórico de uma tarefa pelo seu Id.
        /// </summary>
        /// <param name="idTarefa">Id da tarefa a qual se deseja ver o histórico.</param>
        /// <returns>Retorna o status 200 ou 400 com suas informações no corpo.</returns>
        [HttpGet("{idTarefa}")]
        public IActionResult ObterHistoricoPorIdDaTarefa(int idTarefa)
        {
            var historico = _historico.ObterHistoricoPorIdDaTarefa(idTarefa);
            IEnumerable<ReadHistoricoTarefaDto> readDto = _mapper.Map<IEnumerable<ReadHistoricoTarefaDto>>(historico);

            if (readDto == null)
                return NotFound(new { Error = Textos.NaoNulo("Histórico") });

            return Ok(readDto);
        }

        /// <summary>
        /// Registra um novo histórico na database.
        /// </summary>
        /// <param name="historico">Histórico a ser adicionado.</param>
        /// <returns>Retorna 201 ou 400.</returns>
        [NonAction]
        [HttpPost] //Talvez mudar para bool
        public IActionResult Criar(HistoricoTarefa historico)
        {
            if (historico == null)
                return BadRequest(new { Error = Textos.NaoNulo("Histórico") });

            _historico.Criar(historico);

            return CreatedAtAction(nameof(historico), new { id = historico.Id}, historico);
        }
    }
}