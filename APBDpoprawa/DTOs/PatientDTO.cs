using APBDpoprawa.Models;

namespace APBDpoprawa.DTOs;

public class PatientDTO
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public decimal TotalAmountMoneySpent{ get; set; }

    public int numberOfVisit{ get; set; }
    
    public List<VisitDTO> Visits { get; set; }

    
}