using RxAPI.Config;
using RxAPI.Interfaces.Services;
using RxAPI.Models;

namespace RxAPI.Services;

public class PatientService : IPatientService
{
    readonly HttpClient _httpClient;
    readonly string _patientApiUri;

    public PatientService(HttpClient httpClient, ApiConfig apiConfig)
    {
        _httpClient = httpClient;
        _patientApiUri = apiConfig.PatientsUrl;
    }

    public async Task<PatientModel> GetPatient(string id)
    {
        PatientModel patient = null;

        var response = await _httpClient.GetAsync($"{_patientApiUri}/patient/{id}");
        if (response.IsSuccessStatusCode)
        {
            patient = await response.Content.ReadFromJsonAsync<PatientModel>();
        }
        return patient;
    }

    public async Task<IEnumerable<PatientModel>> GetPatients()
    {
        IEnumerable<PatientModel> patients = Enumerable.Empty<PatientModel>();
        var response = await _httpClient.GetAsync($"{_patientApiUri}/patients");
        if (response.IsSuccessStatusCode)
        {
            patients = await response.Content.ReadFromJsonAsync<IEnumerable<PatientModel>>() ?? Enumerable.Empty<PatientModel>();
        }
        return patients;
    }
}