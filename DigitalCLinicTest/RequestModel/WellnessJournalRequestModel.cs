namespace DigitalCLinicTest.RequestModel
{
    public class CreateWellnessJournalRequestModel
    {
        public int PatientId { get; set; }
        public DateOnly EntryDate { get; set; }
        public int? MoodScore { get; set; }
        public string? Notes { get; set; }
    }
}
