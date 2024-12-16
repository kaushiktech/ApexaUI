using ApexApi.Data.Repository.IRepository;
using ApexApi.Models;
using ApexApi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApexApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : Controller
    {
        private readonly IAdvisorRepository _repository;
        public AdvisorController(IAdvisorRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [Route("GetAllAdvisors")]
        public async Task<IActionResult> GetAdvisors()
        {
            var objAdvisorList = _repository.GetAll();
            return Json(new { data = objAdvisorList });
        }
        [HttpGet]
        [Route("GetAdvisor")]
        public async Task<IActionResult> GetAdvisor(long id)
        {
            var objAdvisor = _repository.Get(adv => adv.Id == id);
            if (objAdvisor == null)
                return NotFound(new { Status = "Error", Message = "Advisor not found!" });
            else
                return Json(new { data = objAdvisor });
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("DeleteAdvisor")]
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            else
            {
                var advisor = _repository.Get(x => x.Id == id);
                if (advisor == null)
                    return NotFound(new { Status = "Error", Message = "Advisor not found!" });
                _repository.Remove(advisor);
                _repository.Save();
                return Ok(new Response { Status = "Success", Message = "Advisor deleted successfully!" });
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("CreateAdvisor")]
        [HttpPost]
        public IActionResult Create(Advisor obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(obj);
                _repository.Save();
                return Ok(new Response { Status = "Success", Message = "Advisor created successfully!" });
            }
            else
                return BadRequest();
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("UpdateAdvisor")]
        [HttpPut]
        public IActionResult Edit(UpdateAdvisor obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(obj);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
