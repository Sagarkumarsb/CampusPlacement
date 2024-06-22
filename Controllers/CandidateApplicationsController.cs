using CampusPlacement.Dtos;
using CampusPlacement.Models;
using CampusPlacement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CandidateApplicationsController : ControllerBase
{
    private readonly ICandidateApplicationService _candidateApplicationService;

    public CandidateApplicationsController(ICandidateApplicationService candidateApplicationService)
    {
        _candidateApplicationService = candidateApplicationService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitApplication([FromBody] CandidateApplicationDto applicationDto)
    {
        var application = new CandidateApplication
        {
            Id = applicationDto.Id,
            ProgramId = applicationDto.ProgramId,
            Answers = applicationDto.Answers.ConvertAll(a => new Answer
            {
                QuestionId = a.QuestionId,
                Response = a.Response
            })
        };

        await _candidateApplicationService.SubmitApplicationAsync(application);
        return CreatedAtAction(nameof(GetApplicationById), new { id = application.Id }, application);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        var application = await _candidateApplicationService.GetApplicationByIdAsync(id);
        if (application == null)
        {
            return NotFound();
        }
        return Ok(application);
    }
}
