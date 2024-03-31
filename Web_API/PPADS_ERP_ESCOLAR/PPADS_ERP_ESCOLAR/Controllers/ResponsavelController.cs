using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/responsavel")]
public class ResponsavelController : ControllerBase
{
    private readonly IResponsavelRepository _responsavelRepository;

    public ResponsavelController(IResponsavelRepository responsavelRepository)
    {
        _responsavelRepository = responsavelRepository ?? throw new ArgumentNullException(nameof(responsavelRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(ResponsavelViewModel responsavelModel)
    {
        var responsavel = new Responsavel(responsavelModel.nome, responsavelModel.email);

        _responsavelRepository.Add(responsavel);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var responsaveis = _responsavelRepository.Get();
        return Ok(responsaveis);
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

        var responsavel = _responsavelRepository.GetById(id);

        if (responsavel == null)
        {
            return NotFound("Responsável não encontrado.");
        }

        return Ok(responsavel);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] ResponsavelViewModel updatedResponsavel)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingResponsavel = _responsavelRepository.GetById(id);

            if (existingResponsavel == null)
            {
                return NotFound("Responsável não encontrado");
            }

            existingResponsavel.nome = updatedResponsavel.nome;
            existingResponsavel.email = updatedResponsavel.email;
            

            _responsavelRepository.Update(existingResponsavel);

            return Ok("Responsável atualizado com sucesso!");

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
        var responsavel = _responsavelRepository.GetById(id);
        if(responsavel == null)
        {
            return NotFound();
        }
        _responsavelRepository.Delete(responsavel);
        return NoContent();
    }

}
