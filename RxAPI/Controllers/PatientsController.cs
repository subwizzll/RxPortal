using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RxAPI.Interfaces.Services;
using RxAPI.Models;
using RxAPI.Models.DTO;
using RxAPI.Types;

namespace RxAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(UserRole.Admin))]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    readonly ILogger<PatientsController> _logger;
    readonly IPatientService _patientService;
    readonly IEncryptionService _encryptionService;

    public PatientsController(ILogger<PatientsController> logger, IPatientService patientService, IEncryptionService encryptionService)
    {
        _logger = logger;
        _patientService = patientService;
        _encryptionService = encryptionService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatients()
    {
        var patientList = await _patientService.GetPatients();
        var patientModelList = patientList.Select(Convert);

        if (patientModelList is null || !patientModelList.Any())
        {
            _logger.LogError("GetPatients: NotFound");
            return NotFound();
        }
        
        _logger.LogInformation("GetPatient: OK");
        return Ok(patientModelList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PatientModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPatient(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest();

        var patient = Convert(await _patientService.GetPatient(_encryptionService.Decrypt(id)));

        if (patient is null)
        {
            _logger.LogError("GetPatient: NotFound");
            return NotFound();
        }

        _logger.LogInformation("GetPatient: OK");
        return Ok(patient);
    }

    PatientDTO Convert(PatientModel patientModel) => new() 
    { 
        PatientId = _encryptionService.Encrypt(patientModel.PatientId),
        FirstName = patientModel.FirstName,
        LastName = patientModel.LastName,
        Gender = patientModel.Gender,
        DateOfBirth = patientModel.DateOfBirth,
        AddressLine1 = patientModel.AddressLine1,
        AddressLine2 = patientModel.AddressLine2,
        City = patientModel.City,
        State = patientModel.State,
        PostalCode = patientModel.PostalCode,
    };
}