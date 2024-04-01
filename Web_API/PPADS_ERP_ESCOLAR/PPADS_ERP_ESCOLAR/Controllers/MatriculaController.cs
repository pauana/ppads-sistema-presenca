using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/matricula")]
public class MatriculaController : ControllerBase
{
    private readonly IMatriculaRepository _matriculaRepository;

    public MatriculaController(IMatriculaRepository matriculaRepository)
    {
        _matriculaRepository = matriculaRepository ?? throw new ArgumentNullException(nameof(matriculaRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(MatriculaViewModel matriculaModel)
    {
        var matricula = new Matricula(matriculaModel.idAluno, matriculaModel.idSerie, matriculaModel.idTurma, matriculaModel.chamada);

        _matriculaRepository.Add(matricula);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var matriculas = _matriculaRepository.Get();
        return Ok(matriculas);
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

        var matricula = _matriculaRepository.GetById(id);

        if (matricula == null)
        {
            return NotFound("Matrícula não encontrado.");
        }

        return Ok(matricula);
    }

    [HttpGet]
    [Route("matricula_por_serieturma/{tipo}:{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get(char tipo, int id)
    {
        if (id == 0)
        {
            return BadRequest("id Inválido!");
        }

        var matriculas = _matriculaRepository.Get(tipo, id);

        if (matriculas.Count == 0)
        {
            return NotFound("Matrículas não encontradas.");
        }

        return Ok(matriculas);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] MatriculaViewModel updatedMatricula)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingMatricula = _matriculaRepository.GetById(id);

            if (existingMatricula == null)
            {
                return NotFound("Aula não encontrada");
            }

            existingMatricula.idTurma = updatedMatricula.idTurma;
            existingMatricula.idAluno = updatedMatricula.idAluno;
            existingMatricula.chamada = updatedMatricula.chamada;

            _matriculaRepository.Update(existingMatricula);

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
        var matricula = _matriculaRepository.GetById(id);
        if (matricula == null)
        {
            return NotFound();
        }
        _matriculaRepository.Delete(matricula);
        return NoContent();
    }
}
