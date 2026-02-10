using DigitalCLinicTest.DatabaseContext;
using DigitalCLinicTest.Helpers;
using DigitalCLinicTest.RequestModel;
using DigitalCLinicTest.ResponceModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalCLinicTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly DigitalTwinPatientDbtestContext _db;
        private readonly JwtService _jwtService;

        public AuthController(DigitalTwinPatientDbtestContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost("patient/auth")]
        public IActionResult PatientAuth([FromBody] PatientAuthRequestModel model)
        {
            var hashedPassword = PasswordHasher.Hash(model.Password);

            var patient = _db.Patients
                .FirstOrDefault(x => x.Email == model.Email && x.Password == hashedPassword);

            if (patient == null)
                return Unauthorized(new { Message = "Неверный логин или пароль" });

            var token = _jwtService.GenerateToken(
               userId: patient.Id.ToString(),
               role: "Patient",
               name: $"{patient.Surname} {patient.Name} {patient.Patronymic}"
           );

            return Ok(new PatientAuthResponseModel
            {
                Patient = patient,
                Token = token,
                Role = "Patient"
            });
        }

        [HttpPost("doctor/auth")]
        public IActionResult DoctorAuth([FromBody] DoctorAuthRequestModel model)
        {
            var hashedPassword = PasswordHasher.Hash(model.Password);

            var doctor = _db.Doctors
                .FirstOrDefault(x => x.Email == model.Email && x.Password == hashedPassword);

            if (doctor == null)
                return Unauthorized(new { Message = "Неверный логин или пароль" });

            var token = _jwtService.GenerateToken(
               userId: doctor.Id.ToString(),
               role: "Doctor",
               name: $"{doctor.Surname} {doctor.Name} {doctor.Patronymic}"
           );

            return Ok(new DoctorAuthResponseModel
            {
                Doctor = doctor,
                Token = token,
                Role = "Doctor"
            });
        }
    }
}
