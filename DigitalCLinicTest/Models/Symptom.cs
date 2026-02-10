using System;
using System.Collections.Generic;

namespace DigitalCLinicTest.Models;

public partial class Symptom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int SymptomCategoryId { get; set; }

    public virtual ICollection<PatientComplaint> PatientComplaints { get; set; } = new List<PatientComplaint>();

    public virtual SymptomCategory SymptomCategory { get; set; } = null!;
}
