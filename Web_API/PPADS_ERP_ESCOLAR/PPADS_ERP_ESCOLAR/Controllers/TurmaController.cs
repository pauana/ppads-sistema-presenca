using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/turma")]
public class TurmaController : ControllerBase
{
    private readonly ITurmaRepository _turmaRepository;

    public TurmaController(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository ?? throw new ArgumentNullException(nameof(turmaRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(TurmaViewModel turmaModel)
    {
        var turma = new Turma(turmaModel.idSerie, turmaModel.qtdAlunos, turmaModel.classe);

        _turmaRepository.Add(turma);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var turmas = _turmaRepository.Get();
        return Ok(turmas);
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

        var turma = _turmaRepository.GetById(id);

        if (turma == null)
        {
            return NotFound("Turma não encontrada.");
        }

        return Ok(turma);
    }

    [HttpGet]
    [Route("turma_por_serie/{idSerie}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get(int idSerie)
    {
        if (idSerie == 0)
        {
            return BadRequest("idSerie Inválido!");
        }

        var turmas = _turmaRepository.Get(idSerie);

        if (turmas.Count == 0)
        {
            return NotFound("Turmas não encontradas.");
        }

        return Ok(turmas);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] TurmaViewModel updatedTurma)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingTurma = _turmaRepository.GetById(id);

            if (existingTurma == null)
            {
                return NotFound("Turma não encontrada");
            }

            existingTurma.idSerie = updatedTurma.idSerie;
            existingTurma.qtdAlunos = updatedTurma.qtdAlunos;
            existingTurma.classe = updatedTurma.classe;

            _turmaRepository.Update(existingTurma);

            return Ok("Turma atualizada com sucesso!");

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
        var turma = _turmaRepository.GetById(id);
        if(turma == null)
        {
            return NotFound();
        }
        _turmaRepository.Delete(turma);
        return NoContent();
    }

}
