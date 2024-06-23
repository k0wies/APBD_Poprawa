using APBDpoprawa.DTOs;

namespace APBDpoprawa.Interfaces;

public interface IPatientRepository
{
    Task<PatientDTO> GetPatientById(int idPatient);
}