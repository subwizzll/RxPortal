using RxAPI.Models;

namespace RxAPI.Interfaces.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientModel>> GetPatients();
    Task<PatientModel> GetPatient(string id);
}