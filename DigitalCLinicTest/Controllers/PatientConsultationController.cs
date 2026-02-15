using DigitalCLinicTest.DatabaseContext;
using DigitalCLinicTest.Helpers;
using DigitalCLinicTest.Models;
using DigitalCLinicTest.RequestModel;
using DigitalCLinicTest.ResponceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalCLinicTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientConsultationController : ControllerBase
    {
        private readonly DigitalTwinPatientDbtestContext _db;

        public PatientConsultationController(DigitalTwinPatientDbtestContext db)
        {
            _db = db;
        }

        [HttpGet("{patientId}")]
        public IActionResult GetPatientConsultations(int patientId)
        {
            try
            {
                var patient = _db.Patients
                    .Where(p => p.Id == patientId)
                    .Select(p => new
                    {
                        p.Id,
                        p.Surname,
                        p.Name,
                        p.Patronymic
                    })
                    .FirstOrDefault();

                if (patient == null)
                    return NotFound(new { error = "Patient not found" });

                var consultations = _db.Consultations
                    .Include(c => c.Doctor)
                    .Where(c => c.PatientId == patientId)
                    .Select(c => new PatientConsultationResponseModel
                    {
                        PatientFullName = $"{patient.Surname} {patient.Name} {patient.Patronymic}".Trim(),
                        DoctorFullName = $"{c.Doctor.Surname} {c.Doctor.Name} {c.Doctor.Patronymic}".Trim(),
                        DateConsultation = c.DateConsultation,
                        Notes = c.Notes
                    })
                    .ToList();

                var result = new PatientFullConsultationResponseModel
                {
                    PatientId = patientId,
                    PatientFullName = $"{patient.Surname} {patient.Name} {patient.Patronymic}".Trim(),
                    Consultations = consultations,
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createPatientConsultation")]
        public IActionResult CreatePatientConsultation(CreateConsultationRequestModel model)
        {
            try
            {
                var consultaion = new Consultation
                {
                    PatientId = model.PatientId,
                    DoctorId = model.DoctorId,
                    DateConsultation = model.DateConsultation,
                    Notes = model.Notes
                };

                _db.Consultations.Add(consultaion);
                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Консультация создана"
                });
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message); 
            }
        }
    }
}
