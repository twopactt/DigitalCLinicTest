using System;
using System.Collections.Generic;

namespace DigitalCLinicTest.Models;

public partial class SymptomCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Symptom> Symptoms { get; set; } = new List<Symptom>();
}
