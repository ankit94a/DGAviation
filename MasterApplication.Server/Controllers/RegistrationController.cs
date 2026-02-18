using MasterApplication.DB.Implements;
using MasterApplication.DB.Interface;
using MasterApplication.DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace MasterApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly EncriptionService _encriptionService;
        private readonly IMemberRegistrationDb _memberRegistrationDb;
        public RegistrationController(IMemberRegistrationDb memberRegistrationDb, EncriptionService encriptionService)
        {
            _memberRegistrationDb = memberRegistrationDb;
            _encriptionService = encriptionService;
        }
        [HttpPost, Route("personalInfo")] 
        public async Task<IActionResult> AddPersonalInfo([FromForm] PersonalInfo personalInfo, [FromForm] IFormFile image)
        {
            personalInfo.Name = personalInfo.Name.ToUpper();
            personalInfo.CreatedBy = 1;
            personalInfo.CreatedOn = DateTime.Now;
            personalInfo.IsDeleted = false;
            personalInfo.IsActive = true;

            if (image != null && image.Length > 0)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-img");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = image.FileName;
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                personalInfo.ImgUrl = $"/profile-img/{fileName}";
            }

            //personalInfo = _encriptionService.EncryptModel(personalInfo);
            return Ok(await _memberRegistrationDb.AddPersonalInfo(personalInfo));
        }
        [HttpPost, Route("personalInfo/updated")]
        public async Task<IActionResult> UpdatePersonalInfo([FromForm] PersonalInfo personalInfo, [FromForm] IFormFile image)
        {
            personalInfo.Name = personalInfo.Name.ToUpper();
            personalInfo.UpdatedBy = 1;
            personalInfo.UpdatedOn = DateTime.Now;
            if (image != null && image.Length > 0)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-img");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var fileName = image.FileName;
                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);

                personalInfo.ImgUrl = $"/profile-img/{fileName}";
            }
            //personalInfo = _encriptionService.EncryptModel(personalInfo);
            return Ok(await _memberRegistrationDb.UpdatePersonalInfo(personalInfo));
        }

        [HttpGet, Route("personalInfo")]
        public IActionResult GetAll()
        {
            var encryptedData = _memberRegistrationDb.GetAllPersonalInfo();
       
          //  var decryptedData = encryptedData.Select(p => _encriptionService.DecryptModel(p)).ToList();
            return Ok(encryptedData);
        }

        [HttpGet, Route("personalInfo/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _memberRegistrationDb.GetPersonalInfoById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        //[HttpPost, Route("LastThreeAAAS")]
        //public IActionResult AddLastThreeAAAS([FromForm] LastThreeAAAS lastThreeAAAS)
        //{

        //    lastThreeAAAS.CreatedBy = 1;
        //    lastThreeAAAS.CreatedOn = DateTime.Now;
        //    lastThreeAAAS.IsDeleted = false;
        //    lastThreeAAAS.IsActive = true;
        //    //lastThreeAAAS = _encriptionService.EncryptModel(lastThreeAAAS);
        //    return Ok(_memberRegistrationDb.AddLastThreeAAAS(lastThreeAAAS));
        //}
        //[HttpPost, Route("AccidentDetails")]
        //public IActionResult AddAccidentDetails([FromForm] AccidentDetails accidentDetails)
        //{

        //    accidentDetails.CreatedBy = 1;
        //    accidentDetails.CreatedOn = DateTime.Now;
        //    accidentDetails.IsDeleted = false;
        //    accidentDetails.IsActive = true;
        //    //accidentDetails = _encriptionService.EncryptModel(accidentDetails);
        //    return Ok(_memberRegistrationDb.AddAccidentDetails(accidentDetails));
        //}

        [HttpPost, Route("SaveAAAAndAccident")]
        public async Task<IActionResult> SaveAAAAndAccident([FromBody] AAAAndAccidentRequest request)
        {
            if (request == null)
                return BadRequest("Invalid data");

            var now = DateTime.Now;

            foreach (var aaa in request.LastThreeAAAS)
            {
                aaa.CreatedBy = 1;
                aaa.CreatedOn = now;
                aaa.IsDeleted = false;
                aaa.IsActive = true;
            }

            foreach (var accident in request.AccidentDetails)
            {
                accident.CreatedBy = 1;
                accident.CreatedOn = now;
                accident.IsDeleted = false;
                accident.IsActive = true;
            }

            return Ok(await _memberRegistrationDb.SaveAAAAndAccident(request));
        }

        [HttpPost, Route("AdvExecRptRaised")]
        public IActionResult AddAdvExecRptRaised([FromForm] AdvExecRptRaised advExecRptRaised)
        {

            advExecRptRaised.CreatedBy = 1;
            advExecRptRaised.CreatedOn = DateTime.Now;
            advExecRptRaised.IsDeleted = false;
            advExecRptRaised.IsActive = true;
            //advExecRptRaised = _encriptionService.EncryptModel(advExecRptRaised);
            return Ok(_memberRegistrationDb.AddAdvExecRptRaised(advExecRptRaised));
        }
       [HttpPost, Route("AddPhysService")]
        public IActionResult AddPhysService([FromForm] PhysService physService)
        {

            physService.CreatedBy = 1;
            physService.CreatedOn = DateTime.Now;
            physService.IsDeleted = false;
            physService.IsActive = true;
            //physService = _encriptionService.EncryptModel(physService);
            return Ok(_memberRegistrationDb.AddPhysService(physService));
        }
        [HttpPost, Route("AddForeignVisit")]
        public IActionResult AddForeignVisit([FromForm] ForeignVisit foreignVisit)
        {

            foreignVisit.CreatedBy = 1;
            foreignVisit.CreatedOn = DateTime.Now;
            foreignVisit.IsDeleted = false;
            foreignVisit.IsActive = true;
            //foreignVisit = _encriptionService.EncryptModel(foreignVisit);
            return Ok(_memberRegistrationDb.AddForeignVisit(foreignVisit));
        }
        [HttpPost, Route("AddDvDtails")]
        public IActionResult AddDvDtails([FromForm] DvDtails dvDtails)
        {
            dvDtails.CreatedBy = 1; // later replace with logged-in user
            dvDtails.CreatedOn = DateTime.Now;
            dvDtails.IsDeleted = false;
            dvDtails.IsActive = true;

            // dvDtails = _encriptionService.EncryptModel(dvDtails);

            return Ok(_memberRegistrationDb.AddDvDtails(dvDtails));
        }
        [HttpPost, Route("AddServiceFLG")]
        public IActionResult AddServiceFLG([FromForm] ServiceFLG serviceFLG)
        {
            serviceFLG.CreatedBy = 1; // Replace with logged-in user later
            serviceFLG.CreatedOn = DateTime.Now;
            serviceFLG.IsDeleted = false;
            serviceFLG.IsActive = true;

            // serviceFLG = _encriptionService.EncryptModel(serviceFLG);

            return Ok(_memberRegistrationDb.AddServiceFLG(serviceFLG));
        }

        [HttpPost, Route("AddCourseAvnQual")]
        public IActionResult AddCourseAvnQual([FromForm] CourseAvnQual courseAvnQual)
        {
            courseAvnQual.CreatedBy = 1; // Replace with logged user
            courseAvnQual.CreatedOn = DateTime.Now;
            courseAvnQual.IsDeleted = false;
            courseAvnQual.IsActive = true;

            // courseAvnQual = _encriptionService.EncryptModel(courseAvnQual);

            return Ok(_memberRegistrationDb.AddCourseAvnQual(courseAvnQual));
        }
        [HttpPost, Route("AddCoreAttributeFlgRating")]
        public IActionResult AddCoreAttributeFlgRating([FromForm] CoreAttributeFlgRating coreAttributeFlgRating)
        {
            coreAttributeFlgRating.CreatedBy = 1; // Replace with logged user
            coreAttributeFlgRating.CreatedOn = DateTime.Now;
            coreAttributeFlgRating.IsDeleted = false;
            coreAttributeFlgRating.IsActive = true;

            // coreAttributeFlgRating = _encriptionService.EncryptModel(coreAttributeFlgRating);

            return Ok(_memberRegistrationDb.AddCoreAttributeFlgRating(coreAttributeFlgRating));
        }
        [HttpPost, Route("AddRecomForEMP")]
        public IActionResult AddRecomForEMP([FromForm] RecomForEMP recomForEMP)
        {
            recomForEMP.CreatedBy = 1; // Replace with logged-in user
            recomForEMP.CreatedOn = DateTime.Now;
            recomForEMP.IsDeleted = false;
            recomForEMP.IsActive = true;

            // recomForEMP = _encriptionService.EncryptModel(recomForEMP);

            return Ok(_memberRegistrationDb.AddRecomForEMP(recomForEMP));
        }
        [HttpPost, Route("AddReportingOfficersDetails")]
        public IActionResult AddReportingOfficersDetails([FromForm] ReportingOfficersDetails reportingOfficersDetails)
        {
            reportingOfficersDetails.CreatedBy = 1; // Replace with logged user
            reportingOfficersDetails.CreatedOn = DateTime.Now;
            reportingOfficersDetails.IsDeleted = false;
            reportingOfficersDetails.IsActive = true;

            // reportingOfficersDetails = _encriptionService.EncryptModel(reportingOfficersDetails);

            return Ok(_memberRegistrationDb.AddReportingOfficersDetails(reportingOfficersDetails));
        }


    }


}
