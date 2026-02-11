CREATE NONCLUSTERED INDEX IX_HealthMetric_PatientId_MeasuredAt
ON dbo.HealthMetric (PatientId, MeasuredAt)
INCLUDE (MetricTypeId, Value);

CREATE NONCLUSTERED INDEX IX_HealthMetric_MetricTypeId_MeasuredAt
ON dbo.HealthMetric (MetricTypeId, MeasuredAt)
INCLUDE (PatientId, Value);

CREATE NONCLUSTERED INDEX IX_HealthMetric_MeasuredAt
ON dbo.HealthMetric (MeasuredAt)
INCLUDE (PatientId, MetricTypeId, Value);

CREATE NONCLUSTERED INDEX IX_HealthMetric_PatientId_MeasuredAt_Desc
ON dbo.HealthMetric (PatientId, MeasuredAt DESC)
INCLUDE (MetricTypeId, Value);