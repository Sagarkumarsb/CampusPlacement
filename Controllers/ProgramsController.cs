using CampusPlacement.Dtos;
using CampusPlacement.Models;
using CampusPlacement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProgramsController : ControllerBase
{
    private readonly IProgramService _programService;

    public ProgramsController(IProgramService programService)
    {
        _programService = programService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProgram([FromBody] ProgramDto programDto)
    {
        var program = new ProgramModel
        {
            Id = programDto.Id,
            Name = programDto.Name,
            Description = programDto.Description,
            Questions = programDto.Questions.ConvertAll(q => new QuestionModel
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                Options = q.Options
            })
        };

        await _programService.AddProgramAsync(program);
        return CreatedAtAction(nameof(GetProgramById), new { id = program.Id }, program);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProgram(string id, [FromBody] ProgramDto programDto)
    {
        var program = new ProgramModel
        {
            Id = programDto.Id,
            Name = programDto.Name,
            Description = programDto.Description,
            Questions = programDto.Questions.ConvertAll(q => new QuestionModel
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type,
                Options = q.Options
            })
        };

        await _programService.UpdateProgramAsync(id, program);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProgramById(string id)
    {
        var program = await _programService.GetProgramByIdAsync(id);
        if (program == null)
        {
            return NotFound();
        }
        return Ok(program);
    }
}
