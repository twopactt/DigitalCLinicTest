namespace DigitalCLinicTest.RequestModel
{
    public class CreateConsultationRequestModel
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateConsultation { get; set; }
        public string? Notes { get; set; }
    }
}
