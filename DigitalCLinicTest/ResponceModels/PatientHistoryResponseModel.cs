namespace DigitalCLinicTest.ResponceModels
{
    public class PatientHistoryResponseModel
    {
        public string PatientFullName { get; set; }
        public string DiagnosisName { get; set; }
        public DateOnly DiagnosedDate { get; set; }
        public string DiagnosisStatusName { get; set; }
        public string MedicationName { get; set; }
        public decimal Quantity { get; set; }
        public string DoseUnitName { get; set; }
        public string FrequencyName { get; set; }
        public int DurationInDays { get; set; }
        public string? Notes { get; set; }
    }

    public class PatientFullHistoryResponseModel
    {
        public int PatientId { get; set; }
        public string PatientFullName { get; set; }
        public List<PatientHistoryResponseModel> Histories { get; set; } = new();
    }
}
