using APBDpoprawa.DTOs;

namespace APBDpoprawa.Interfaces;

public interface IVisitRepository
{
    Task<int> AddVisit(AddVisitDTO visit);
}