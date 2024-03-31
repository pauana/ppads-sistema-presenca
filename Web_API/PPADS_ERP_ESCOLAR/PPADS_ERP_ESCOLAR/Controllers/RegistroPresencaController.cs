using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/registro_presenca")]
public class RegistroPresencaController : ControllerBase
{
    private readonly IRegistroPresencaRepository _registroPresencaRepository;

    public RegistroPresencaController(IRegistroPresencaRepository registroPresencaRepository)
    {
        _registroPresencaRepository = registroPresencaRepository ?? throw new ArgumentNullException(nameof(registroPresencaRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(RegistroPresencaViewModel registroPresencaModel)
    {
        var registroPresenca = new RegistroPresenca(registroPresencaModel.idMatricula, registroPresencaModel.idAula, registroPresencaModel.presenca);

        _registroPresencaRepository.Add(registroPresenca);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var registrosPresenca = _registroPresencaRepository.Get();
        return Ok(registrosPresenca);
    }

    [HttpGet]
    [Route("registroPresenca_por_id/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetById(int id)
    {
        if (id == 0)
        {
            return BadRequest("Id Inválido!");
        }

        var registroPresenca = _registroPresencaRepository.GetById(id);

        if (registroPresenca == null)
        {
            return NotFound("Registro não encontrado.");
        }

        return Ok(registroPresenca);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] RegistroPresencaViewModel updatedRegistroPresenca)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            if (updatedRegistroPresenca.presenca != "P" && updatedRegistroPresenca.presenca != "F")
            {
                return BadRequest("Presença inválida!");
            }

            var existingRegistroPresenca = _registroPresencaRepository.GetById(id);

            if (existingRegistroPresenca == null)
            {
                return NotFound("Registro não encontrado");
            }

            //existingRegistroPresenca.idMatricula = updatedRegistroPresenca.idMatricula;
            //existingRegistroPresenca.idAula = updatedRegistroPresenca.idAula;
            existingRegistroPresenca.presenca = updatedRegistroPresenca.presenca; // só pode alterar a presença

            _registroPresencaRepository.Update(existingRegistroPresenca);

            return Ok("Registro atualizado com sucesso!");

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
        var registroPresenca = _registroPresencaRepository.GetById(id);
        if(registroPresenca == null)
        {
            return NotFound();
        }
        _registroPresencaRepository.Delete(registroPresenca);
        return NoContent();
    }

}
