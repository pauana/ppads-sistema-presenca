using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;


[ApiController]
[Route("api/v1/disciplina")]
public class DisciplinaController : ControllerBase
{
    private readonly IDisciplinaRepository _disciplinaRepository;

    public DisciplinaController(IDisciplinaRepository disciplinaRepository)
    {
        _disciplinaRepository = disciplinaRepository ?? throw new ArgumentNullException(nameof(disciplinaRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(DisciplinaViewModel disciplinaModel)
    {
        var disciplina = new Disciplina(disciplinaModel.materia);

        _disciplinaRepository.Add(disciplina);
        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var disciplinas = _disciplinaRepository.Get();
        return Ok(disciplinas);
    }

    [HttpGet]
    [Route("disciplina_por_id")]
    public IActionResult GetDisciplinaById(int id) 
    {
        if (id == 0)
        {
            return BadRequest("Id Inválido!");
        }

        var disciplina = _disciplinaRepository.GetById(id);

        if (disciplina == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(disciplina);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] DisciplinaViewModel updatedDisciplina)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingDisciplina = _disciplinaRepository.GetById(id);

            if (existingDisciplina == null)
            {
                return NotFound("Disciplina não encontrado!");
            }

            existingDisciplina.materia = updatedDisciplina.materia;

            _disciplinaRepository.Update(existingDisciplina);
            return Ok("Disciplina atualizada com sucesso!");
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
        var disciplina = _disciplinaRepository.GetById(id);
        if (disciplina == null)
        {
            return NotFound();
        }
        _disciplinaRepository.Delete(disciplina);

        return NoContent();
    }
}
