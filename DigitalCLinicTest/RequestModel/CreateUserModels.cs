namespace DigitalCLinicTest.RequestModel
{
    public class CreatePatientRequestModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public int GenderId { get; set; }
        public int AddressId { get; set; }
        public DateOnly Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateDoctorRequestModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public int SpecializationId { get; set; }
        public int DepartmentId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
