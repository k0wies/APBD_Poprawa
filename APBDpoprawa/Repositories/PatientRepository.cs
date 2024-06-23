using APBDpoprawa.Context;
using APBDpoprawa.DTOs;
using APBDpoprawa.Interfaces;
using APBDpoprawa.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDpoprawa.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly MasterContext _context;

    public PatientRepository(MasterContext context)
    {
        _context = context;
    }

    public async Task<PatientDTO> GetPatientById(int idPatient)
    {
        var result = _context
            .Patients
            .Include(e => e.Visits)
                .ThenInclude(d => d.IdDoctor)
            .Where(e => e.IdPatient == idPatient)
            .Select(e => new PatientDTO
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Birthdate = e.Birthdate,
                TotalAmountMoneySpent = e.Visits
                    .Select(f => f.Price).Sum(),
                numberOfVisit = e.Visits
                    .Select(f => f.IdPatient == idPatient).Count(),
                Visits = e.Visits
                    .Select(v => new VisitDTO
                    {
                        IdVisit = v.IdVisit,
                        Doctor = v.IdDoctorNavigation.FirstName + " " + v.IdDoctorNavigation.LastName,
                        Date = v.Date,
                        Price = $"{v.Price} zł"
                    }).ToList()
            }).ToList().FirstOrDefault();

        return result;
    }
    
}