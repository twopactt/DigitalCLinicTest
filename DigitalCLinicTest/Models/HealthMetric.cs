using System;
using System.Collections.Generic;

namespace DigitalCLinicTest.Models;

public partial class HealthMetric
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int MetricTypeId { get; set; }

    public decimal Value { get; set; }

    public DateTime MeasuredAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual MetricType MetricType { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
