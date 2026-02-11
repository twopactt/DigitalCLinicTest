using System;
using System.Collections.Generic;

namespace DigitalCLinicTest.Models;

public partial class MedicalCard
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int BloodTypeId { get; set; }

    public int Height { get; set; }

    public decimal Weight { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
