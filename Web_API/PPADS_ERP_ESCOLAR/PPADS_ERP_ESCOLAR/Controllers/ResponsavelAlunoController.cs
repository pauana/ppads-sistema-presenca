using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/responsavel_aluno")]
public class ResponsavelAlunoController : ControllerBase
{
    private readonly IResponsavelAlunoRepository _responsavelAlunoRepository;

    public ResponsavelAlunoController(IResponsavelAlunoRepository responsavelAlunoRepository)
    {
        _responsavelAlunoRepository = responsavelAlunoRepository ?? throw new ArgumentNullException(nameof(responsavelAlunoRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(ResponsavelAlunoViewModel responsavelAlunoModel)
    {
        var responsavelAluno = new ResponsavelAluno(responsavelAlunoModel.idResponsavel, responsavelAlunoModel.idAluno);

        _responsavelAlunoRepository.Add(responsavelAluno);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var responsaveisAluno = _responsavelAlunoRepository.Get();
        return Ok(responsaveisAluno);
    }

    [HttpGet]
    [Route("responsaveisAluno_por_id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        var responsaveisAlunos = _responsavelAlunoRepository.GetById(id);
        return Ok(responsaveisAlunos);
    }


    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var responsavelaluno = _responsavelAlunoRepository.GetById(id);
        if (responsavelaluno == null)
        {
            return NotFound();
        }
        _responsavelAlunoRepository.Delete(responsavelaluno);
        return NoContent();
    }
}
