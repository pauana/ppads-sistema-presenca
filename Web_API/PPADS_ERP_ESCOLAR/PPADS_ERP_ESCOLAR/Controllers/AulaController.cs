using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;
using System.Linq;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/aula")]
public class AulaController : ControllerBase
{
    private readonly IAulaRepository _aulaRepository;
    private readonly IMatriculaRepository _matricuilaRepository;
    private readonly IRegistroPresencaRepository _registroPresencaRepository;
    private readonly IAlunoRepository _alunoRepository;
    private readonly DBConnection _context;

    public AulaController(IAulaRepository aulaRepository, IMatriculaRepository matriculaRepository, IRegistroPresencaRepository registroPresencaRepository, IAlunoRepository alunoRepository, DBConnection context)
    {
        _aulaRepository = aulaRepository ?? throw new ArgumentNullException(nameof(aulaRepository));
        _matricuilaRepository = matriculaRepository ?? throw new ArgumentNullException(nameof(matriculaRepository));
        _registroPresencaRepository = registroPresencaRepository ?? throw new ArgumentNullException(nameof(registroPresencaRepository));
        _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(AulaViewModel aulaModel)
    {
        var aula = new Aula(aulaModel.idTurma, aulaModel.idProfessor, aulaModel.data, aulaModel.periodo, aulaModel.conteudo);

        _aulaRepository.Add(aula);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var aulas = _aulaRepository.Get();
        return Ok(aulas);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] AulaViewModel updatedAula)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingAula = _aulaRepository.GetById(id);

            if (existingAula == null)
            {
                return NotFound("Aula não encontrada");
            }

            existingAula.idTurma = updatedAula.idTurma;
            existingAula.idProfessor = updatedAula.idProfessor;
            existingAula.data = updatedAula.data;
            existingAula.data = updatedAula.data;
            existingAula.conteudo = updatedAula.conteudo;

            _aulaRepository.Update(existingAula);

            return Ok("Aula atualizada com sucesso!");

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("lista_alunos_com_registro_presenca")]
    public IActionResult ListaDePresenca(int turmaId, int professorId, DateTime data, string periodo)
    {
        var query = from turma in _context.Turmas
                join matricula in _context.Matriculas on turma.idTurma equals matricula.idTurma
                join aluno in _context.Alunos on matricula.idAluno equals aluno.idAluno
                join aula in _context.Aulas
                    .Where(a => a.data == data && a.idProfessor == professorId) on turma.idTurma equals aula.idTurma into aulasGroup
                from aula in aulasGroup.DefaultIfEmpty()
                join registro in _context.RegistrosPresenca on new { MatriculaId = matricula.idMatricula, AulaId = (int?)aula.idAula } equals new { MatriculaId = registro.idMatricula, AulaId = (int?)registro.idAula } into registrosGroup
                from registro in registrosGroup.DefaultIfEmpty()
                where turma.idTurma == turmaId
                orderby matricula.chamada
                select new
                {
                    IdMatricula = matricula.idMatricula,
                    Aluno = $"{matricula.chamada} - {aluno.nome} ({aluno.ra})",
                    Presenca = registro != null ? registro.presenca : null
                };

        var result = query.ToList();

        return Ok(result);
    } 

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var aula = _aulaRepository.GetById(id);
        if (aula == null)
        {
            return NotFound();
        }
        _aulaRepository.Delete(aula);
        return NoContent();
    }


}
