using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/professor")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessorController(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository ?? throw new ArgumentNullException(nameof(professorRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(ProfessorViewModel professorModel)
    {
        var professor = new Professor(professorModel.nome, professorModel.ativo);

        _professorRepository.Add(professor);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var professores = _professorRepository.Get();
        return Ok(professores);
    }

    [HttpGet]
    [Route("user_por_id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id Inválido!");
        }

        var professor = _professorRepository.GetById(id);

        if (professor == null)
        {
            return NotFound("Professor não encontrado.");
        }

        return Ok(professor);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] ProfessorViewModel updatedProfessor)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingProfessor = _professorRepository.GetById(id);

            if (existingProfessor == null)
            {
                return NotFound("Aula não encontrada");
            }

            existingProfessor.nome = updatedProfessor.nome;
            existingProfessor.ativo = updatedProfessor.ativo;

            _professorRepository.Update(existingProfessor);

            return Ok("Matrícula atualizado com sucesso!");

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var professor = _professorRepository.GetById(id);
        if (professor == null)
        {
            return NotFound();
        }
        _professorRepository.Delete(professor);
        return NoContent();
    }
}
