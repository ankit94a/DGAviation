using MasterApplication.DB.Interface;
using MasterApplication.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterApplication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeController : ControllerBase
    {
        private readonly EncriptionService _encriptionService;
        private readonly IAttributeDB _attributeDB;
        public AttributeController(IAttributeDB attributeDB, EncriptionService encriptionService)
        {
            _attributeDB = attributeDB;
            _encriptionService = encriptionService;
        }

        [HttpGet, Route("category")]
        public IActionResult GetAllCategory()
        {
            var encryptedData = _attributeDB.GetAllCategory();
            var decryptedData = encryptedData.Select(p => _encriptionService.DecryptModel(p)).ToList();
            return Ok(encryptedData);
        }

        [HttpPost, Route("category")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            category.Name = category.Name.ToUpper();
            category.CreatedBy = 2;
            category.CreatedOn = DateTime.Now;
            category.IsDeleted = false;
            category.IsActive = true;
            category = _encriptionService.EncryptModel(category);
            return Ok(_attributeDB.AddCategory(category));
        }
        [HttpPost, Route("category/updated")]
        public IActionResult UpdateCategory([FromBody] Category category)
        {
            category.Name = category.Name.ToUpper();
            category.UpdatedBy = 2;
            category.UpdatedOn = DateTime.Now;
            category.IsDeleted = false;
            category.IsActive = true;
            category = _encriptionService.EncryptModel(category);
            return Ok(_attributeDB.UpdateCategory(category));
        }

        [HttpGet, Route("natureofproject")]
        public IActionResult GetAllNatureOfProject()
        {
            var encryptedData = _attributeDB.GetAllNatureOfProject();
            var decryptedData = encryptedData.Select(p => _encriptionService.DecryptModel(p)).ToList();
            return Ok(encryptedData);
        }

        [HttpPost, Route("natureofproject")]
        public IActionResult AddNatureOfProject([FromBody] NatureOfProject natureOfProject)
        {
            natureOfProject.Name = natureOfProject.Name.ToUpper();
            natureOfProject.CreatedBy = 2;
            natureOfProject.CreatedOn = DateTime.Now;
            natureOfProject.IsDeleted = false;
            natureOfProject.IsActive = true;
            natureOfProject = _encriptionService.EncryptModel(natureOfProject);
            return Ok(_attributeDB.AddNatureOfProject(natureOfProject));
        }
        [HttpPost, Route("natureofproject/updated")]
        public IActionResult UpdateNatureOfProject([FromBody] NatureOfProject natureOfProject)
        {
            natureOfProject.Name = natureOfProject.Name.ToUpper();
            natureOfProject.UpdatedBy = 2;
            natureOfProject.UpdatedOn = DateTime.Now;
            natureOfProject.IsDeleted = false;
            natureOfProject.IsActive = true;
            natureOfProject = _encriptionService.EncryptModel(natureOfProject);
            return Ok(_attributeDB.UpdateNatureOfProject(natureOfProject));
        }

        [HttpGet, Route("projectstatus")]
        public IActionResult GetAllProjectStatus()
        {
            var encryptedData = _attributeDB.GetAllProjectStauts();
            var decryptedData = encryptedData.Select(p => _encriptionService.DecryptModel(p)).ToList();
            return Ok(encryptedData);
        }

        [HttpPost, Route("projectstatus")]
        public IActionResult AddProjectStatus([FromBody] ProjectStatus projectStatus)
        {
            projectStatus.Name = projectStatus.Name.ToUpper();
            projectStatus.CreatedBy = 2;
            projectStatus.CreatedOn = DateTime.Now;
            projectStatus.IsDeleted = false;
            projectStatus.IsActive = true;
            projectStatus = _encriptionService.EncryptModel(projectStatus);
            return Ok(_attributeDB.AddProjectStauts(projectStatus));
        }
        [HttpPost, Route("projectstatus/updated")]
        public IActionResult UpdateProjectStatus([FromBody] ProjectStatus projectStatus)
        {
            projectStatus.Name = projectStatus.Name.ToUpper();
            projectStatus.UpdatedBy = 2;
            projectStatus.UpdatedOn = DateTime.Now;
            projectStatus.IsDeleted = false;
            projectStatus.IsActive = true;
            projectStatus = _encriptionService.EncryptModel(projectStatus);
            return Ok(_attributeDB.UpdateProjectStauts(projectStatus));
        }

        [HttpPost, Route("delete")]
        public IActionResult DeleteDynamic([FromBody] DeactivateModel data)
        {
            return Ok(_attributeDB.DeleteDynamic(data));
        }
    }
}
