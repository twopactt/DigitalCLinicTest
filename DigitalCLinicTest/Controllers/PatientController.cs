using DigitalCLinicTest.DatabaseContext;
using DigitalCLinicTest.Helpers;
using DigitalCLinicTest.Models;
using DigitalCLinicTest.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace DigitalCLinicTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {
        private readonly DigitalTwinPatientDbtestContext _db;
        private readonly JwtService _jwtService;

        public PatientController(DigitalTwinPatientDbtestContext db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        [HttpPost("create-patient")]
        public IActionResult CreatePatient([FromBody] CreatePatientRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest(new { Message = "Почта и пароль обязательны" });

            var emailValidation = EmailValidator.ValidateEmail(model.Email);
            if (!emailValidation.IsValid)
                return BadRequest(new { Message = emailValidation.Message });

            var passwordValidation = PasswordValidator.ValidatePatientPassword(model.Password);
            if (!passwordValidation.IsValid)
                return BadRequest(new { Message = passwordValidation.Message });

            if (_db.Patients.Any(x => x.Email == model.Email))
                return BadRequest(new { Message = "Email уже зарегистрирован" });

            var patient = new Patient
            {
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                GenderId = model.GenderId,
                AddressId = model.AddressId,
                Birthday = model.Birthday,
                Phone = model.Phone,
                Email = model.Email,
                Password = PasswordHasher.Hash(model.Password)
            };

            _db.Patients.Add(patient);
            _db.SaveChanges();

            var token = _jwtService.GenerateToken(
                userId: patient.Id.ToString(),
                role: "Patient",
                name: $"{patient.Surname} {patient.Name} {patient.Patronymic}"
            );

            return Ok(new
            {
                Message = "Регистрация успешна",
                Patient = new
                {
                    patient.Id,
                    patient.Surname,
                    patient.Name,
                    patient.Email,
                    patient.Phone
                },
                Token = token,
                Role = "Patient"
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var patients = _db.Patients.ToList();
            return Ok(patients);
        }
    }
}
