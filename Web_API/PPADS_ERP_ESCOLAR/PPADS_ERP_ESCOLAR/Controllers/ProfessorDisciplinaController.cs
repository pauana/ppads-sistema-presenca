using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/professor_disciplina")]
public class ProfessorDisciplinaController : ControllerBase
{
    private readonly IProfessorDisciplinaRepository _profDiscRepository;

    public ProfessorDisciplinaController(IProfessorDisciplinaRepository profDiscRepository)
    {
        _profDiscRepository = profDiscRepository ?? throw new ArgumentNullException(nameof(profDiscRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(ProfessorDisciplinaViewModel profDiscModel)
    {
        var profDisc = new ProfessorDisciplina(profDiscModel.idProfessor, profDiscModel.idDisciplina);

        _profDiscRepository.Add(profDisc);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var professores = _profDiscRepository.Get();
        return Ok(professores);
    }

    [HttpGet]
    [Route("professorDisciplina_por_id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetByProfessorId(int id)
    {
        var professores = _profDiscRepository.GetByProfId(id);
        return Ok(professores);
    }


    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int id)
    {
        var professor = _profDiscRepository.GetByProfId(id);
        if (professor == null)
        {
            return NotFound();
        }
        _profDiscRepository.Delete(professor);
        return NoContent();
    }
}
