using APBDpoprawa.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APBDpoprawa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    
    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientById([FromRoute] int idPatient)
    {
        try
        {
            var result = await _patientRepository.GetPatientById(idPatient);

            if (result == null)
            {
                return NotFound("No patients found.");
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }


}