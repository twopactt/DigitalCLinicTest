ALTER TABLE [dbo].[HealthMetric] 
WITH CHECK ADD CONSTRAINT [CHK_HealthMetric_CreatedAt] 
CHECK (([CreatedAt] >= [MeasuredAt]));

ALTER TABLE [dbo].[HealthMetric] 
WITH CHECK ADD CONSTRAINT [CHK_HealthMetric_MeasuredAt] 
CHECK (([MeasuredAt] <= GETDATE()));