using System;
using System.Collections.Generic;

namespace DigitalCLinicTest.Models;

public partial class Consultation
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public DateTime DateConsultation { get; set; }

    public string? Notes { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
