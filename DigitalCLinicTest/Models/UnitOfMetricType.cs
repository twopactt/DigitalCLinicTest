using System;
using System.Collections.Generic;

namespace DigitalCLinicTest.Models;

public partial class UnitOfMetricType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MetricType> MetricTypes { get; set; } = new List<MetricType>();
}
