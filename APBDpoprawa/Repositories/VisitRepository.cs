using APBDpoprawa.Context;
using APBDpoprawa.DTOs;
using APBDpoprawa.Interfaces;
using APBDpoprawa.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDpoprawa.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly MasterContext _context;

    public async Task<int> AddVisit(AddVisitDTO visit)
    {
        try
        {
            var result = 0;

            var patient = await _context
                .Patients
                .FirstOrDefaultAsync(e => e.IdPatient == visit.IdPatient);
        
            if (patient == null)
            {
                throw new Exception("No such Patient in DB");
            }
        
            var doctor = await _context
                .Doctors
                .FirstOrDefaultAsync(e => e.IdDoctor == visit.IdDoctor);
        
            if (doctor == null)
            {
                throw new Exception("No such Doctor in DB");
            }

            var futureVisits = _context
                .Visits
                .FirstOrDefaultAsync(v => v.IdPatient == visit.IdPatient && v.Date > DateTime.Now);
            
            if (futureVisits != null)
            {
                throw new ArgumentException("Patient has visits in future");
            }
            
            var doctorSchedule = _context
                .Schedules
                .FirstOrDefault(s => s.IdDoctor == visit.IdDoctor && s.DateFrom <= visit.Date && s.DateTo >= visit.Date);
            
            if (doctorSchedule == null)
            {
                throw new ArgumentException("Doctor does not work at this date");
            }

            var visitsWithThisDoctor = _context
                .Visits
                .Count(v => v.IdPatient == visit.IdPatient && v.IdDoctor == visit.IdDoctor);

            var price = doctor.PriceForVisit;
            
            if (visitsWithThisDoctor > 10)
            {
                price *= 0.9M;
            }
             
            var newVisit = new Visit
            {
                Date = DateTime.Now,
                IdPatient = visit.IdPatient,
                IdDoctor = visit.IdDoctor,
                Price = doctor.PriceForVisit
            };
        
            _context.Visits.Add(newVisit);
            result = await _context.SaveChangesAsync();
        
            return result;
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}