using DigitalCLinicTest.Models;

namespace DigitalCLinicTest.ResponceModels
{
    public class PatientAuthResponseModel
    {
        public Patient Patient { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }

    public class DoctorAuthResponseModel
    {
        public Doctor Doctor { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
