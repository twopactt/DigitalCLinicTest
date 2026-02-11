using DigitalCLinicTest.DatabaseContext;
using DigitalCLinicTest.Helpers;
using DigitalCLinicTest.Models;
using DigitalCLinicTest.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace DigitalCLinicTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellnessJournalController : ControllerBase
    {
        private readonly DigitalTwinPatientDbtestContext _db;

        public WellnessJournalController(DigitalTwinPatientDbtestContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateWellnessJournal([FromBody] CreateWellnessJournalRequestModel model)
        {
            try
            {
                var wellnessJournal = new WellnessJournal
                {
                    PatientId = model.PatientId,
                    EntryDate = model.EntryDate,
                    MoodScore = model.MoodScore,
                    Notes = model.Notes
                };

                _db.WellnessJournals.Add(wellnessJournal);
                _db.SaveChanges();

                return Ok(new
                {
                    Message = "Дневник самочувствия создан"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
