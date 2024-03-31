using Microsoft.AspNetCore.Mvc;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;
using PPADS_ERP_ESCOLAR.Models;
using PPADS_ERP_ESCOLAR.ViewModels;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1/serie")]
public class SerieController : ControllerBase
{
    private readonly ISerieRepository _serieRepository;

    public SerieController(ISerieRepository serieRepository)
    {
        _serieRepository = serieRepository ?? throw new ArgumentNullException(nameof(serieRepository));
    }

    [HttpPost]
    [Route("criar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Add(SerieViewModel serieModel)
    {
        var serie = new Serie(serieModel.ano, serieModel.nomeSerie, serieModel.qtdeTurmas, serieModel.vagas);

        _serieRepository.Add(serie);

        return Ok();
    }

    [HttpGet]
    [Route("lista")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var series = _serieRepository.Get();
        return Ok(series);
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

        var serie = _serieRepository.GetById(id);

        if (serie == null)
        {
            return NotFound("Série não encontrada.");
        }

        return Ok(serie);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int id, [FromBody] SerieViewModel updatedSerie)
    {
        try
        {
            if (id <= 0)
            {
                return BadRequest("Id Inválido!");
            }

            var existingSerie = _serieRepository.GetById(id);

            if (existingSerie == null)
            {
                return NotFound("Série não encontrada");
            }

            existingSerie.ano = updatedSerie.ano;
            existingSerie.nomeSerie = updatedSerie.nomeSerie;
            existingSerie.qtdeTurmas = updatedSerie.qtdeTurmas;
            existingSerie.vagas = updatedSerie.vagas;
            

            _serieRepository.Update(existingSerie);

            return Ok("Série atualizada com sucesso!");

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
        var serie = _serieRepository.GetById(id);
        if(serie == null)
        {
            return NotFound();
        }
        _serieRepository.Delete(serie);
        return NoContent();
    }

}
