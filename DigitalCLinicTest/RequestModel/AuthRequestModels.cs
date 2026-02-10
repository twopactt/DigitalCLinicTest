namespace DigitalCLinicTest.RequestModel
{
    public class PatientAuthRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class DoctorAuthRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
