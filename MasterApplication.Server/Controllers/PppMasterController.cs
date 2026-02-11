using MasterApplication.DB.Implements;
using MasterApplication.DB.Interface;
using MasterApplication.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PppMasterController : ControllerBase
    {
        private readonly EncriptionService _encriptionService;
        private readonly IPppMasterDB _pppMasterDB;
        public PppMasterController(EncriptionService encriptionService, IPppMasterDB pppMasterDB)
        {
            _encriptionService = encriptionService;
            _pppMasterDB = pppMasterDB;
        }

        [HttpGet]
        public IActionResult GetAllPPP()
        {
            var encryptedData = _pppMasterDB.GetAllPppMaster();
            //var decryptedData = encryptedData.Select(p => _encriptionService.DecryptModel(p)).ToList();
            return Ok(encryptedData);
        }
        [HttpPost]
        public IActionResult AddPpp([FromBody] PppMaster pppMaster)
        {
            pppMaster.CreatedBy = 2;
            pppMaster.CreatedOn = DateTime.Now;
            pppMaster.IsDeleted = false;
            pppMaster.IsActive = true;
            pppMaster = _encriptionService.EncryptModel(pppMaster);
            return Ok(_pppMasterDB.AddPppMaster(pppMaster));
        }
        [HttpPost, Route("update")]
        public IActionResult UpdatePpp([FromBody] PppMaster pppMaster)
        {
            pppMaster.CreatedBy = 2;
            pppMaster.CreatedOn = DateTime.Now;
            pppMaster.IsDeleted = false;
            pppMaster.IsActive = true;
            pppMaster = _encriptionService.EncryptModel(pppMaster);
            return Ok(_pppMasterDB.UpdatePppMaster(pppMaster));
        }
    }
}
