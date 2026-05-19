using Cwiczenia7.DTOss;
using Cwiczenia7.Servicess;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia7.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PcsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        var result = await _dbService.GetAllPcsAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetPcComponents(int id)
    {
        var result = await _dbService.GetPcComponentsAsync(id);

        if (result == null)
            return NotFound($"PC with id {id} was not found.");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePc([FromBody] CreatePcDto dto)
    {
        var result = await _dbService.CreatePcAsync(dto);

        return CreatedAtAction(
            nameof(GetPcComponents),
            new { id = result.Id },
            result
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePc(int id, [FromBody] UpdatePcDto dto)
    {
        var result = await _dbService.UpdatePcAsync(id, dto);

        if (result == null)
            return NotFound($"PC with id {id} was not found.");

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        var deleted = await _dbService.DeletePcAsync(id);

        if (!deleted)
            return NotFound($"PC with id {id} was not found.");

        return NoContent();
    }
}