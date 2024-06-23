using APBDpoprawa.DTOs;
using APBDpoprawa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBDpoprawa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitController : ControllerBase
{
    private readonly IVisitRepository _visitRepository;

    public VisitController(IVisitRepository visitRepository)
    {
        _visitRepository = visitRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddVisit([FromBody] AddVisitDTO visit)
    {
        try
        {
            await _visitRepository.AddVisit(visit);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
    
}