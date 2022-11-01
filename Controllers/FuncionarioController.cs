using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Entities;
using System.Text.RegularExpressions;
using TrilhaApiDesafio.Dtos;
using AutoMapper;
using TrilhaApiDesafio.Services;

namespace TrilhaApiDesafio.Controllers
{
    /// <summary>
    /// Controller da entidade Funcionario, ela manipula os eventos, determinando qual resposta enviar para o usuário dependendo da requisição
    /// e do que foi passado.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private IMapper _mapper;
        private IFuncionarioService _funcionario;

        /// <summary>
        /// Construtor da controller funcionario.
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="funcionario"></param>
        public FuncionarioController(IMapper mapper, IFuncionarioService funcionario)
        {
            _mapper = mapper;
            _funcionario = funcionario;
        }

        /// <summary>
        /// Método do tipo GET, utilizado para receber todos os funcionários que estão cadastrados.
        /// </summary>
        /// <returns>Pode retornar o status 404 ou 200, tendo em seu corpo o json com todos os funcionários cadastrados.</returns>
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var funcionarios = _funcionario.ObterTodos();
            
            return Ok(funcionarios);
        }

        /// <summary>
        /// Método do tipo GET, exibe todas as tarefas as que possuem responsabilidade de determinado funcionário.
        /// </summary>
        /// <param name="id">Id do funcionário responsável</param>
        /// <returns>Pode retornar status 404 ou 200, tendo em seu corpo o json com todas as tarefas atribuídas ao funcionário</returns>
        [HttpGet("TarefasDoFuncionario/{id}")]
        public IActionResult TarefasDoFuncionario(int id)
        {
            if (_funcionario.ObterPorId(id) == null)
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });

            IEnumerable<Tarefa> tarefas = _funcionario.TarefasDoFuncionario(id);
            IEnumerable<ReadTarefaDtoSemFuncionarioId> readDto = 
                _mapper.Map<IEnumerable<ReadTarefaDtoSemFuncionarioId>>(tarefas);

            return Ok(readDto);
        }

        /// <summary>
        /// Método GET usado para se obter um funcionário pelo seu Id.
        /// </summary>
        /// <param name="id">Id do usuário que se deseja encontrar.</param>
        /// <returns>Retorna status 404 ou 200, possuindo um json em seu corpo com as informações do funcionário.</returns>
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            if (id == 0)
                return NotFound(new {Error = Textos.NaoSelecionado("Funcionário")} );
            
            var funcionario = _funcionario.ObterPorId(id);
            
            if (VerificaNulo(funcionario))
                return NotFound(new {Error = Textos.NaoEncontrado("Funcionário")} );

            return Ok(funcionario);
        }

        /// <summary>
        /// Método GET usado para se obter todos os funcionários que tenham um determinado nome.
        /// </summary>
        /// <param name="nome">Nome do(s) funcionário(s) que se deseja pesquisar</param>
        /// <returns>Pode retornar status 404 ou 200, com um json em seu corpo com a informação de todos os funcionários com esse
        /// nome encontrados.</returns>
        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var funcionarios = _funcionario.ObterPorNome(nome);

            if (VerificaNulo(funcionarios))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });
            else if (funcionarios.Count() == 0)
                return NotFound(new { Error = Textos.NaoCadastrado("Funcionário") });

            return Ok(funcionarios);
        }

        /// <summary>
        /// Método GET usado para se obter o funcionário que tenha um determinado e-mail.
        /// </summary>
        /// <param name="email">Nome do funcionário que se deseja pesquisar</param>
        /// <returns>Pode retornar status 404 ou 200, com um json em seu corpo com a informação do funcionário com esse e-mail 
        /// encontrado. Note que se só passar uma parte do e-mail, ele poderá devolver outros que contenham essa parte.</returns>
        [HttpGet("ObterPorEmail")]
        public IActionResult ObterPorEmail(string email)
        {
            var funcionarios = _funcionario.ObterPorEmail(email);
            if (VerificaNulo(funcionarios))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });
            else if (funcionarios.Count() == 0)
                return NotFound(new { Error = Textos.NaoCadastrado("Funcionário") });

            return Ok(funcionarios);
        }

        /// <summary>
        /// Método GET usado para se obter o funcionário que tenha um determinado telefone.
        /// </summary>
        /// <param name="telefone">Nome do funcionário que se deseja pesquisar</param>
        /// <returns>Pode retornar status 404 ou 200, com um json em seu corpo com a informação do funcionário com esse telefone 
        /// encontrado. Note que se só passar uma parte do telefone, ele poderá devolver outros que contenham essa parte.</returns>
        [HttpGet("ObterPorTelefone")]
        public IActionResult ObterPorTelefone(string telefone)
        {
            var funcionario = _funcionario.ObterPorTelefone(telefone);

            if (VerificaNulo(funcionario) || funcionario.Count() == 0)
                return NotFound(new { Error = Textos.NaoCadastrado("Funcionário") });

            return Ok(funcionario);
        }

        /// <summary>
        /// Método POST, adiciona um novo funcionário no database, verificando-se algumas informações são vazias, como nome, email, telefone
        /// e se o nome já não existe.
        /// </summary>
        /// <param name="funcionario">Objeto da classe funcionário que se deseja adicionar</param>
        /// <returns>Pode retornar status 400 ou 201, possuindo informações como o nome da ação, a rota e o objeto adicionado.</returns>
        [HttpPost]
        public IActionResult CadastrarFuncionario(Funcionario funcionario)
        {
            if (NomeJaExistente(funcionario.Nome))
                return BadRequest(new { Error = Textos.JaExistente("Nome") });
            else if (!VerificaTelefone(funcionario.Telefone))
                return BadRequest(new { Error = Textos.TelefoneForaPadrao() });

            _funcionario.CadastrarFuncionario(funcionario);

            return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id}, funcionario);
        }

        /// <summary>
        /// Método PUT, utilizado para atualizar as informações de um funcionário já existente.
        /// </summary>
        /// <param name="id">Id do usuário que se quer atualizar</param>
        /// <param name="funcionario">Novas informações do usuário</param>
        /// <returns>Pode retornar status 204, 400 ou 404, não possuindo informações sobre o funcionário em seu corpo, visto que é uma
        /// atualização.</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizarFuncionario(int id, Funcionario funcionario)
        {
            if (VerificaNulo(funcionario))
                return BadRequest(new { Error = Textos.NaoNulo("Funcionário") });
            else if (funcionario.Nome == "")
                return BadRequest(new { Error = Textos.NaoVazio("Nome") });
            else if (NomeJaExistente(funcionario.Nome))
                return BadRequest(new { Error = Textos.JaExistente("Nome") });
            else if (!VerificaTelefone(funcionario.Telefone))
                return BadRequest(new { Error = Textos.TelefoneForaPadrao() });

            var funcionarioBanco = _funcionario.AtualizarFuncionario(id, funcionario);

            if (VerificaNulo(funcionarioBanco))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });
            
            return NoContent();
        }

        /// <summary>
        /// Método PATCH, ele permite atualizar uma parte das informações, nesse caso, o nome do funcionário, verificando se o mesmo 
        /// é vazio, nulo ou repetido.
        /// </summary>
        /// <param name="id">Id do usuário que se quer atualizar</param>
        /// <param name="nome">Novo nome que o usuário passará a ter</param>
        /// <returns>Pode retornar 204, 400 ou 404, </returns>
        [HttpPatch("AtualizarNome/{id}")]
        public IActionResult AtualizarNome(int id, string nome)
        {
            if (VerificaNulo(nome))
                return BadRequest(new { Error = Textos.NaoNulo("Nome") });
            else if (nome == "")
                return BadRequest(new { Error = Textos.NaoVazio("Nome") });
            else if (NomeJaExistente(nome))
                return BadRequest(new { Error = Textos.JaExistente("Nome") });

            Funcionario funcionario = _funcionario.AtualizarNome(id, nome);

            if (VerificaNulo(funcionario))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });

            return NoContent();
        }

        /// <summary>
        /// Método PATCH utilizado para atualizar o e-mail do funcionário, verificando se ele é vazio, nulo e se o funcionário foi encontrado.
        /// </summary>
        /// <param name="id">Id do usuário que se deseja atualizar.</param>
        /// <param name="email">Email novo a ser atribuído ao funcionário.</param>
        /// <returns>Pode retornar status 204, 400 ou 404.</returns>
        [HttpPatch("AtualizarEmail/{id}")]
        public IActionResult AtualizarEmail(int id, string email)
        {
            if (VerificaNulo(email))
                return BadRequest(new { Error = Textos.NaoNulo("E-mail") });
            else if (email == "")
                return BadRequest(new { Error = Textos.NaoVazio("E-mail") });


            Funcionario funcionario = _funcionario.AtualizarEmail(id, email);

            if (VerificaNulo(funcionario))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });

            return NoContent();
        }

        /// <summary>
        /// Método PATCH utilizado para atualizar o telefone do funcionário, verificando se ele é nulo, vazio e se encontrou o funcionário.
        /// </summary>
        /// <param name="id">Id do usuário que se quer atualizar.</param>
        /// <param name="telefone">Novo telefone para atribuir ao funcionário.</param>
        /// <returns>Retornar status 204, 400 ou 404.</returns>
        [HttpPatch("AtualizarTelefone/{id}")]
        public IActionResult AtualizarTelefone(int id, string telefone)
        {
            if (VerificaNulo(telefone))
                return BadRequest(new { Error = Textos.NaoNulo("Telefone") });
            else if (!VerificaTelefone(telefone))
                return BadRequest(new { Error = Textos.TelefoneForaPadrao() });

            Funcionario funcionario = _funcionario.AtualizarTelefone(id, telefone);

            if (VerificaNulo(funcionario))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });

            return NoContent();
        }

        /// <summary>
        /// Método DELETE, deve ser utilizado para apagar um funcionário do banco de dados.
        /// </summary>
        /// <param name="id">Id do usuário que se deseja apagar.</param>
        /// <returns>Pode retornar status 204 ou 404.</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var funcionario = _funcionario.ObterPorId(id);

            if (VerificaNulo(funcionario))
                return NotFound(new { Error = Textos.NaoEncontrado("Funcionário") });

            _funcionario.Deletar(id);

            return NoContent();
        }
        
        /// <summary>
        /// Método auxiliar que verifica se o nome passado já está cadastrado.
        /// </summary>
        /// <param name="nome">Nome que se deseja verificar.</param>
        /// <returns>Retorna um booleano se o nome é repetido ou não.</returns>
        [NonAction]
        public bool NomeJaExistente(string nome)
        {
            bool nomeRepetido = _funcionario.ObterTodos().Where(x => x.Nome.Contains(nome)).Count() > 0 
                                ? true : false;

            return nomeRepetido;
        }

        /// <summary>
        /// Método auxiliar, verifica se um número está de acordo com os padrões
        /// </summary>
        /// <param name="telefone">Telefone que se deseja verificar</param>
        /// <returns>Retorna um booleano dizendo se o número é válido ou não</returns>
        [NonAction]
        public bool VerificaTelefone(string telefone)
        {
            bool numeroValido = false;
            Regex numeroCelular = new Regex("^[0-9]{5}-[0-9]{4}$");
            Regex numeroCelularComDDD = new Regex("^\\([0-9]{2}\\)[0-9]{5}-[0-9]{4}$");

            if (numeroCelular.IsMatch(telefone) || numeroCelularComDDD.IsMatch(telefone))
                numeroValido = true;

            return numeroValido;
        }

        [NonAction]
        public bool VerificaNulo(Object objeto)
        {
            if (objeto == null)
                return true;

            return false;
        }
    }
}