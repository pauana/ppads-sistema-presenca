using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/aluno")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoController(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository ?? throw new ArgumentNullException(nameof(alunoRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(AlunoViewModel alunoModel)
    {
        var aluno = new Aluno(alunoModel.nome, alunoModel.ra);

        _alunoRepository.Add(aluno);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var alunos = _alunoRepository.Get();
        return Ok(alunos);
    }

    [HttpGet]
    [Route("user_por_id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id Inválido!");
        }

        var aluno = _alunoRepository.GetById(id);

        if (aluno == null)
        {
            return NotFound("Aluno não encontrado.");
        }

        return Ok(aluno);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] AlunoViewModel updatedAluno)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingAluno = _alunoRepository.GetById(id);

            if (existingAluno == null)
            {
                return NotFound("Aula não encontrada");
            }

            existingAluno.nome = updatedAluno.nome;
            existingAluno.ra = updatedAluno.ra;
            

            _alunoRepository.Update(existingAluno);

            return Ok("Aluno atualizado com sucesso!");

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
        var aluno = _alunoRepository.GetById(id);
        if(aluno == null)
        {
            return NotFound();
        }
        _alunoRepository.Delete(aluno);
        return NoContent();
    }

}
