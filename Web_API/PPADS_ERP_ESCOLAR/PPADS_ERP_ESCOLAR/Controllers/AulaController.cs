using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/aula")]
public class AulaController : ControllerBase
{
    private readonly IAulaRepository _aulaRepository;

    public AulaController(IAulaRepository aulaRepository)
    {
        _aulaRepository = aulaRepository ?? throw new ArgumentNullException(nameof(aulaRepository));
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
