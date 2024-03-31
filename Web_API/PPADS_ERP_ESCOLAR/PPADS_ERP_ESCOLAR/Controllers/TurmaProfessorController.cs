using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/turma_professor")]
public class TurmaProfessorController : ControllerBase
{
    private readonly ITurmaProfessorRepository _turmaProfessorRepository;

    public TurmaProfessorController(ITurmaProfessorRepository turmaProfessorRepository)
    {
        _turmaProfessorRepository = turmaProfessorRepository ?? throw new ArgumentNullException(nameof(turmaProfessorRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(TurmaProfessorViewModel turmaProfessorModel)
    {
        var turmaProfessor = new TurmaProfessor(turmaProfessorModel.idTurma, turmaProfessorModel.idProfessor);

        _turmaProfessorRepository.Add(turmaProfessor);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var turmaProfessor = _turmaProfessorRepository.Get();
        return Ok(turmaProfessor);
    }

    [HttpGet]
    [Route("turmaProfessor_por_id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        var turmasProfessor = _turmaProfessorRepository.GetById(id);
        return Ok(turmasProfessor);
    }


    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var turmaprofessor = _turmaProfessorRepository.GetById(id);
        if (turmaprofessor == null)
        {
            return NotFound();
        }
        _turmaProfessorRepository.Delete(turmaprofessor);
        return NoContent();
    }
}
