namespace DigitalCLinicTest.ResponceModels
{
    public class PatientConsultationResponseModel
    {
        public string PatientFullName { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime DateConsultation { get; set; }
        public string? Notes { get; set; }
    }

    public class PatientFullConsultationResponseModel
    {
        public int PatientId { get; set; }
        public string PatientFullName { get; set; }
        public List<PatientConsultationResponseModel> Consultations { get; set; } = new();
    }
}
